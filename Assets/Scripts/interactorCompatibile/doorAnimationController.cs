using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorAnimationController : MonoBehaviour, IInteractableHold
{
    public bool IsLocked = false;
    public static bool isLocked = false;

    private bool isOpen = false;
    private bool isSlight = false;
    private Animator animator;

    void Start()
    {
        isLocked = IsLocked;
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Brak komponentu Animator na obiekcie.");
        }
    }

    public void Interact()
    {
        if (isLocked)
        {
            if (DialogueManager.Instance.isMessageDisplaying == false)
            {
                DialogueManager.Instance.CommentMessage("It's locked. No going thorugh without code.");
            }
        }
        else if (animator != null && !isLocked)
        {
            if (isSlight)
            {
                isSlight = !isSlight;
                animator.SetBool("IsSlight", isSlight);

                isOpen = !isOpen;
                animator.SetBool("IsOpen", isOpen);
            }
            else
            {
                isOpen = !isOpen;
                animator.SetBool("IsOpen", isOpen);
            }
            
        }
    }

    public void InteractHold()
    {
        if (animator != null)
        {
            if (!isSlight && !isOpen)
            {
                isSlight = true;
                animator.SetBool("IsSlight", isSlight);
            }
            
        }
    }
}
