using UnityEngine;

public class noteInteractor : MonoBehaviour, IInteractableHold
{
    public string NoteText;
    public string NoteDate;

    void Start()
    {
        gameObject.tag = "Interactable";
    }

    public void Interact()
    {
        if (gameObject.tag == "Interactable")
        {
            FindFirstObjectByType<NoteInspector>().ShowNote(NoteText, NoteDate);
            gameObject.tag = "Untagged";
        }
    }

    public void InteractHold()
    {

    }
}
