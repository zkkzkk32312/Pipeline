using System;
using System.Text;
using System.Net;
using System.Net.WebSockets;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Coe.WebSocketWrapper;
using Sirenix.OdinInspector;


public class SocketAsyncController : MonoBehaviour
{
    string socketURL = "wss://echo.websocket.org";
    UnityWebRequest unityWebRequest;
    ClientWebSocket clientWebSocket;
    WebSocketWrapper wsw;

    public Action<string, WebSocketWrapper> onMessageAction;
    public Action<WebSocketWrapper> onConnectAction;

    // Start is called before the first frame update
    void Start()
    {
        //unityWebRequest = new UnityWebRequest();
        //DoFetch();

        wsw = WebSocketWrapper.Create(socketURL);

        onMessageAction = OnMessageCallback;
        onConnectAction = OnConnectCallback;

        wsw.OnConnect(onConnectAction);
        wsw.OnMessage(onMessageAction);

        wsw.Connect();
    }

    private void OnConnectCallback(WebSocketWrapper obj)
    {
        Debug.LogError("Socket Server Connected : " + socketURL);
    }

    public void OnMessageCallback (string s, WebSocketWrapper wrapper)
    {
        Debug.LogError("Socket Message Received : " + s);
        FlowDataManager.Singleton.FlowDataReceived(s);
    }

    [Button]
    public void DoSend(string message)
    {
        wsw.SendMessage(message);
        Debug.LogError("Socket Message Sent : " + message);
    }

    IEnumerator FetchSocketData ()
    {
        unityWebRequest = UnityWebRequest.Get(socketURL);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
        {
            Debug.LogError("GetchSocketData ERROR : " + unityWebRequest.error);
        }
        else
        {
            Debug.LogError("SOCKET DATA = " + unityWebRequest.downloadHandler.text);
        }
    }

    async void FetchSocketData2 ()
    {
        clientWebSocket = new ClientWebSocket();
        clientWebSocket.Options.AddSubProtocol("Tls");

        try
        {
            Uri uri = new Uri(socketURL);
            await clientWebSocket.ConnectAsync(uri, CancellationToken.None);

            if (clientWebSocket.State == WebSocketState.Open)
            {
                Debug.Log("Input message ('exit' to exit): ");

                ArraySegment<byte> bytesToSend = new ArraySegment<byte>(
                    Encoding.UTF8.GetBytes("hello fury from unity")
                );
                await clientWebSocket.SendAsync(
                    bytesToSend,
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None
                );
                ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
                WebSocketReceiveResult result = await clientWebSocket.ReceiveAsync(
                    bytesReceived,
                    CancellationToken.None
                );
                Debug.Log(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));
            }
            Debug.Log("[WS][connect]:" + "Connected");
        }
        catch (Exception e)
        {
            Debug.Log("[WS][exception]:" + e.Message);
            if (e.InnerException != null)
            {
                Debug.Log("[WS][inner exception]:" + e.InnerException.Message);
            }
        }
    }

    async Task FetchSocketDataEcho()
    {
        using (ClientWebSocket ws = new ClientWebSocket())
        {
            Uri serverUri = new Uri(socketURL);
            await ws.ConnectAsync(serverUri, CancellationToken.None);
            while (ws.State == WebSocketState.Open)
            {
                //Console.Write("Input message ('exit' to exit): ");
                string msg = Console.ReadLine();
                if (msg == "exit")
                {
                    break;
                }
                ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(msg));
                await ws.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
                ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
                WebSocketReceiveResult result = await ws.ReceiveAsync(bytesReceived, CancellationToken.None);
                //Console.WriteLine(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));
                Debug.LogError(Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count));
            }
        }
    }
}
