using UnityEngine;

public class keypad_keyPress : MonoBehaviour, IInteractableHold
{
    public string KeyCode;  // Wartoœæ przypisana do przycisku
    public static bool animationOn = false;
    private Animator animator;

    private keypad_handler keypadHandler;

    void Start()
    {
        gameObject.tag = "Interactable";
        animator = GetComponentInChildren<Animator>();

        // Znalezienie referencji do bazy (keypad_handler)
        keypadHandler = FindObjectOfType<keypad_handler>();
        if (keypadHandler == null)
        {
            Debug.LogError("keypad_handler nie znaleziony w scenie!");
        }
    }

    public void Interact()
    {
        if (!animationOn)
        {
            animator.SetBool("Pressed", true);
            animationOn = animator.GetBool("Pressed");
            GetComponentInChildren<AudioSource>().Play();

            if (keypadHandler != null)
            {
                keypadHandler.AddDigit(KeyCode);
            }
            else
            {
                Debug.LogError("keypad_handler jest null!");
            }
        }
    }

    public void InteractHold()
    {
        
    }
}
