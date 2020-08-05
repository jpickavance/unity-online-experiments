using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public AudioClip collectSound;
    public ExperimentController experimentController;
    
    void OnTriggerEnter(Collider other)
    {
        if(experimentController.moving)
        {
            if(other.tag == "cursor")
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position, 1.0f);
                experimentController.TrialEnd();
            }
        }
    }
}

