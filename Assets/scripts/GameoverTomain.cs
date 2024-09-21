using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameoverTomain : MonoBehaviour
{
    public Image fadeImage; 
    public float fadeDuration = 1f; // Duration of the fade
    public float stayDuration = 2f; // Duration to stay after fade in

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeCoroutine());
    }

    private IEnumerator FadeCoroutine()
    {
        // Fade In
        yield return Fade(1f, 0f); // From black (1) to clear (0)
        // Stay colorful
        yield return new WaitForSeconds(stayDuration);
        // Fade Out
        yield return Fade(0f, 1f); // From clear (0) to black (1)
        // Load Main Menu
        SceneManager.LoadScene("Mainmenu");
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float time = 0;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, time / fadeDuration);
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }
        
        Color finalColor = fadeImage.color;
        finalColor.a = endAlpha;
        fadeImage.color = finalColor;
    }
}
