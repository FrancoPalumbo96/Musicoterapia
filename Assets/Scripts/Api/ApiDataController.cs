using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ApiDataController : MonoBehaviour
{
    private String url = "https://musicoterapia-api.herokuapp.com";

    private static ApiDataController _instance;

    public EventHandler<MyCustomArguments> IdEvent;
    
    public static ApiDataController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ApiDataController>();
            }

            return _instance;
        }
    }

    public void updateUserDataPOST(User user)
    {
        StartCoroutine(UpdateUserDataCoroutine(user));
    }
    
    /*
     * Metodo para hacer POST a la api con el tiempo que paso un usuario con un video y una cancion.
     */
    public void updateUserDataPOST(String id, String video, String music, int time)
    {
        Record record = new Record()
        {
            video = video,
            music = music,
            time = time
        };
        StartCoroutine(UpdateUserDataCoroutine(new User()
        {
            id = id,
            data = record
        }));
    }

    /*
     *Solo creo al usuario la primera vez que se llama y guarda el ID en PlayerPrefs.
     */
    public void createUser(String name)
    {
        String id = PlayerPrefs.GetString(name, "nf");
        if (id == "nf")
        {
            createUserGET(name);
        }
    }
    
    

    /*
     * Metodo para hacer un GET y crear un nuevo usuario. Este metodo debe llamarse una unica vez.
     */
    private void createUserGET(String userName)
    {
        StartCoroutine(CreateUserGetCoroutine(userName));
    }
    


    
    IEnumerator UpdateUserDataCoroutine(User userData)
    {
        var jsonData = JsonUtility.ToJson(userData);
        using (UnityWebRequest www = UnityWebRequest.Post(url + "/updateUserData", jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonData));
            yield return www.SendWebRequest();
            Debug.Log("POST Finished");
        }
    }
    
    public IEnumerator CreateUserGetCoroutine(String userName)
    {
        //isGetCoroutineRunning = true;
        using (UnityWebRequest www = UnityWebRequest.Get(url + "/createUser/" + userName))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError)
            {
                Debug.Log(www.error);
                //isGetCoroutineRunning = false;
            }
            else
            {
                if (www.isDone)
                {
                    // handle the result
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);

                    var userAndId = JsonUtility.FromJson<UserAndId>(result);
                    
                    yield return userAndId;
                    //isGetCoroutineRunning = false;

                    /*//Trigger event
                    MyCustomArguments myCustomArguments = new MyCustomArguments();
                    myCustomArguments.id = userAndId.id;
                    IdEvent.Invoke(this, myCustomArguments);*/
                    PlayerPrefs.SetString("ID", userAndId.id);
                }
                else
                {
                    yield return "fail";
                    //isGetCoroutineRunning = false;
                    Debug.Log("Error! data couldn't get.");
                }
            }
        }
    }

    
   

    [Serializable]
    public class User
    {
        public String id;
        public Record data;
    }
    
    [Serializable]
    public class Record
    {
        public String video;
        public String music;
        //Time in seconds
        public int time;
    }
    
    [Serializable]
    private class UserAndId
    {
        public String user;
        public String id;
    }

    public class MyCustomArguments: EventArgs
    {
        public String id { get; set; }
    }

}
