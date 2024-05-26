using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ContinueGameHandler : MonoBehaviour
{
    [SerializeField] int levelsTotal = 2;
    [SerializeField] GameObject panel;

    public void ContinueGame()
    {
        var level = SaveSystem.LoadLevel();
        if (level > levelsTotal)
        {
            Debug.Log("game completed");
            panel.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(level);
        }
    }
}
