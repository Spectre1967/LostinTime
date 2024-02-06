using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorSwitcher : MonoBehaviour, IInteractable
{
    private Material material;
    bool state = true;

    public void Interact()
    {
        NewColor();
    }

    public string PromptText()
    {
        return "Change Color";
    }

    private void Start()
    {
        material = gameObject.GetComponent<Renderer>().material;
    }

    void NewColor()
    {
        state = false;
        Invoke(nameof(MakeInteractable), 1);

        Debug.Log("Changing color...");
        var randomColor = Random.ColorHSV();

        if (material.GetColor("_Color") != randomColor)
        {
            material.SetColor("_Color", randomColor);
            material.SetColor("_EmissionColor", randomColor);
        }

        Debug.Log("Color change done!");

    }

    void MakeInteractable()
    {
        state = true;
    }

    public bool IsInteractable()
    {
        if (state == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
