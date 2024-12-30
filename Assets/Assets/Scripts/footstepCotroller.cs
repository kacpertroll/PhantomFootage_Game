using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioSource footstepAudioSource; // ród³o dŸwiêku kroków
    public AudioClip[] footstepClips; // Tablica z ró¿nymi dŸwiêkami kroków
    public float minPitch = 0.9f; // Minimalny pitch dŸwiêku
    public float maxPitch = 1.1f; // Maksymalny pitch dŸwiêku
    public float stepInterval = 0.5f; // Czas pomiêdzy krokami w sekundach
    private float sprintInterval;

    private int firstSound = 0;
    private int lastSound = 3;

    private float stepTimer = 0f; // Timer do obs³ugi interwa³u kroków

    private void Start()
    {
        sprintInterval = stepInterval / 1.5f;
    }

    private void Update()
    {
        if (AmbienceController.triggerCounter >= 1)
        {
            firstSound = 4;
            lastSound = 7;
            footstepAudioSource.volume = 0.1f;
        }
        else
        {
            firstSound = 0;
            lastSound = 3;
            footstepAudioSource.volume = 0.01f;
        }

        // Sprawdzanie, czy postaæ siê porusza
        if (FirstPersonController.isWalking)
        {
            stepTimer -= Time.deltaTime;
            if (FirstPersonController.isSprinting)
            {
                if (stepTimer <= 0f)
                {
                    PlayFootstep();
                    stepTimer = sprintInterval; // Reset timera
                }
            }
            else
            {
                // Jeœli up³yn¹³ czas miêdzy krokami
                if (stepTimer <= 0f)
                {
                    PlayFootstep();
                    stepTimer = stepInterval; // Reset timera
                }
            }
            
        }
        else
        {
            // Reset timera, gdy postaæ przestaje siê poruszaæ
            stepTimer = 0f;
        }
    }

    private void PlayFootstep()
    {
        if (footstepClips.Length > 0)
        {
            // Wybierz losowy dŸwiêk z tablicy
            AudioClip clip = footstepClips[Random.Range(firstSound, lastSound)];

            // Ustaw losowy pitch
            footstepAudioSource.pitch = Random.Range(minPitch, maxPitch);

            // Odtwórz dŸwiêk
            footstepAudioSource.PlayOneShot(clip);
        }
    }
}
