using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour, IInteractable
{
    public string pickupName;
    /// <summary>
    /// The max size of the object if it were a sphere. Used when calculating where to drop the object.
    /// </summary>
    public float maxSizeSphereRadius;
    public Vector3 offsetPosition;
    public Quaternion offsetRotation;
    [Space]
    public bool isPickable = true;

    public Rigidbody rb { get; private set; }
    public Collider c { get; private set; }

    // INTERACTABLE INTERFACE
    public void Interact()
    {
        isPickable = false;
        c.enabled = false;
        rb.useGravity = false;
        rb.freezeRotation = true;
        PlayerEvents.current.PickUpObject(this);
    }

    public bool IsInteractable()
    {
        return isPickable;
    }

    public string PromptText()
    {
        if (pickupName == "")
            return "Pick Up";

        return "Pick Up " + pickupName;
    }

    void OnDrop(PickableObject pickable)
    {
        if(pickable == this)
        {
            isPickable = true;
            c.enabled = true;
            rb.useGravity = true;
            rb.freezeRotation = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        c = GetComponent<Collider>();

        PlayerEvents.current.onDropObject += OnDrop;
    }

    private void OnDestroy()
    {
        PlayerEvents.current.onDropObject -= OnDrop;
    }
}
