using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameHandler : MonoBehaviour
{
    public void StartGame()
    {
        var level = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt("level", level);
        SceneManager.LoadScene(level);
    }
}
