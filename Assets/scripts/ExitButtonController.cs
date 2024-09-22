using UnityEngine;
using UnityEngine.UI;

public class ExitButtonController : MonoBehaviour
{
    private Button exitButton;

    void Start()
    {
        // Get the Button component attached to this GameObject
        exitButton = GetComponent<Button>();

        if (exitButton != null)
        {
            // Add a listener to the exit button
            exitButton.onClick.AddListener(OnExitButtonClicked);
        }
        else
        {
            Debug.LogError("Button component not found on the Exit-Button.");
        }
    }

    void OnExitButtonClicked()
    {
        // Quit the application
        Application.Quit();

        // If running in the editor, stop playing the scene
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
