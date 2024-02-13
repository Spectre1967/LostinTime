using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public static PlayerEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<PickableObject> onPickupObject;
    public void PickUpObject(PickableObject pickable)
    {
        onPickupObject?.Invoke(pickable);
    }

    public event Action<PickableObject> onDropObject;
    public void DropObject(PickableObject pickable)
    {
        onDropObject?.Invoke(pickable);
    }
}
