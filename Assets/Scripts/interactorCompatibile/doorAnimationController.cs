using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorAnimationController : MonoBehaviour, IInteractableHold
{
    //public bool IsLocked;
    public bool isLocked;
    public string Dialogue;

    private bool isOpen = false;
    private bool isSlight = false;
    private Animator animator;

    void Start()
    {
        //isLocked = IsLocked;
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
            isLocked = false;
        }
    }

    public void Interact()
    {
        if (isLocked)
        {
            if (DialogueManager.Instance.isMessageDisplaying == false)
            {
                animator.SetBool("LockedPlay", true);
                if (GetComponent<AudioSource>() != null)
                {
                    GetComponent<AudioSource>().Play();
                }
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

    public void DoorLocked()
    {
        animator.SetBool("LockedPlay", false);
        DialogueManager.Instance.CommentMessage(Dialogue);
    }
}
