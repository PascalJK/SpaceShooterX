using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool _isGameOver;

    public void IsGameOver()
    {
        _isGameOver = true;
    }

    void Update()
    {
        ReloadScene();
    }

    void ReloadScene()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            var currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
    }
}
