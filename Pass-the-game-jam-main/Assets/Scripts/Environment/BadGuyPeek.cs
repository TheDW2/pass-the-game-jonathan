using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuyPeek : MonoBehaviour
{
    [SerializeField]
    private CameraFollow cameraManager;
    [SerializeField]
    private GameObject peekPoint;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cameraManager.SetTarget(peekPoint.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cameraManager.SetTarget("Player");
        }
    }
}
