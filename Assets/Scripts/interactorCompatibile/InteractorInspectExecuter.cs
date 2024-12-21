using UnityEngine;

public class InteractorInspectExecuter : MonoBehaviour, IInteractableHold
{
    public bool isAnimated = false; // Je�eli obiekt ma animacj�, zaznaczamy true.
    public bool isDialogue = false; // Je�eli obiekt przy interakcji ma wywo�a� tekst, zaznaczamy true.
    public bool destroyOnInteract = false;

    public string animationName; // Nazwa warunku animacji.
    public string[] dialogueText; // Tablica tekstu wywo�ywanego przez obiekt w trakcie interakcji.

    void Start()
    {
        transform.gameObject.tag = "Interactable"; // Na start �eby si� nie bawi�, ka�dy obiekt z tym skryptem zmienia tag na Interactable, �eby reagowa� na Outline.
    }

    public void Interact()
    {
        if (DialogueManager.Instance.isMessageDisplaying == false)
        {
            if (isAnimated)
            {
                GetComponent<Animator>().SetBool(animationName, true); // Zmiana warunku animacji obiektu na true.
            }
            if (isDialogue)
            {
                for (int i = 0; i < dialogueText.Length; i++) // Dla ka�dego wpisanego do tablicy tekstu.
                {
                    if (i == 0)
                    {
                        DialogueManager.Instance.CommentMessage(dialogueText[i]); // Pierwszy tekst.
                    }
                    else
                    {
                        DialogueManager.Instance.CommentDelayMessage(dialogueText[i]); // Ka�dy kolejny tekst.
                    }
                }
            }
            transform.gameObject.tag = "Untagged";
            if (destroyOnInteract)
            {
                Destroy(gameObject);
            }
        }
    }

    public void InteractHold()
    {
        // Narazie pusto, ale mo�e co� wymy�l�.
    }
}
