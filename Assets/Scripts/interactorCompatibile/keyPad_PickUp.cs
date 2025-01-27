using UnityEngine;

public class keyPad_PickUp : MonoBehaviour, IInteractableHold
{
    void Start()
    {
        gameObject.tag = "Interactable";
    }

    public void Interact()
    {
        InventoryManager.Inventory.keypadKey = true;
        objectiveHandler.ObjectivePopup("Missing Button added to Inventory");
        Destroy(gameObject);
    }

    public void InteractHold()
    {

    }
}
