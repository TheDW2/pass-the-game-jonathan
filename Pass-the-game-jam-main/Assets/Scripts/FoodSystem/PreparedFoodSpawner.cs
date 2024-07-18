using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
///     Takes prepared food from the ResourceManager and spawns it in the world
/// </summary>
public class PreparedFoodSpawner : MonoBehaviour
{
    /// <summary>
    ///     Dev Description
    /// </summary>
    [SerializeField]
    [TextArea]
    private string Description;

    [SerializeField]
    private GameObject ItemPrefab; //stores the item spawned by the spwaner (assign the item you want to spawn to it)

    //TAKE NOTE items should have several properties(I figured this out the hard way) this being, a rigidbody, the Outline script, the type script(in this case FoodType) 
    //and finally Holdable Object script - Serhistorybuff

    /// <summary>
    ///     Location to spawn the item
    /// </summary>
    [SerializeField]
    private Transform spawnPoint;

    /// <summary>
    ///     The type of food to spawn; used to track if we have any of this food left
    /// </summary>
    [SerializeField]
    private ResourceManager.PreparedFoodTypes FoodTypeToSpawn;

    // References the current spawned item, so we know when its been consumed
    private GameObject item;

    private void Update()
    {
        // Spawn food item if there are still food items remaining and the current item has been consumed
        if (item == null)
        {
            int foodRemaining = ResourceManager.Instance.GetPreparedFoodAmount(FoodTypeToSpawn);

            // Use one instance of the food type stored in the ResourceManager, and materialize it into reality
            if (foodRemaining > 0)
            {
                SpawnPreparedFood();
                ResourceManager.Instance.ChangePreparedFoodAmount(FoodTypeToSpawn, -1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a gizmo to show where the item will spawn
        Gizmos.color = Color.red;
        if (spawnPoint != null)
        {
            Gizmos.DrawWireCube(spawnPoint.position, Vector3.one * 0.5f);
        }

#if UNITY_EDITOR
        if (ItemPrefab)
        {
            Handles.Label(spawnPoint.position, $"<color=red>{ItemPrefab.name} Spawn</color>",
                DebugStyles.DebugLabelStyle);
        }
#endif
    }

    /// <summary>
    ///     Spawn the item
    /// </summary>
    private void SpawnPreparedFood()
    {
        GameObject obj = Instantiate(ItemPrefab, spawnPoint.position, spawnPoint.rotation);

        var holdable = obj.GetComponentInChildren<HoldableObject>();
        if (holdable != null)
        {
            holdable.OnPickedUp += OnItemPickedUp;
        }

        item = obj;
    }

    /// <summary>
    ///     Respawn the item when it is picked up
    /// </summary>
    private void OnItemPickedUp()
    {
        var holdable = item.GetComponentInChildren<HoldableObject>();
        if (holdable != null)
        {
            holdable.OnPickedUp -= OnItemPickedUp;
        }

        item = null;
    }
}