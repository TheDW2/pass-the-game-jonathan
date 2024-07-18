using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float LerpSpeed;

    PlayerMovement playerMovement;

    Vector3 _offset;
    void Start()
    {
        playerMovement = GameManger.instance.player;
        _offset = transform.position - playerMovement.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + _offset, LerpSpeed * Time.deltaTime);
    }

    public void SetTarget(string name)
    {
        Target = GameObject.Find(name).transform;
    }
    public void SetTarget(Transform transform)
    {
        Target = transform;
    }
    public void LockPlayer()
    {
        GameManger.instance.LockPlayer();
    }
}
