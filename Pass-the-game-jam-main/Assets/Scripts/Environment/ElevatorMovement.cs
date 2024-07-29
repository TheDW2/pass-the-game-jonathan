using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMovement : MonoBehaviour
{
    private bool isOnElevator = false;
    public bool isUp = false;
    private IEnumerator currentCoroutine;

    public Vector3 floor = new Vector3(3.7f,6.1f,0.06f);
    public Vector3 secondFloor = new Vector3(3.7f,12.0f,0.6f);
    public float elevatorSpeed = 5.0f;


    void Start()
    {
        currentCoroutine = LerpPosition(floor, elevatorSpeed);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isOnElevator)
        {
            if (isUp)
            {
                CallElevatorDown();
            }
            else
            {
                CallElevatorUp();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOnElevator == false && other.gameObject.tag == "Player")
        {
            isOnElevator = true;
        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (isOnElevator == true && other.gameObject.tag == "Player")
        {
            isOnElevator = false;
        }
    }

    public void CallElevatorDown()
    {
        StopCoroutine(currentCoroutine);
        currentCoroutine = LerpPosition(floor, elevatorSpeed);
        StartCoroutine(currentCoroutine);
        isUp = false;
    }

    public void CallElevatorUp()
    {
        StopCoroutine(currentCoroutine);
        currentCoroutine = LerpPosition(secondFloor, elevatorSpeed);
        StartCoroutine(currentCoroutine);
        isUp = true;
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
