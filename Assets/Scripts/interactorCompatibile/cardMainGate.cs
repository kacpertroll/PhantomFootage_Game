using UnityEngine;

public class cardMainGate : MonoBehaviour, IInteractableHold
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject GameObject;

    public void Interact()
    {
        if (DialogueManager.Instance.isMessageDisplaying == false)
        {
            GameObject.SetActive(false);
            InventoryManager.Inventory.mainGateCard = true;
            DialogueManager.Instance.CommentMessage("That should work.");
        }
    }

    public void InteractHold()
    {

    }
}
