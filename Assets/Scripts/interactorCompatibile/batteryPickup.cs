using Unity.Collections;
using UnityEngine;

public class batteryPickup : MonoBehaviour, IInteractableHold
{
    void Start()
    {
        transform.gameObject.tag = "Interactable"; // Na start ¿eby siê nie bawiæ, ka¿dy obiekt z tym skryptem zmienia tag na Interactable, ¿eby reagowa³ na Outline.
    }

    public void Interact()
    {
        if (!DialogueManager.Instance.isMessageDisplaying)
        {
            DialogueManager.Instance.CommentMessage("That'll be useful");
            CameraManager.batteryPrec = 1000f;
            objectiveHandler.ObjectivePopup("Camera battery set to 100%");
            Destroy(gameObject);
        }    
        Debug.Log("Interacted");
    }

    public void InteractHold()
    {
        if (!DialogueManager.Instance.isMessageDisplaying)
        {
            DialogueManager.Instance.CommentMessage("Camera is at " + Mathf.Floor(CameraManager.batteryPrec / 10) + " precent.");
        }
    }
}
