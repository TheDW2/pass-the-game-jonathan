using UnityEngine;

/// <summary>
///     Used to tag a gameobject as a physical prepared food object
/// </summary>
public class PreparedFoodComponent : MonoBehaviour
{
    // This script can be used to manage the types of food
    [SerializeField]
    public ResourceManager.PreparedFoodTypes FoodComponent;
}