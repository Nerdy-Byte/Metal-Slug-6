using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class ScoreControler : MonoBehaviour
{
    public static int scoreValue = 0;
    public TextMeshProUGUI scoreText; // Use TextMeshProUGUI for UI text

    void Start()
    {
        // Optionally, assign the TextMeshProUGUI component if not done through the Inspector
        if (scoreText == null)
        {
            scoreText = GetComponent<TextMeshProUGUI>();
        }

        // Check if the current scene is "lvl1-fortress"
        if (SceneManager.GetActiveScene().name == "lvl1-fortress")
        {
            scoreValue = 0; // Reset score to zero
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + scoreValue; 
    }

    public static void AddPoints(int pointsToAdd)
    {
        scoreValue += pointsToAdd;
    }
}
