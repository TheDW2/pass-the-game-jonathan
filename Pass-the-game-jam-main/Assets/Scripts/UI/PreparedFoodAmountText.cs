using TMPro;
using UnityEngine;

/// <summary>
///     Simple component that updates the text to display the current amount of prepared food
/// </summary>
public class PreparedFoodAmountText : MonoBehaviour
{
    [SerializeField]
    private ResourceManager.PreparedFoodTypes foodType;

    [SerializeField]
    private TMP_Text text;

    private void Start()
    {
        ResourceManager.Instance.OnPreparedFoodAmountChanged += OnPreparedFoodAmountChanged;
    }

    private void OnDestroy()
    {
        ResourceManager.Instance.OnPreparedFoodAmountChanged -= OnPreparedFoodAmountChanged;
    }

    private void OnPreparedFoodAmountChanged(ResourceManager.PreparedFoodTypes type, int newAmount)
    {
        if (type == foodType)
        {
            text.text = newAmount.ToString();
        }
    }
}