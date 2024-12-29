using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioSource footstepAudioSource; // èrÛd≥o düwiÍku krokÛw
    public AudioClip[] footstepClips; // Tablica z rÛønymi düwiÍkami krokÛw
    public float minPitch = 0.9f; // Minimalny pitch düwiÍku
    public float maxPitch = 1.1f; // Maksymalny pitch düwiÍku
    public float stepInterval = 0.5f; // Czas pomiÍdzy krokami w sekundach
    private float sprintInterval;

    private float stepTimer = 0f; // Timer do obs≥ugi interwa≥u krokÛw

    private void Start()
    {
        sprintInterval = stepInterval / 1.5f;
    }

    private void Update()
    {
        // Sprawdzanie, czy postaÊ siÍ porusza
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
                // Jeúli up≥ynπ≥ czas miÍdzy krokami
                if (stepTimer <= 0f)
                {
                    PlayFootstep();
                    stepTimer = stepInterval; // Reset timera
                }
            }
            
        }
        else
        {
            // Reset timera, gdy postaÊ przestaje siÍ poruszaÊ
            stepTimer = 0f;
        }
    }

    private void PlayFootstep()
    {
        if (footstepClips.Length > 0)
        {
            // Wybierz losowy düwiÍk z tablicy
            AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];

            // Ustaw losowy pitch
            footstepAudioSource.pitch = Random.Range(minPitch, maxPitch);

            // OdtwÛrz düwiÍk
            footstepAudioSource.PlayOneShot(clip);
        }
    }
}
