using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public static PlayerEvents current;
    public CanvasGroup followPromptCanvas;

    private void Awake()
    {
        current = this;
    }

    private void Update()
    {
        if (!KattMovement.followingPlayer)
        {
            followPromptCanvas.gameObject.SetActive(true);
            followPromptCanvas.alpha = 1;
        }
        else
        {
            followPromptCanvas.alpha = 0;
            followPromptCanvas.gameObject.SetActive(false);
        }
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
