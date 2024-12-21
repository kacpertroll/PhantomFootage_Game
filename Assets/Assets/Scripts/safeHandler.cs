using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

public class safeHandler : MonoBehaviour, IInteractableHold
{
    public int codeId = 0;

    public Animator Animator;

    public void Start()
    {
        codeId = Animator.GetInteger("codeNumber");
    }

    public void Interact()
    {
            CodeIncrement();
    }

    public void InteractHold()
    {
        


    }

    public void CodeIncrement()
    {
        AnimatorStateInfo stateInfo = Animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime >= 1f && !Animator.IsInTransition(0))
        {
                if (codeId < 9)
                {
                    codeId += 1;
                }
                else
                {
                    codeId = 0;
                }
                Animator.SetInteger("codeNumber", codeId);
                Debug.Log(Animator.GetInteger("codeNumber"));
                GetComponent<AudioSource>().Play();
        }
    }

}
