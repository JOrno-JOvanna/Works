using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WebScript2 : MonoBehaviour
{
    public TMP_Text login, password, reg_sername, reg_name, reg_patronymic, reg_login, reg_password;
    public Text street, num, comm, log_message, reg_message, gpsOut, numofcrimes, fio, info_dist, info_street, info_homenum, info_type, info_comm, info_date, message_body;
    public Dropdown drop_dist, drop_type;
    public string dist, type, gpsData;
    public panelScript panels;
    public bool isUpdating;
    public static string user_id;
    public string[] message;
    string checklist = "entered";
    bool e = false;
    public DateTime dateTime = DateTime.Today;
    MailMessage mailMessage;
    SmtpClient client;

    private void Update()
    {
        if (!isUpdating)
        {
            StartCoroutine(GetLocation());
            isUpdating = !isUpdating;
        }
    }
    IEnumerator GetLocation()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }

        if (!Input.location.isEnabledByUser)
            yield return new WaitForSeconds(10);

        Input.location.Start();

        int maxWait = 3;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            gpsOut.text = "Timed out";
            print("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            gpsOut.text = "Unable to determine device location";
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            gpsData = Input.location.lastData.latitude.ToString() + ";" + Input.location.lastData.longitude.ToString();
            gpsOut.text = "Location: " + Input.location.lastData.latitude + ";" + Input.location.lastData.longitude + ";" + Input.location.lastData.altitude + 100f + ";" + Input.location.lastData.horizontalAccuracy + ";" + Input.location.lastData.timestamp;
            print("Location: " + Input.location.lastData.latitude + ";" + Input.location.lastData.longitude + ";" + Input.location.lastData.altitude + ";" + Input.location.lastData.horizontalAccuracy + ";" + Input.location.lastData.timestamp);
        }

        isUpdating = !isUpdating;
        Input.location.Stop();
    }


    public void Registration()
    {
        WWWForm form = new WWWForm();
        form.AddField("type", "regist");
        form.AddField("login", reg_login.text);
        form.AddField("password", reg_password.text);
        form.AddField("sername", reg_sername.text);
        form.AddField("name", reg_name.text);
        form.AddField("patronymic", reg_patronymic.text);
        StartCoroutine(SendData(form));
    }

    public void LogIn()
    {
        WWWForm form = new WWWForm();
        form.AddField("type", "login");
        form.AddField("login", login.text);
        form.AddField("password", password.text);
        StartCoroutine(SendData(form));
    }

    public void NewCrim() 
    {
        WWWForm form = new WWWForm();
        form.AddField("type", "update");
        form.AddField("user_id", user_id);
        form.AddField("dist", drop_dist.captionText.text);
        form.AddField("street", street.text);
        form.AddField("num", num.text);
        form.AddField("crimtype", drop_type.captionText.text);
        form.AddField("comm", comm.text);
        form.AddField("date", dateTime.ToString());
        form.AddField("geoloc", gpsData);
        StartCoroutine(SendData(form));
    }

    public void DowloadDATA()
    {
        WWWForm form = new WWWForm();
        form.AddField("type", "download");
        form.AddField("user_id", user_id);
        StartCoroutine(SendData(form));

    }

    IEnumerator SendData(WWWForm form)
    {

        using (UnityWebRequest www = UnityWebRequest.Post("http://kursach/new.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                reg_message.text = www.downloadHandler.text;
                Debug.Log(www.downloadHandler.text);
                message = www.downloadHandler.text.Split();
                foreach (string s in message)
                {
                    if (checklist == s)
                    {
                        user_id = www.downloadHandler.text.Replace("entered", "");
                        Debug.Log(user_id);
                        e = true;
                        break;
                    }
                }

                if (e)
                {
                    panels.StartButton();
                }

                message = www.downloadHandler.text.Split(';');
                int i = 0;
                foreach (string s in message)
                {
                    i++;
                    if(i > 1)
                    {
                        fio.text = message[1] + " " + message[2] + " " + message[3];
                        info_dist.text = message[4];
                        info_street.text = message[5];
                        info_homenum.text = message[6];
                        info_type.text = message[7];
                        info_comm.text = message[8];
                        info_date.text = message[9];
                    }
                }
                numofcrimes.text = message[0];
            }

        }
    }

    public void SendFeedback()
    {
        mailMessage.Body = "Проверочное сообщение";
        mailMessage.From = new MailAddress("maksfyodorov01@mail.ru");
        mailMessage.To.Add("maksfyodorov01@mail.ru");
        mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

        client.Host = "smpt.mail.ru";
        client.Port = 587;
        client.Credentials = new NetworkCredential(mailMessage.From.Address, "");
        client.EnableSsl= true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };
        client.Send(mailMessage);
    }
}
