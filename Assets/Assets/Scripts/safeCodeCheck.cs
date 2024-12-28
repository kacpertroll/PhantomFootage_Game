using UnityEngine;

public class safeCodeCheck : MonoBehaviour
{
    public string codeValid;
    public string Dialogue;

    public GameObject code_1;
    public GameObject code_2;
    public GameObject code_3;

    public Animator safeAnimator;

    private string codeActive;

    void Start()
    {
        
    }

    private int GetCodeId(GameObject obj)
    {
        safeHandler codeScript = obj.GetComponent<safeHandler>();
        return codeScript.codeId;
    }

    private void CodeCheck()
    {
        if (!safeAnimator.GetBool("isOpen") && codeActive.Equals(codeValid))
        {
            safeAnimator.SetBool("isOpen", true);
            code_1.tag = "Untagged";
            code_2.tag = "Untagged";
            code_3.tag = "Untagged";
            if (DialogueManager.Instance.isMessageDisplaying == false)
            {
                DialogueManager.Instance.CommentMessage(Dialogue);
            }
        }
    }

    void Update()
    {
        int code_1_num = GetCodeId(code_1);
        int code_2_num = GetCodeId(code_2);
        int code_3_num = GetCodeId(code_3);

        codeActive = $"{code_1_num}" + $"{code_2_num}" + $"{code_3_num}";

        CodeCheck();
    }
}
