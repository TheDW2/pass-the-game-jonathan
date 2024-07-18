using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ResetZone : MonoBehaviour
{
    [SerializeField]
    private Transform _resetPosition;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_resetPosition.position, Vector3.one);

#if UNITY_EDITOR
        Handles.Label(_resetPosition.position, "<color=red>Reset Position</color>", DebugStyles.DebugLabelStyle);
#endif
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            other.transform.position = _resetPosition.position;
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}