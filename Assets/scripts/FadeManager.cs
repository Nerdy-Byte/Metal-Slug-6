using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This class manages the fade in and fade out transitions between scenes.
public class FadeManager : MonoBehaviour
{
    // Image component used for the fade effect.
    public Image fadeImage;

    // Name of the next scene to load after the fade out.
    public string nextSceneName; // Public variable to set the next scene

    private void Start()
    {
        // Start the fade in and out coroutine when the game starts.
        StartCoroutine(FadeInAndOut());
    }

    // Coroutine to handle the fade in and out process.
    private IEnumerator FadeInAndOut()
    {
        // Fade In from transparent to opaque over 1 second.
        yield return Fade(1f, 0f, 1f); 

        // Wait for 5 seconds before fading out.
        yield return new WaitForSeconds(5f);

        // Fade Out from opaque to transparent over 1 second.
        yield return Fade(0f, 1f, 1f); 

        // Load the next scene specified in the nextSceneName variable.
        SceneManager.LoadScene(nextSceneName); 
    }

    // Coroutine to handle the actual fading effect.
    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f; // Track the elapsed time
        Color color = fadeImage.color; // Get the current color of the fade image
        color.a = startAlpha; // Set the starting alpha
        fadeImage.color = color; // Apply the starting color

        // Loop until the duration is reached
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime; // Increase elapsed time
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration); // Calculate new alpha value
            fadeImage.color = color; // Update the fade image color
            yield return null; // Wait for the next frame
        }

        // Ensure the final alpha value is set correctly
        color.a = endAlpha;
        fadeImage.color = color;
    }
}
