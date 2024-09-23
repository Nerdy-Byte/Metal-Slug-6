using UnityEngine;
using UnityEngine.SceneManagement;

// This class manages the main menu functionalities.
public class MainMenuController : MonoBehaviour
{
    // This method is called to start the game by loading the first level.
    public void StartGame()
    {
        // Load the scene named "lvl1-fortress" to start the game.
        SceneManager.LoadScene("lvl1-fortress");
    }
}
