using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameHandler : MonoBehaviour
{
    public void StartGame()
    {
        var level = SceneManager.GetActiveScene().buildIndex + 1;
        SaveSystem.SaveLevel(level);
        SceneManager.LoadScene(level);
    }
}
