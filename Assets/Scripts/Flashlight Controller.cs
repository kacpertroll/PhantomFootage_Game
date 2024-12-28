using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    private Light flashlight;
    private AudioSource clickSound;
    
    private void Start()
    {
        flashlight = GetComponentInChildren<Light>();
        clickSound = GetComponent<AudioSource>();
    }

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
