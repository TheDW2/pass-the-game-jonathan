using TMPro;
using UnityEngine;

/// <summary>
///     Simple component that updates the text to display the current amount of raw food
/// </summary>
public class RawFoodAmountText : MonoBehaviour
{
    [SerializeField]
    private ResourceManager.RawFoodTypes foodType;

    [SerializeField]
    private TMP_Text text;

    private void Start()
    {
        ResourceManager.Instance.OnRawFoodAmountChanged += OnRawFoodAmountChanged;
    }

    private void OnDestroy()
    {
        ResourceManager.Instance.OnRawFoodAmountChanged -= OnRawFoodAmountChanged;
    }

    private void OnRawFoodAmountChanged(ResourceManager.RawFoodTypes type, int newAmount)
    {
        if (type == foodType)
        {
            text.text = newAmount.ToString();
        }
    }
}