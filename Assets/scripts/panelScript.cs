using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class panelScript : MonoBehaviour
{
    public GameObject _profile, _GPS, _feedback, _menu, _auth, _regist, _send;
    public Animator _anim;

    public void CheckTog()
    {
        
    }

    public void RegistButton()
    {
        _auth.SetActive(false);
        _regist.SetActive(true);
    }

    public void AuthButton()
    {
        _regist.SetActive(false);
        _auth.SetActive(true);
    }

    public void StartButton()
    {
        _profile.SetActive(true);
        _GPS.SetActive(false);
        _feedback.SetActive(false);
        _auth.SetActive(false);
        _regist.SetActive(false);
        _send.SetActive(false);
    }

    public void ProfileButton()
    {
        _profile.SetActive(true);
        _anim.SetBool("Click", false);
        _GPS.SetActive(false);
        _feedback.SetActive(false);
        _send.SetActive(false);
    }

    public void SendButton()
    {
        _send.SetActive(true);
        _anim.SetBool("Click", false);
        _feedback.SetActive(false);
        _profile.SetActive(false);
        _GPS.SetActive(false);
    }

    public void GPSButton()
    {
        _GPS.SetActive(true);
        _send.SetActive(false);
        _anim.SetBool("Click", false);
        _feedback.SetActive(false);
        _profile.SetActive(false);
    }

    public void FeedbackButton()
    {
        _feedback.SetActive(true);
        _anim.SetBool("Click", false);
        _profile.SetActive(false);
        _GPS.SetActive(false);
        _send.SetActive(false);
    }

    public void MenuButton()
    {
        //if(_profile.activeSelf == true || _map.activeSelf == true || _feedback.activeSelf == true)
        //{
        //    _profile.SetActive(false);
        //    _map.SetActive(false);
        //    _feedback.SetActive(false);
        //}
        //_menu.SetActive(true);

        _anim.SetBool("Click", true);
    }


}
