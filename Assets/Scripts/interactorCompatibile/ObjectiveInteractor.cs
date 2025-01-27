using UnityEngine;

public class ObjectiveInteractor : MonoBehaviour, IInteractableHold
{
    public string findMessage;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (DialogueManager.Instance.isMessageDisplaying == false && findMessage != null)
        {
            objectiveHandler.ObjectiveFound(findMessage);
            gameObject.SetActive(false);
        }
    }

    public void InteractHold()
    {

    }
}
