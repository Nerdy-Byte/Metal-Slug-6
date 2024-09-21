using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;
    public string nextSceneName; // Public variable to set the next scene

    private void Start()
    {
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        // Fade In
        yield return Fade(1f, 0f, 1f); // From transparent to opaque over 1 second

        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Fade Out
        yield return Fade(0f, 1f, 1f); // From opaque to transparent over 1 second

        // Load the next level
        SceneManager.LoadScene(nextSceneName); // Use the variable here
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = startAlpha;
        fadeImage.color = color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }
}
