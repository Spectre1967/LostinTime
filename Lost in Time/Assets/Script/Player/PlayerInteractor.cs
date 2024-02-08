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
    public Camera playerCamera;
    public float interactionRange;
    [Space]
    public KeyCode interactionKey;
    public float holdTime = 0.5f;

    [Space]

    public Sprite interactionKeyRegular;
    public Sprite interactionKeyHeld;
    [Space]
    public RectTransform promptUI;
    public TMP_Text promptText;
    public Image promptImage;
    public Image promptFill;
    public CanvasGroup promptCanvas;

    float interactionTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(interactionKey))
        {
            promptImage.sprite = interactionKeyHeld;
            PerformInteraction();

            promptFill.fillAmount = Mathf.Clamp(interactionTimer / holdTime, 0, 1);

        }
        else
        {
            promptImage.sprite = interactionKeyRegular;
            interactionTimer = 0;
            promptFill.fillAmount = 0;
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
                if (interactObj.IsInteractable())
                {
                    interactionTimer += Time.deltaTime;

                    if(interactionTimer >= holdTime)
                        interactObj.Interact();
                }
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
                    Vector3 screenPos = playerCamera.WorldToScreenPoint(hitInfo.transform.position);
                    promptUI.anchoredPosition = screenPos;
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
