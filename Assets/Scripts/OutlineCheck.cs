using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineSelection : MonoBehaviour
{
    private Transform highlight;
    private RaycastHit raycastHit;
    public Animator animator;

    void Update()
    {
        // Podœwietlenie
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        highlight = raycastHit.transform;
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit, 1.5f) && highlight.CompareTag("Interactable")) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
                animator.SetBool("interactable", true);
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    
                }
        }  
        else
        {
            animator.SetBool("interactable", false);
            highlight = null;
        }
    }
}