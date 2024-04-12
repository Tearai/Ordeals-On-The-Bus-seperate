using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // Define the name of the scene to switch to
    public string nextSceneName;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the collider
        if (other.CompareTag("Player"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            // Load the next scene
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
