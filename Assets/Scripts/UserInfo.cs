using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{
    //Static reference
    public static UserInfo Instance {get; private set;}
    //Data to persist
    public string tokenId;
    public string handedness;
    public bool consent;
    private void Awake()
    {   
        //check if info is null
        if (Instance == null)
        {
            //This instance becomes the single instance available
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
            //Otherwise check if the control instance is not this one
        else
        {
            //In case there is a different instance destroy this one.
            Destroy(gameObject);
        }
    }
}
