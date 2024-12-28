using UnityEngine;

public class keyPad_buttonCheck : MonoBehaviour, IInteractableHold
{
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        gameObject.tag = "Interactable";
    }

    public void Interact()
    {
        if (InventoryManager.Inventory.keypadKey == false)
        {
            DialogueManager.Instance.CommentMessage("One of the buttons is missing... I hope there's a spare one around here.");
        }
        else
        {
            animator.SetBool("keyInstal", true);
        }
    }

    public void InteractHold()
    {

    }

    public void ColliderGone()
    {
        gameObject.tag = "Untagged";
        DialogueManager.Instance.CommentMessage("Okay... Now the code.");
    }
}
