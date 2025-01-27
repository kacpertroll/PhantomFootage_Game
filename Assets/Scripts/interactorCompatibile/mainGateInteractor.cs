using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Experimental.GlobalIllumination;

public class mainGateInteractor : MonoBehaviour, IInteractableHold
{
    public Animator animator;
    public AudioSource gateClosed;
    public AudioSource gateOpen;

    void Start()
    {
        if (animator == null)
        {
            Debug.LogWarning("Brak komponentu Animator na obiekcie.");
        }
    }

    public void Interact()
    {
        if (animator != null)
        {
            if (InventoryManager.Inventory.mainGateCard == false)
            {
                if (DialogueManager.Instance.isMessageDisplaying == false) // !!WA¯NE!! ¯eby zapobiedz zapêtlaniu dialogu trzeba u¿yæ tego ifa!
                {
                    gateClosed.Play();
                    DialogueManager.Instance.CommentMessage("Closed. That might complicate matters.");
                    DialogueManager.Instance.CommentDelayMessage("Maybe there's something around here that could help.");
                }
            }
            if (InventoryManager.Inventory.mainGateCard == true && animator.GetBool("open") == false)
            {
                gateOpen.PlayDelayed(1);
                objectiveHandler.ObjectivePopup("Access Card used");
                animator.SetBool("open", true);
                DialogueManager.Instance.CommentMessage("That checks out.");
                transform.gameObject.tag = "Untagged";
            }
        }
    }

    public void InteractHold()
    {

    }
}
