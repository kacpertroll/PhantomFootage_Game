using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteInspector : MonoBehaviour
{
    public TextMeshProUGUI textNote;
    public TextMeshProUGUI textDate;
    public Image background;
    public GameObject displayNote;
    public MonoBehaviour playerMovementScript; // Skrypt odpowiedzialny za ruch postaci
    public MonoBehaviour camcorderControllerScript;

    private bool isNoteVisible = false;

    void Update()
    {
        if (isNoteVisible && Input.GetKeyDown(KeyCode.E))
        {
            CloseNote();
        }
    }

    // Funkcja do wywo³ywania w innych skryptach
    public void ShowNote(string noteText, string noteDate)
    {
        textNote.text = noteText;
        textDate.text = noteDate;
        displayNote.SetActive(true);
        playerMovementScript.enabled = false;
        camcorderControllerScript.enabled = false;// Wy³¹cza ruch postaci
        isNoteVisible = true;
    }

    private void CloseNote()
    {
        displayNote.SetActive(false);
        playerMovementScript.enabled = true;
        camcorderControllerScript.enabled = true;
        isNoteVisible = false;
    }
}
