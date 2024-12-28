using UnityEngine;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour
{
    public Animator batteryAnim;

    public float cameraDeplet = 1;
    public static float batteryPrec = 1000;
    private bool cameraActive = false;
    private bool batteryOn = true;

    public GameObject CamcorderCamera;
    public Camera mainCamera;
    public GameObject Outline;

    private void Start()
    {
        batteryPrec = 1000;
    }
    // Update is called once per frame
    void Update()
    {
        ZoomAnimation();
        BatteryManager();
        BatteryAnimation();
    }

    void BatteryManager()
    {
        // Sprawdza aktywnoœc kamery, oczywiœcie ¿eby aktywowaæ kamerê, to musi byæ ona nieaktywna, a bateria musi byæ na³adowana.
        if (Input.GetKeyDown(KeyCode.Mouse1) && !cameraActive && batteryOn)
        {
            cameraActive = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            cameraActive = false;
        }

        if (cameraActive)
        {
            batteryPrec -= cameraDeplet * Time.deltaTime;   // Je¿eli kamera jest aktywna, to pobiera 0.1 procent baterii na sekundê.
            Debug.Log($"Battery: {batteryPrec}");
        }

        if(!cameraActive)
        {
            mainCamera.farClipPlane = 50;
            CamcorderCamera.SetActive(false);
        }
        else 
        {
            mainCamera.farClipPlane = 10;
            CamcorderCamera.SetActive(true); 
        }

        if (batteryPrec <= 0)
        {
            batteryPrec = 0;
            batteryOn = false;
            cameraActive = false;
            zoomer.SetBool("zoomActive", false);
        }

        else if (batteryPrec > 0) 
        {
            batteryOn = true;
        }
    }

    void BatteryAnimation()
    {
        if (cameraActive)
        {
            batteryAnim.SetFloat("battery%", batteryPrec);
        }
    }

    public Animator zoomer;
    private bool zoomActive;

    void ZoomAnimation()
    {
        // Zwyczajna obs³uga animacji kamery, te¿ mo¿liwa tylko przy na³adowanej baterii.
        // TODO: Informacja o roz³adowanej baterii gdy próbujemy w³¹czyæ z roz³adowan¹ bateri¹.
        if (Input.GetKeyDown(KeyCode.Mouse1) && batteryOn)
        {
            zoomActive = true;
            zoomer.SetBool("zoomActive", zoomActive);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            zoomActive = false;
            zoomer.SetBool("zoomActive", zoomActive);
        }
    }

}
