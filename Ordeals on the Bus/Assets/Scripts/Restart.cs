using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void LoadScene(string Aaron)
    {
        SceneManager.LoadScene(Aaron);
    }
}