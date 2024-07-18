using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//I didn't have enough time to fully write notes out so feel free to ask me about this thing - Serhistorybuff

public class RestockPoint : MonoBehaviour
{
    [TextArea]
    public string Description;

    [Tooltip("Food Maker to restock when a box is delivered")]
    public FoodMaker FoodMaker;

    [SerializeField]
    private SphereCollider triggerCollider;

    public bool PlayerNear { get; private set; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, triggerCollider.radius);
#if UNITY_EDITOR
        if (FoodMaker)
        {
            Handles.Label(transform.position, $"<color=green>Restock Point: {FoodMaker.gameObject.name}</color>",
                DebugStyles.DebugLabelStyle);
        }
#endif
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");
        if (other.CompareTag("Player"))
        {
            PlayerNear = true;
        }
        else if (other.CompareTag("Box"))
        {
            Debug.Log("Box Entered" + other.name);
            Destroy(other.gameObject);
            FoodMaker.Restock();
        }
    }
}