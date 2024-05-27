using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameHandler : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("exiting");
        Application.Quit();
    }
}
