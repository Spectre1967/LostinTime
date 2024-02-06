using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

interface IInteractable
{
    public void Interact();
    public string PromptText();
    public bool IsInteractable();
}

public class PlayerInteractor : MonoBehaviour
{
    public Transform interactorSource;
    public float interactionRange;
    [Space]
    public KeyCode interactionKey;

    [Space]

    public Sprite interactionKeyRegular;
    public Sprite interactionKeyHeld;
    [Space]
    public TMP_Text promptText;
    public Image promptImage;
    public CanvasGroup promptCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactionKey))
        {
            PerformInteraction();
        }

        if (Input.GetKey(interactionKey))
        {
            promptImage.sprite = interactionKeyHeld;
        }
        else
        {
            promptImage.sprite = interactionKeyRegular;
        }

        CheckForInteractions();
    }

    void PerformInteraction()
    {
        Ray r = new Ray(interactorSource.position, interactorSource.forward);
        if(Physics.Raycast(r, out RaycastHit hitInfo, interactionRange))
        {
            if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                if(interactObj.IsInteractable())
                    interactObj.Interact();
            }
        }
    }

    void CheckForInteractions()
    {
        Ray r = new Ray(interactorSource.position, interactorSource.forward);
        if (Physics.Raycast(r, out RaycastHit hitInfo, interactionRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                if (interactObj.IsInteractable())
                {
                    promptText.text = interactObj.PromptText();
                    promptCanvas.alpha = 1;
                }
                else
                {
                    promptText.text = "";
                    promptCanvas.alpha = 0;
                }
            }
            else
            {
                promptText.text = "";
                promptCanvas.alpha = 0;
            }
        }
        else
        {
            promptText.text = "";
            promptCanvas.alpha = 0;
        }
    }
}
