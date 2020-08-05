using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    //reference to camera we project from
    public Camera projectionCamera;
    public ExperimentController experimentController;
    public TimeSpan timer;
    public List<string> timeList;
    public List<string> posxList;
    public List<string> poszList;

    // Update is called once per frame
    void Update()
    {
        //Mouse position in pixels
        Vector3 mousePos = Input.mousePosition;
        //Convert to world position(i.e. metres/unity units)
        Vector3 worldPos = projectionCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        //fix y position to original pos
        worldPos.y = transform.position.y;
        //update position of object based on calculated world pos
        transform.position = worldPos;

        if(experimentController.moving == true)
        {
            timer = DateTime.Now - experimentController.trialBegin;
            string timeOutput = string.Format   ("{0}{1:000}",
                                                (int)timer.TotalSeconds,
                                                timer.Milliseconds);
            string posxOutput = transform.position.x.ToString();
            string poszOutput = transform.position.z.ToString();
            timeList.Add(timeOutput);
            posxList.Add(posxOutput);
            poszList.Add(poszOutput);
        }
    }
}
