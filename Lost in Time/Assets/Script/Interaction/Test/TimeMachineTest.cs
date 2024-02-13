using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMachineTest : MonoBehaviour, IInteractable
{
    public Renderer indicator;
    public Material offColor;
    public Material onColor;

    bool partInserted;

    public void Interact()
    {
        PickableObject temp = PlayerPickup.currentPickableObject;

        PlayerPickup.currentPickableObject = null;
        Destroy(temp.gameObject);
        partInserted = true;
    }

    public bool IsInteractable()
    {
        if(!partInserted && PlayerPickup.currentPickableObject != null && PlayerPickup.currentPickableObject.pickupName == "Machine Part")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string PromptText()
    {
        return "Insert Part into Slot";
    }

    // Update is called once per frame
    void Update()
    {
        if (partInserted)
        {
            indicator.material = onColor;
        }
        else
        {
            indicator.material = offColor;
        }
    }
}
