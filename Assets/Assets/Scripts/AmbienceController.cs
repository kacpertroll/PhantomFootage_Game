using UnityEngine;

public class AmbienceController : MonoBehaviour
{
    public AudioSource AmbienceIndoor; // DŸwiêk wewn¹trz budynku
    public AudioSource AmbienceOutdoor; // DŸwiêk na zewn¹trz budynku
    public float fadeSpeed = 1f; // Prêdkoœæ zmiany g³oœnoœci

    private float targetIndoorVolume = 0f; // Docelowa g³oœnoœæ dla indoor
    private float targetOutdoorVolume = 0.1f; // Docelowa g³oœnoœæ dla outdoor
    public static int triggerCounter = 0; // Licznik wejœæ do triggerów

    private void Start()
    {
        triggerCounter = 0;
        // Ustaw pocz¹tkowe g³oœnoœci
        AmbienceIndoor.volume = 0f;
        AmbienceOutdoor.volume = 0.1f;
    }

    private void Update()
    {
        // Stopniowe przejœcie g³oœnoœci do docelowych wartoœci
        AmbienceIndoor.volume = Mathf.Lerp(AmbienceIndoor.volume, targetIndoorVolume, fadeSpeed * Time.deltaTime);
        AmbienceOutdoor.volume = Mathf.Lerp(AmbienceOutdoor.volume, targetOutdoorVolume, fadeSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Gracz wchodzi do triggera - zwiêkszamy licznik
            triggerCounter++;
            if (triggerCounter == 1) // Tylko jeœli to pierwsze wejœcie, zmieniamy g³oœnoœci
            {
                targetIndoorVolume = 0.05f;
                targetOutdoorVolume = 0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Gracz wychodzi z triggera - zmniejszamy licznik
            triggerCounter--;
            if (triggerCounter == 0) // Tylko jeœli opuœci³ wszystkie triggery, zmieniamy g³oœnoœci
            {
                targetIndoorVolume = 0f;
                targetOutdoorVolume = 0.05f;
            }
        }
    }
}
