using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class FoodMaker : MonoBehaviour
{
    //Goodluck y'all - Serhistorybuff

    /// <summary>
    ///     Dev info
    /// </summary>
    [SerializeField]
    [TextArea]
    private string Description;

    /// <summary>
    ///     the location of the maker assign the maker to this Transform (it is needed to check the distance away from the
    ///     player)
    /// </summary>
    [SerializeField]
    private Transform interactPoint;

    /// <summary>
    ///     the rawfood needed to operate the foodmaker
    /// </summary>
    [SerializeField]
    private ResourceManager.RawFoodTypes usedRawFoodType;

    /// <summary>
    ///     what kind of food maker this is(note the name determines functionality so make sure it is named correctly)
    /// </summary>
    [SerializeField]
    private ResourceManager.PreparedFoodTypes preparedFoodType;

    /// <summary>
    ///     How much prepared food is automatically created at start time
    /// </summary>
    [SerializeField]
    private int initialPreparedFoodAmount;

    /// <summary>
    ///     How much raw food is provided by a single supply box?
    /// </summary>
    [SerializeField]
    private int resupplyAmount;

    /// <summary>
    ///     Player object
    /// </summary>
    [SerializeField]
    private GameObject player;

    /// <summary>
    ///     how far away the player can access the maker
    /// </summary>
    [SerializeField]
    private float workDistance;

    //These variables track how many foods were made total and in specific categories, this is for debugging purposes 
    [Header("Debugging (Do not assign)")]

    [SerializeField]
    private int foodPreparedCount;

    private void Start()
    {
        // Create some prepared food to start off with
        ResourceManager.Instance.ChangePreparedFoodAmount(preparedFoodType, initialPreparedFoodAmount);
    }

    private void Update()
    {
        //this fuction checks if the player presses E near a food maker and then decides to make food based on if they are close enough(and if they have enough rawfood) 
        //It adds food by activating a function in ItemSpawner which increases the food by the amount made by the specificed maker
        if (Input.GetKeyDown(KeyCode.E))
        {
            float d = (player.transform.position - interactPoint.transform.position).sqrMagnitude;
            if (d < workDistance * workDistance)
            {
                int rawFoodAmount = ResourceManager.Instance.GetRawFoodAmount(usedRawFoodType);

                if (rawFoodAmount > 0)
                {
                    // Prepare the food; consume one raw food and produce one prepared food
                    ResourceManager.Instance.ChangeRawFoodAmount(usedRawFoodType, -1);
                    ResourceManager.Instance.ChangePreparedFoodAmount(preparedFoodType, 1);
                    foodPreparedCount++;
                }
                else
                {
                    Debug.Log("Not Enough Raw Food Go Grab a box from outside");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        //this draws a gizmo to show where the food maker and its interaction range is
        Gizmos.color = Color.red;
        if (interactPoint != null)
        {
            Gizmos.DrawWireSphere(interactPoint.position, workDistance);
        }

#if UNITY_EDITOR
        Handles.Label(interactPoint.position,
            $"<color=red>{gameObject.name}: {usedRawFoodType} -> {preparedFoodType}</color>",
            DebugStyles.DebugLabelStyle);
#endif
    }

    public void Restock()
    {
        ResourceManager.Instance.ChangeRawFoodAmount(usedRawFoodType, resupplyAmount);
    }
}