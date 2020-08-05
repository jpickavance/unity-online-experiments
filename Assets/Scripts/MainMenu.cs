using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
        //js headers  
    [DllImport("__Internal")]
    private static extern string ReadData (string tableName,
                                           string token);
    
    [DllImport("__Internal")]
    private static extern string UpdateToken (string tableName,
                                              string token);

    [DllImport("__Internal")]
    private static extern void InsertUser (string tableName,
                                           string token,
                                           string handedness,
                                           string consentTime,
                                           string startTime);
    public InputField TokenField;
    public InputField HandednessField;

    public Text errMessage;
    public GameObject consentForm;
    public DateTime consentTime;
    public DateTime startTime;

    [System.Serializable]
    public class tokenClass
    {
        public string tokenId;
        public bool available;
    }

    public void Awake()
    {
        UserInfo.Instance.consent = false;
        TokenField.Select();
    }

    public void PlayGame() 
    { 
        if(UserInfo.Instance.consent == false)
        {
            errMessage.text = "You must provide your consent before participating in the experiment";
        }
        else if (UserInfo.Instance.consent == true)
        {
            startTime = DateTime.Now;
            ReadData("tokenTable", UserInfo.Instance.tokenId.ToString());
        }
 
    }

    public void ViewConsent()
    {
        consentForm.SetActive(true);
    }
    public void GiveConsent()
    {
        consentTime = DateTime.Now;
        UserInfo.Instance.consent = true;
        errMessage.text = "";
        consentForm.SetActive(false);
    }

    public void RefuseConsent()
    {
        UserInfo.Instance.consent = false;
        consentForm.SetActive(false);
    }
    
    public void GetToken()
    {
        UserInfo.Instance.tokenId = TokenField.text.ToString();
    }

    public void GetHandedness()
    {
        UserInfo.Instance.handedness = HandednessField.text.ToString();
    }

        // This is called in the ReadData() .js function, supplying the requested data as its argument
    public void StringCallback (string reqData)
    {
        tokenClass tokenObject = JsonUtility.FromJson<tokenClass>(reqData);
        if(tokenObject.available == true)
        {

            UpdateToken("tokenTable", 
                        UserInfo.Instance.tokenId.ToString());
            InsertUser("schoolUserData",  UserInfo.Instance.tokenId.ToString(),
                                          UserInfo.Instance.handedness.ToString(),
                                          consentTime.ToString(),
                                          startTime.ToString());
            SceneManager.LoadScene("Experiment");
        }
        else if(tokenObject.available == false)
        {
            errMessage.text = "The token you have entered has already been used.";
        }
    }
    public void ErrorCallback(string error)
        {
            errMessage.text = error;
        }
 
}
