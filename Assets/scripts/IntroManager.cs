using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// This class manages the introductory sequence of the game, including fading in/out and playing audio.
public class IntroManager : MonoBehaviour
{
    public float introDuration = 5f; // Duration of the intro display.
    public float fadeDuration = 1f; // Duration for fade in/out effects.
    private CanvasGroup canvasGroup; // Reference to the CanvasGroup for fading.
    private AudioSource audioSource; // Reference to the audio source for playing intro sound.

    private void Start()
    {
        // Get the AudioSource component attached to this GameObject.
        audioSource = GetComponent<AudioSource>();
        // Access the CanvasGroup component in the child GameObject for fade effects.
        canvasGroup = GetComponentInChildren<CanvasGroup>(); 

        // Play the intro sound if the AudioSource is not null.
        if (audioSource != null)
        {
            audioSource.Play(); // Start playing the intro sound.
        }

        // Start the coroutine to handle the intro sequence.
        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        // Fade in the screen from black to transparent.
        yield return StartCoroutine(Fade(2f, 0f)); 

        // Wait for the specified duration of the intro.
        yield return new WaitForSeconds(introDuration); 

        // Fade out the screen from transparent to black.
        yield return StartCoroutine(Fade(0f, 1f)); 

        // Load the main menu scene after the intro ends.
        LoadMainMenu();
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f; // Track the time elapsed for fading.

        // Gradually change the alpha value from startAlpha to endAlpha.
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime; // Increment elapsed time.
            // Change transparency level based on elapsed time and fade duration.
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            yield return null; // Wait until the next frame.
        }

        // Check the final alpha value is set correctly.
        canvasGroup.alpha = endAlpha; 
    }

    private void LoadMainMenu()
    {
        // Load the main menu scene.
        SceneManager.LoadScene("Mainmenu");
    }
}
