using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class PlayerPickup : MonoBehaviour
{
    public static PickableObject currentPickableObject;
    public Transform pickedUpPosition;
    public Transform dropDirection;
    [Space]
    [Header("UI")]
    public CanvasGroup dropPromptGroup;
    public TMP_Text dropPromptText;

    private void Start()
    {
        PlayerEvents.current.onPickupObject += Pickup;
        PlayerEvents.current.onDropObject += Drop;
    }

    private void OnDestroy()
    {
        PlayerEvents.current.onPickupObject -= Pickup;
        PlayerEvents.current.onDropObject -= Drop;
    }

    void Pickup(PickableObject pickable)
    {
        if(currentPickableObject != null)
        {
            PlayerEvents.current.DropObject(currentPickableObject);
        }

        currentPickableObject = pickable;
    }

    void Drop(PickableObject pickable)
    {
        if (pickable == currentPickableObject)
        {
            if (Physics.SphereCast(dropDirection.position, currentPickableObject.maxSizeSphereRadius, dropDirection.forward, out RaycastHit hitInfo))
            {
                currentPickableObject.transform.position = hitInfo.point + new Vector3(0, currentPickableObject.maxSizeSphereRadius);
            }
            currentPickableObject = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPickableObject != null)
        {
            if (currentPickableObject.pickupName == "")
            {
                dropPromptText.text = "Drop";
            }
            else {
                dropPromptText.text = "Drop " + currentPickableObject.pickupName;
            }
            dropPromptGroup.alpha = 1;

            currentPickableObject.transform.position = Vector3.Lerp(currentPickableObject.transform.position, pickedUpPosition.position + currentPickableObject.offsetPosition, Time.deltaTime * 50);
            currentPickableObject.transform.rotation = pickedUpPosition.rotation * currentPickableObject.offsetRotation;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                PlayerEvents.current.DropObject(currentPickableObject);
            }
        }
        else
        {
            dropPromptGroup.alpha = 0;
        }
    }
}
