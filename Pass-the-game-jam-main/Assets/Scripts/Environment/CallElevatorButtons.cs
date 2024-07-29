using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallElevatorButtons : MonoBehaviour
{

    [SerializeField] private ElevatorMovement elevatorManager;

    private bool isNearButton = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isNearButton)
        {
            if (elevatorManager.isUp)
            {
                elevatorManager.CallElevatorDown();
            }
            else
            {
                elevatorManager.CallElevatorUp();
            }
        }
    }

        private void OnTriggerEnter(Collider other)
    {
        if (isNearButton == false && other.gameObject.tag == "Player")
        {
            isNearButton = true;
        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (isNearButton == true && other.gameObject.tag == "Player")
        {
            isNearButton = false;
        }
    }
}
