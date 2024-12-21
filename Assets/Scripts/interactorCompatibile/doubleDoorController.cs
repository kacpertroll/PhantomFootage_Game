using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doubleDoorController : MonoBehaviour, IInteractableHold
{
    private bool isOpen = false;
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
                isOpen = !isOpen;
                animator.SetBool("IsOpen", isOpen);
        }
    }

    public void InteractHold()
    {

    }
}
