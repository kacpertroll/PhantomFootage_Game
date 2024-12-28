using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorAnimationController : MonoBehaviour, IInteractableHold
{
    public bool IsLocked;
    public static bool isLocked;
    public string Dialogue;

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

    void Update()
    {
        if(animator.GetBool("IsOpen"))
        {
            IsLocked = false;
        }
    }

    public void Interact()
    {
        if (IsLocked)
        {
            if (DialogueManager.Instance.isMessageDisplaying == false)
            {
                DialogueManager.Instance.CommentMessage(Dialogue);
            }
        }
        else if (animator != null && !IsLocked)
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
