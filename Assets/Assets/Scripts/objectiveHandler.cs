using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class objectiveHandler : MonoBehaviour
{

    public static float objectivesCount = 7;
    public static float pickedObjectives;

    public TextMeshProUGUI objectiveMessage;
    public static TextMeshProUGUI objectiveStaticMessage;
    
    private static Animator animatorObjective;

    void Start()
    {
        objectiveStaticMessage = objectiveMessage;

        pickedObjectives = 0;
        animatorObjective = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public static void ObjectiveFound(string messageFind)
    {
        pickedObjectives++;
        objectiveStaticMessage.text = $"Objectives - {pickedObjectives}/{objectivesCount}";
        animatorObjective.SetBool("Playing", true);
        DialogueManager.Instance.CommentMessage(messageFind);
    }

    public void ObjectiveAnimationEnd()
    {
        animatorObjective.SetBool("Playing", false);
    }

    public static void ObjectivePopup(string alertMessage)
    {
        objectiveStaticMessage.text = alertMessage;
        animatorObjective.SetBool("Playing", true);
    }
}
