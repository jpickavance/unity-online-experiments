using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPointController : MonoBehaviour
{
    public Color red;
    public Color amber;
    public Color green;
    public ExperimentController experimentController;

    Material material;

    void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    //create a coroutine function that doesn't execute until after 500ms
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.5f);
        material.color = green;
        experimentController.StartTrial();
    }
    void OnTriggerEnter(Collider other)
    {
        if(experimentController.setup == true)
        {
            if(other.tag == "cursor")
            {
                StartCoroutine(Countdown());
                material.color = amber;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (experimentController.moving == false)
        {
            if(other.tag == "cursor")
            {
                material.color = red;
                StopAllCoroutines();
            }
        }
        else
        {
            if(other.tag == "cursor")
            {
                material.color = green;
                StopAllCoroutines();
            }
        }
    }
}
