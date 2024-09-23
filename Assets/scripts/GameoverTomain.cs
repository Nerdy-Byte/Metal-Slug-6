using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This class handles the game over screen transition to the main menu with a fade effect.
public class GameoverTomain : MonoBehaviour
{
    // Image component used for the fade effect.
    public Image fadeImage; 

    // Duration of the fade effect.
    public float fadeDuration = 1f; 

    // Duration to stay visible after the fade in.
    public float stayDuration = 2f; 

    // Start is called before the first frame update
    void Start()
    {
        // Begin the fade coroutine when the script starts.
        StartCoroutine(FadeCoroutine());
    }

    // Coroutine to handle the fade in, stay, and fade out transitions.
    private IEnumerator FadeCoroutine()
    {
        // Fade In from black (1) to clear (0).
        yield return Fade(1f, 0f); 

        // Wait for the specified duration to stay visible.
        yield return new WaitForSeconds(stayDuration);

        // Fade Out from clear (0) to black (1).
        yield return Fade(0f, 1f); 

        // Load the main menu scene.
        SceneManager.LoadScene("Mainmenu");
    }

    // Coroutine to perform the actual fade effect.
    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float time = 0; // Track the elapsed time for the fade

        // Loop until the fade duration is reached
        while (time < fadeDuration)
        {
            time += Time.deltaTime; // Increase elapsed time
            // Calculate the new alpha value based on the elapsed time
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            Color color = fadeImage.color; // Get the current color of the fade image
            color.a = alpha; // Set the new alpha value
            fadeImage.color = color; // Apply the updated color
            yield return null; // Wait for the next frame
        }
        
        // Ensure the final alpha value is set correctly
        Color finalColor = fadeImage.color;
        finalColor.a = endAlpha;
        fadeImage.color = finalColor;
    }
}
