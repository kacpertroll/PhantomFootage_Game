using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu_UI : MonoBehaviour
{
    public TextMeshProUGUI versionNum;

    void Start()
    {
        string versionNumber = Application.version;

        versionNum.text = "v" + versionNumber;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
