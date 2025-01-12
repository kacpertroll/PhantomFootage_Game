using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class objectiveHandler : MonoBehaviour
{

    public float objectivesCount = 7;
    public static float pickedObjectives;

    public TextMeshProUGUI objectiveMessage;
    
    private static Animator animatorObjective;

    void Start()
    {
        pickedObjectives = 0;
        animatorObjective = GetComponent<Animator>();
        objectiveMessage.text = $"Objectives - {pickedObjectives}/{objectivesCount}";
    }

    void Update()
    {
        objectiveMessage.text = $"Objectives - {pickedObjectives}/{objectivesCount}";
    }

    public static void ObjectiveFound(string messageFind)
    {
        pickedObjectives++;
        animatorObjective.SetBool("Playing", true);
        DialogueManager.Instance.CommentMessage(messageFind);
    }

    public void ObjectiveAnimationEnd()
    {
        animatorObjective.SetBool("Playing", false);
    }
}
