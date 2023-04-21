using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

[System.Serializable]
public class UserData
{
    public Player playerData;
    public Error error;
}

[System.Serializable]
public class Error
{
    public string errorText;
    public bool isError;
}

[System.Serializable]
public class Player
{
    public string type;
    public string dist;
    public string street;
    public int home_num;
    public DateTime date = DateTime.Now;
    public float coordinatex;
    public float coordinatey;
    public string comment;

    public Player()
    {

    }

    public Player(string settype, string setdist, string setstreet, int sethome, DateTime setdate, float setcoordinatex, float setcoordinatey, string comm)
    {
        type = settype;
        dist = setdist;
        street = setstreet;
        home_num = sethome;
        date = setdate;
        coordinatex = setcoordinatex;
        coordinatey = setcoordinatey;
        comment = comm;
    }

    public void SetTypeOfCrime(string crimetype) => type = crimetype;
    public void SetDistName(string namedist) => dist = namedist;
    public void SetStreetName(string namestreet) => street = namestreet;
    public void SetHomeNum(int numhome) => home_num = numhome;
    public void SetDateOgCrime(DateTime crimedate) => date = crimedate;
    public void SetCoordinatesOfCrimex(float crimecoordx) => coordinatex = crimecoordx;
    public void SetCoordinatesOfCrimey(float crimecoordy) => coordinatey = crimecoordy;
    public void SetComment(string com) => comment = com;
}


public class WebScript : MonoBehaviour
{
    public static UserData userData = new UserData();

    public string GetUserData(UserData data)
    {
        return JsonUtility.ToJson(data);
    }

    public UserData SetUserData(string data)
    {
        return JsonUtility.FromJson<UserData>(data);
    }

    public void Start()
    {
        userData.error = new Error() { errorText = "text", isError= true };
        userData.playerData = new Player("kill", "Kuib", "HZ", 5, new DateTime(2022, 02, 02), 0, 0, "Комментарий");
    }

    public void Login(string login, string password)
    {
        StopAllCoroutines();
        Logging(login, password);
    }

    public void Registration(string login, string password, string name, string sername, string patronymic)
    {
        StopAllCoroutines();
        Registering(login, password, name, sername, patronymic);
    }

    public void Logging(string login, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", "logging");
        form.AddField("login", login);
        form.AddField("password", password);
        StartCoroutine(SendData(form));
    }

    public void Registering(string login, string password, string name, string sername, string patronymic)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", "register");
        form.AddField("login", login);
        form.AddField("password", password);
        form.AddField("sername", sername);
        form.AddField("name", name);
        form.AddField("patronymic", patronymic);
        StartCoroutine(SendData(form));
    }

    IEnumerator SendData(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post("http://kursach/logreg.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                userData = SetUserData(www.downloadHandler.text);
            }
        }
    }
}
