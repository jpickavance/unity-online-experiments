using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class ExperimentController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void InsertData  (string tableName, 
                                            string tokenId, 
                                            string trialNum,
                                            string movementTime,
                                            string timeData, 
                                            string posxData,
                                            string poszData);
    
    public string tokenId;
    public Text TrialCount;
    public int trial;
    public bool setup;
    public bool moving;
    public Color red;
    public GameObject Target;
    public GameObject StartPoint;
    public MouseMove Cursor;
    public DateTime trialBegin;
    public DateTime trialEnd;
    public TimeSpan movementTime;
    public string timeData;
    public string posxData;
    public string poszData;
    
    public Text gameOver;

    void Awake()
    {
        tokenId = UserInfo.Instance.tokenId.ToString();
        trial = 1;
        setup = true;
        moving = false;
        gameOver.text = "";
    }

    public void TrialEnd()
    {
        trialEnd = DateTime.Now;
        movementTime = trialEnd - trialBegin;
        Target.SetActive(false);
        timeData = String.Join(", ", Cursor.timeList.ToArray());
        posxData = String.Join(", ", Cursor.posxList.ToArray());
        poszData = String.Join(", ", Cursor.poszList.ToArray());
        InsertData ("schoolTrialData",
                    tokenId,
                    trial.ToString(),
                    movementTime.ToString(),
                    timeData,
                    posxData,
                    poszData);                    
        trial += 1;
        Cursor.timeList = new List<string>();
        Cursor.posxList = new List<string>();
        Cursor.poszList = new List<string>();
        timeData = "";
        posxData = "";
        poszData = "";
        if(trial >= 11) 
        {
            GameOver();
            TrialCount.text = "";
        }
        else
        {
            TrialCount.text = "Trial: " + trial.ToString();
            setup = true;
            moving = false;
            StartPoint.GetComponent<MeshRenderer>().material.color = red;
        }

    }

    public void StartTrial()
    {
        trialBegin = DateTime.Now;
        Target.SetActive(true);
        moving = true;
        setup = false;
    }

    public void GameOver()
    {
        gameOver.text = "  Game Over!";
        Target.SetActive(false);
        StartPoint.SetActive(false);
    }
    public void StringCallback (string info)
    {
        string Output = info;
    }

}
