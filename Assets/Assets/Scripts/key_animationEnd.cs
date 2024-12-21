using UnityEngine;

public class key_animationEnd : MonoBehaviour
{
    
    public void PressedEnd()
    {
        GetComponent<Animator>().SetBool("Pressed", false);
        keypad_keyPress.animationOn = false;
    }

}
