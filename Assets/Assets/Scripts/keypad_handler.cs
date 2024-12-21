using UnityEngine;

public class keypad_handler : MonoBehaviour
{
    public string ValidCode;  // Prawid³owy kod do odblokowania
    private string EnteredCode = "";  // Aktualnie wpisywany kod
    public GameObject Door;  // Referencja do drzwi

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Funkcja do dodawania cyfr
    public void AddDigit(string digit)
    {
        if (EnteredCode.Length < ValidCode.Length)
        {
            EnteredCode += digit;
            Debug.Log("Wprowadzony kod: " + EnteredCode);

            // Sprawdzanie, czy osi¹gniêto 4 cyfry
            if (EnteredCode.Length == ValidCode.Length)
            {
                CheckCode();
            }
        }
    }

    private void CheckCode()
    {
        if (EnteredCode == ValidCode)
        {
            Debug.Log("Kod poprawny. Drzwi odblokowane!");
            UnlockDoor();
        }
        else
        {
            Debug.Log("Kod b³êdny!");
            audioSource.PlayDelayed(0.1f); // Bzyczenie b³êdu
            ResetCode();
        }
    }

    private void ResetCode()
    {
        EnteredCode = "";
    }

    private void UnlockDoor()
    {
        // Tutaj odblokowujesz drzwi (np. wywo³anie animacji lub zmiana stanu)
        if (Door != null)
        {
            doorAnimationController.isLocked = false;
            Animator doorAnimator = Door.GetComponent<Animator>();
            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger("IsOpen"); // Zak³adam, ¿e masz trigger 'Open'
            }
        }
    }
}
