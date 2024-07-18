using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SupplyBoxSpawner : MonoBehaviour
{
    //This is adapted from the Itemspawner script 
    [Tooltip("Stores the Box spawned by the spawner")]
    [SerializeField]
    private GameObject BoxPrefab;

    [SerializeField]
    [Tooltip("Stores the spawn point of the box")]
    private Transform spawnPointbox;

    private GameObject box; //stores the box set this to the item you want to spawn 

//TAKE NOTE items should have several properties(I figured this out the hard way) this being, a rigidbody, the Outline script, the type script(in this case BoxType) 
//and finally Holdable Object script - Serhistorybuff

    private void Start()
    {
        //a box will spawn on start
        SpawnBox();
    }

    private void Update()
    {
        //boxes will spawn when there is no box in the world(barring those on spwaners)
        if (box == null)
        {
            SpawnBox();
        }
    }

    private void OnDrawGizmos()
    {
        //this draws a gizmo to show where the box will spawn
        Gizmos.color = Color.green;
        if (spawnPointbox != null)
        {
            Gizmos.DrawWireCube(spawnPointbox.position, new Vector3(1, 1, 1));
        }

#if UNITY_EDITOR
        if (BoxPrefab)
        {
            Handles.Label(spawnPointbox.position, "<color=green>Supply Box Spawnpoint</color>",
                DebugStyles.DebugLabelStyle);
        }
#endif
    }

    private void SpawnBox()
    {
        //this spawns a box
        GameObject obj = Instantiate(BoxPrefab, spawnPointbox.position, spawnPointbox.rotation);
        box = obj;
    }
}