using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioSource footstepAudioSource; // �r�d�o d�wi�ku krok�w
    public AudioClip[] footstepClips; // Tablica z r�nymi d�wi�kami krok�w
    public float minPitch = 0.9f; // Minimalny pitch d�wi�ku
    public float maxPitch = 1.1f; // Maksymalny pitch d�wi�ku
    public float stepInterval = 0.5f; // Czas pomi�dzy krokami w sekundach
    private float sprintInterval;

    private float stepTimer = 0f; // Timer do obs�ugi interwa�u krok�w

    private void Start()
    {
        sprintInterval = stepInterval / 1.5f;
    }

    private void Update()
    {
        // Sprawdzanie, czy posta� si� porusza
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
                // Je�li up�yn�� czas mi�dzy krokami
                if (stepTimer <= 0f)
                {
                    PlayFootstep();
                    stepTimer = stepInterval; // Reset timera
                }
            }
            
        }
        else
        {
            // Reset timera, gdy posta� przestaje si� porusza�
            stepTimer = 0f;
        }
    }

    private void PlayFootstep()
    {
        if (footstepClips.Length > 0)
        {
            // Wybierz losowy d�wi�k z tablicy
            AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];

            // Ustaw losowy pitch
            footstepAudioSource.pitch = Random.Range(minPitch, maxPitch);

            // Odtw�rz d�wi�k
            footstepAudioSource.PlayOneShot(clip);
        }
    }
}
