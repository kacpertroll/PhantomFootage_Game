using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vanDoors : MonoBehaviour, IInteractableHold
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Brak komponentu Animator na obiekcie.");
        }
    }

    public void Interact()
    {
        if (animator != null)
        {
            animator.SetBool("doorOpen", true);
            GetComponent<Collider>().enabled = false;
        }
    }

    public void InteractHold()
    {
        
    }
}
