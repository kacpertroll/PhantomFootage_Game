using UnityEngine;

public class keypad_handler : MonoBehaviour
{
    public string ValidCode;  // Prawid�owy kod do odblokowania
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

            // Sprawdzanie, czy osi�gni�to 4 cyfry
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
            Debug.Log("Kod b��dny!");
            audioSource.PlayDelayed(0.1f); // Bzyczenie b��du
            ResetCode();
        }
    }

    private void ResetCode()
    {
        EnteredCode = "";
    }

    private void UnlockDoor()
    {
        // Tutaj odblokowujesz drzwi (np. wywo�anie animacji lub zmiana stanu)
        if (Door != null)
        {
            doorAnimationController.isLocked = false;
            Animator doorAnimator = Door.GetComponent<Animator>();
            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger("IsOpen"); // Zak�adam, �e masz trigger 'Open'
            }
        }
    }
}
