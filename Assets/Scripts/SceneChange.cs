using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using System.Collections;
using VolFx;

public class SceneChange : MonoBehaviour
{
    public Volume volume;
    public GameObject Main_Menu;
    public GameObject Settings_Menu;

    private bool isPlaying = false;

    public AudioSource choiceStatic;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Settings()
    {
        if (!isPlaying)
        {
            Main_Menu.SetActive(false);
            Settings_Menu.SetActive(true);
        }
    }

    public void MainMenu()
    {
        if (!isPlaying)
        {
            Main_Menu.SetActive(true);
            Settings_Menu.SetActive(false);
        }   
    }

    public void StartLerpVhsDensity()
    {
        if (volume.profile.TryGet(out VhsVol noise))
        {
            // Uruchamiamy korutynê, jeœli efekt VHS istnieje w profilu
            StartCoroutine(LerpVhsDensity(noise, 1f, 0.05f, 1f)); // 1 sekunda
        }
    }

    private IEnumerator LerpVhsDensity(VhsVol noise, float startValue, float endValue, float duration)
    {
        if (!isPlaying)
        {
            isPlaying = true;
            float elapsedTime = 0f;
            choiceStatic.Play();
            // Ustawiamy pocz¹tkow¹ wartoœæ density
            noise._density.value = startValue;

            while (elapsedTime < duration)
            {
                // Interpolujemy wartoœæ density w czasie
                noise._density.value = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null; // Czekamy do koñca aktualnej klatki
            }
            isPlaying = false;
        }

        // Upewniamy siê, ¿e wartoœæ koñcowa jest ustawiona dok³adnie po zakoñczeniu
        noise._density.value = endValue;
    }

    public void GameQuit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
