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
        Destroy(gameObject);
    }

    public void InteractHold()
    {

    }
}
