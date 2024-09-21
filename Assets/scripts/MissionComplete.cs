using UnityEngine;
using System.Collections;

public class MissionComplete : MonoBehaviour
{
    public SpriteRenderer missionCompleteSprite; // Assign the Mission Complete Sprite here
    public float flickerDuration = 2f;           // Total time for flickering
    public float fadeDuration = 1f;              // Time to fade in
    public float flickerSpeed = 5f;              // Speed of flickering

    private void Start()
    {
        StartCoroutine(ShowMissionComplete());
    }

    private IEnumerator ShowMissionComplete()
    {
        // Show the mission complete sprite
        missionCompleteSprite.gameObject.SetActive(true);

        // Fade in
        yield return FadeIn(missionCompleteSprite);

        float elapsedTime = 0f;

        // Flickering effect
        while (elapsedTime < flickerDuration)
        {
            float alpha = Mathf.PingPong(elapsedTime * flickerSpeed, 1);
            SetSpriteAlpha(missionCompleteSprite, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the sprite remains visible at full opacity
        SetSpriteAlpha(missionCompleteSprite, 1);
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

    private void SetSpriteAlpha(SpriteRenderer spriteRenderer, float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
