using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroManager : MonoBehaviour
{
    public float introDuration = 5f;
    public float fadeDuration = 1f; // Duration for fade in/out
    private CanvasGroup canvasGroup;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canvasGroup = GetComponentInChildren<CanvasGroup>(); // Access the CanvasGroup

        if (audioSource != null)
        {
            audioSource.Play(); // Play intro sound
        }

        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        yield return StartCoroutine(Fade(2f, 0f)); // Fade in (from black to transparent)

        yield return new WaitForSeconds(introDuration); // Wait for the intro duration

        yield return StartCoroutine(Fade(0f, 1f)); // Fade out (from transparent to black)

        LoadMainMenu();
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            yield return null; // Wait until the next frame
        }

        canvasGroup.alpha = endAlpha; // Ensure the end value is set
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }
}
