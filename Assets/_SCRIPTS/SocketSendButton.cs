using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SocketSendButton : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public SocketAsyncController socketController;

    public void OnButtonClick ()
    {
        socketController.DoSend(tmpText.text);
    }
}
