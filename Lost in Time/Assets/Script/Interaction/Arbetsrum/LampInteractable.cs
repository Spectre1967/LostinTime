using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LampInteractable : MonoBehaviour, IInteractable
{
    public Light actual_light;
    public TMP_Text time_text;
    bool lightBulbInserted;

    public void Interact()
    {
        PickableObject temp = PlayerPickup.currentPickableObject;

        PlayerPickup.currentPickableObject = null;
        Destroy(temp.gameObject);
        lightBulbInserted = true;
        actual_light.gameObject.SetActive(true);
    }

    public bool IsInteractable()
    {
        if (!lightBulbInserted && PlayerPickup.currentPickableObject != null && PlayerPickup.currentPickableObject.pickupName == "Light Bulb")
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
        return "Screw in Light Bulb";
    }

    // Update is called once per frame
    void Update()
    {
        if (lightBulbInserted)
        {
            time_text.color = Color.Lerp(time_text.color, Color.gray, Time.deltaTime);
        }
    }
}
