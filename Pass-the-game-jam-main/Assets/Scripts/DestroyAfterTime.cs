using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    private float _timeToDestroy = 1f;

    private void Start()
    {
        Destroy(gameObject, _timeToDestroy);
    }
}