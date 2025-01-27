using UnityEngine;

public class forkliftInteractor : MonoBehaviour, IInteractableHold
{
    private Animator forkliftAnim;
    private AudioSource forkliftSound;

    void Start()
    {
        forkliftAnim = GetComponent<Animator>();
        forkliftSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (forkliftAnim != null)
        {
            GetComponent<AudioSource>().Play();
            GetComponent<Animator>().SetTrigger("forkOn");
        }
        gameObject.tag = "Untagged";
    }

    public void InteractHold()
    {

    }
}
