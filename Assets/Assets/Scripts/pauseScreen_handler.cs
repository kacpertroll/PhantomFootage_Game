using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class pauseScreen_handler : MonoBehaviour
{
    public GameObject PauseScreen;
    public GameObject postProcessing;
    public FirstPersonController playerController;
    public CameraManager cameraManager;

    private Volume volume;

    private bool isPaused = false;

    void Start()
    {
        volume = postProcessing.GetComponent<Volume>();
    }

    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !isPaused) 
        {
            Pause();
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && isPaused)
        {
            Continue();
        }
    }

    public void Pause()
    {
        isPaused = true;
        PauseScreen.SetActive(true);

        // Blurring background with Post Processing
        if (volume.profile.TryGet<LiftGammaGain>(out var lfg))
        {
            lfg.active = true;
        }
        if (volume.profile.TryGet<UnityEngine.Rendering.Universal.DepthOfField>(out var dp))
        {
            dp.active = true;
        }

        // Unlocking cursor while on Pause screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Disabling scripts
        playerController.enabled = false;
        cameraManager.enabled = false;

        // Setting timescale to 0 to pause game
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        isPaused = false;
        PauseScreen.SetActive(false);

        // Turning off pause screen post processing
        if (volume.profile.TryGet<LiftGammaGain>(out var lfg))
        {
            lfg.active = false;
        }
        if (volume.profile.TryGet<UnityEngine.Rendering.Universal.DepthOfField>(out var dp))
        {
            dp.active = false;
        }

        // Locking back cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Enabling scripts
        playerController.enabled = true;
        cameraManager.enabled = true;

        // Unpausing time
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
