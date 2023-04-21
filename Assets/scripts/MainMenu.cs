using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text GPSStatus, latitudeValue, longtitudeValue, altitudeValue, horizontalAccuracy, timestampValue;

    public void Start()
    {
        StartCoroutine(GPSLoc());
    }

    IEnumerator GPSLoc()
    {
        if (!Input.location.isEnabledByUser)
        {
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait < 1)
        {
            GPSStatus.text = "Time out";
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            GPSStatus.text = "Can not determine location";
            yield break;
        }
        else
        {
            GPSStatus.text = "Running";
            InvokeRepeating("UpdateGPSData", 0.5f, 1f);
        }
    }

    private void UpdateGPSData()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            GPSStatus.text = "Running";
            latitudeValue.text = Input.location.lastData.latitude.ToString();
            longtitudeValue.text = Input.location.lastData.longitude.ToString();
            altitudeValue.text = Input.location.lastData.altitude.ToString();
            horizontalAccuracy.text = Input.location.lastData.horizontalAccuracy.ToString();
            timestampValue.text = Input.location.lastData.timestamp.ToString();
        }
        else
        {
            GPSStatus.text = "Stop";
        }
    }
}
