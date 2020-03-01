using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

//[Serializable]
//public class User
//{
//    public string Id;
//    public string Name;
//    public string Username;
//    public string Email;
//}

[Serializable]
public class Todo
{
    public int Userid;
    public int Id;
    public string Title;
    public bool Completed;

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("ID: ");
        sb.Append(Id.ToString());
        sb.Append(", USER ID: ");
        sb.Append(Userid.ToString());
        sb.Append(", TITLE: ");
        sb.Append(Title);
        sb.Append(", COMPLETED: ");
        sb.Append(Completed.ToString());
        return sb.ToString();
    }
}

public class DataAsyncController : MonoBehaviour
{
    readonly string USERS_URL = "https://jsonplaceholder.typicode.com/users";
    readonly string TODOS_URL = "https://jsonplaceholder.typicode.com/todos";

    //    async Task<User[]> FetchUsers()
    //    {
    //        var www = await new WWW(USERS_URL);
    //        if (!string.IsNullOrEmpty(www.error))
    //        {
    //            throw new Exception();
    //        }
    //        var json = www.text;
    //        //var userRaws = UnityEngine.JsonUtility.JsonHelper.getJsonArray<UserRaw>(json);
    //        //return userRaws.Select(userRaw => new User(userRaw)).ToArray();
    //        //return JsonConvert.SerializeObject()

    //        var result = JsonConvert.DeserializeObject<>
    //    }

    async Task<Todo[]> FetchTodos()
    {
        var www = await new WWW(TODOS_URL);

        if (!string.IsNullOrEmpty(www.error))
        {
            throw new Exception();
        }
        var json = www.text;
    //var todosRaws = JsonHelper.getJsonArray<TodoRaw>(json);
    //return todosRaws.Select(todoRaw => new Todo(todoRaw)).ToArray();

        List<Todo> result = JsonConvert.DeserializeObject<List<Todo>>(json);
        return result.ToArray();
    }

    async void Start()
    {
        try
        {
            //var users = await FetchUsers();
            var todos = await FetchTodos();

            //foreach (User user in users)
            //{
            //    Debug.Log(user.Name);
            //}
            foreach (Todo todo in todos)
            {
                Debug.LogError(todo.ToString());
            }
        }
        catch
        {
            Debug.LogError("An error occurred");
        }
    }
}