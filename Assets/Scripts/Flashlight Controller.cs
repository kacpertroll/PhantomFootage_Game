using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;
    public AudioSource clickSound;
    // Update is called once per frame
    void Update()
    {
        if (flashlight != null)
        {
            if (Input.GetKeyDown(KeyCode.F) && flashlight.isActiveAndEnabled)
            {
                flashlight.enabled = false;
                clickSound.Play();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                flashlight.enabled = true;
                clickSound.Play();
            }
        }
    }
}
