using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Manager that tracks and modifies the food amounts
/// </summary>
public class ResourceManager : MonoBehaviour
{
    public delegate void OnRawFoodAmountEvent(RawFoodTypes type, int newAmount);

    public delegate void OnPreparedFoodAmountEvent(PreparedFoodTypes type, int newAmount);

    /// <summary>
    ///     Enum types for raw food
    /// </summary>
    public enum RawFoodTypes
    {
        Patties,
        Potatoes,
        Icecream
    }

    /// <summary>
    ///     Enum types for cooked food.
    /// </summary>
    public enum PreparedFoodTypes
    {
        Burgers,
        Fries,
        Milkshakes
    }

    public static ResourceManager Instance;

    public event OnRawFoodAmountEvent OnRawFoodAmountChanged;

    public event OnPreparedFoodAmountEvent OnPreparedFoodAmountChanged;

    /// <summary>
    ///     Dictionaries to track the amount of each food type
    /// </summary>
    private readonly Dictionary<RawFoodTypes, int> rawFoodAmounts = new();

    private readonly Dictionary<PreparedFoodTypes, int> cookedFoodAmounts = new();

    private void Awake()
    {
        Instance = this;
    }

    // -20 offset is necessary due to unintended behavior adding 20 to the visual representation of each ingredient. subtracting 20 gives us the accurate number.
    // Fixed the bug, it was due to all 3 machines containing ingredients for the other machines (so 10+10+10 ingredients were added to the text)
    private void Start()
    {
        //patties -= 20;
        //potatoes -= 20;
        //icecream -= 20;
    }

    public string GetPreparedFoodName(PreparedFoodTypes type)
    {
        return type switch
        {
            PreparedFoodTypes.Burgers => "Burgers",
            PreparedFoodTypes.Fries => "Fries",
            PreparedFoodTypes.Milkshakes => "Milkshakes",
            _ => "Unknown"
        };
    }

    public int GetRawFoodAmount(RawFoodTypes type)
    {
        // Try to add the type to the dictionary if it doesn't exist
        rawFoodAmounts.TryAdd(type, 0);

        return rawFoodAmounts[type];
    }

    public int GetPreparedFoodAmount(PreparedFoodTypes type)
    {
        cookedFoodAmounts.TryAdd(type, 0);

        return cookedFoodAmounts[type];
    }

    public void ChangeRawFoodAmount(RawFoodTypes type, int change)
    {
        rawFoodAmounts.TryAdd(type, 0);

        rawFoodAmounts[type] += change;
        rawFoodAmounts[type] = Mathf.Max(rawFoodAmounts[type], 0);

        OnRawFoodAmountChanged?.Invoke(type, rawFoodAmounts[type]);
    }

    public void ChangePreparedFoodAmount(PreparedFoodTypes type, int change)
    {
        cookedFoodAmounts.TryAdd(type, 0);

        cookedFoodAmounts[type] += change;
        cookedFoodAmounts[type] = Mathf.Max(cookedFoodAmounts[type], 0);

        OnPreparedFoodAmountChanged?.Invoke(type, cookedFoodAmounts[type]);
    }
}