using UnityEngine;

public class AmbienceController : MonoBehaviour
{
    public AudioSource AmbienceIndoor; // D�wi�k wewn�trz budynku
    public AudioSource AmbienceOutdoor; // D�wi�k na zewn�trz budynku
    public float fadeSpeed = 1f; // Pr�dko�� zmiany g�o�no�ci

    private float targetIndoorVolume = 0f; // Docelowa g�o�no�� dla indoor
    private float targetOutdoorVolume = 0.1f; // Docelowa g�o�no�� dla outdoor
    public static int triggerCounter = 0; // Licznik wej�� do trigger�w

    private void Start()
    {
        triggerCounter = 0;
        // Ustaw pocz�tkowe g�o�no�ci
        AmbienceIndoor.volume = 0f;
        AmbienceOutdoor.volume = 0.1f;
    }

    private void Update()
    {
        // Stopniowe przej�cie g�o�no�ci do docelowych warto�ci
        AmbienceIndoor.volume = Mathf.Lerp(AmbienceIndoor.volume, targetIndoorVolume, fadeSpeed * Time.deltaTime);
        AmbienceOutdoor.volume = Mathf.Lerp(AmbienceOutdoor.volume, targetOutdoorVolume, fadeSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Gracz wchodzi do triggera - zwi�kszamy licznik
            triggerCounter++;
            if (triggerCounter == 1) // Tylko je�li to pierwsze wej�cie, zmieniamy g�o�no�ci
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
            if (triggerCounter == 0) // Tylko je�li opu�ci� wszystkie triggery, zmieniamy g�o�no�ci
            {
                targetIndoorVolume = 0f;
                targetOutdoorVolume = 0.05f;
            }
        }
    }
}
