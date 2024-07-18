using System;
using System.Collections.Generic;
using UnityEngine;

public class HoldableObject : MonoBehaviour
{
    public static readonly HashSet<HoldableObject> Objects = new();

    public event Action OnPickedUp;

    private void Awake()
    {
        Objects.Add(this);
    }

    private void OnDestroy()
    {
        Objects.Remove(this);
    }

    public void PickUp()
    {
        OnPickedUp?.Invoke();
    }
}