using UnityEngine;
using System.Collections;

public class MissionStart : MonoBehaviour
{
    public SpriteRenderer missionSprite; // Assign the Mission-1 Sprite here
    public SpriteRenderer startSprite;   // Assign the Start-Mission Sprite here
    public float flickerDuration = 2f;   // Total time for flickering
    public float fadeDuration = 1f;      // Time to fade out
    public float flickerSpeed = 5f;      // Speed of flickering

    private void Start()
    {
        StartCoroutine(ShowIntro());
    }

    private IEnumerator ShowIntro()
    {
        // Show sprites
        missionSprite.gameObject.SetActive(true);
        startSprite.gameObject.SetActive(true);

        // Fade in
        yield return FadeIn(missionSprite);
        yield return FadeIn(startSprite);

        float elapsedTime = 0f;

        // Flickering effect
        while (elapsedTime < flickerDuration)
        {
            float alpha = Mathf.PingPong(elapsedTime * flickerSpeed, 1);
            SetSpriteAlpha(missionSprite, alpha);
            SetSpriteAlpha(startSprite, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Fade out simultaneously
        yield return FadeOutSimultaneously(missionSprite, startSprite);

        // Hide sprites
        missionSprite.gameObject.SetActive(false);
        startSprite.gameObject.SetActive(false);
    }

    private IEnumerator FadeIn(SpriteRenderer spriteRenderer)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            SetSpriteAlpha(spriteRenderer, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetSpriteAlpha(spriteRenderer, 1); // Ensure fully opaque
    }

    private IEnumerator FadeOutSimultaneously(SpriteRenderer sprite1, SpriteRenderer sprite2)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            SetSpriteAlpha(sprite1, alpha);
            SetSpriteAlpha(sprite2, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetSpriteAlpha(sprite1, 0); // Ensure fully transparent
        SetSpriteAlpha(sprite2, 0); // Ensure fully transparent
    }

    private void SetSpriteAlpha(SpriteRenderer spriteRenderer, float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
