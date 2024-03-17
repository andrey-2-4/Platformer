using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float delayBeforeLoading = 0.35f;

    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject[] appleObjects = GameObject.FindGameObjectsWithTag("Apple");
        if (collision.gameObject.name == "Player" && !levelCompleted && appleObjects.Length == 0)
        {
            audioSource.Play();
            levelCompleted = true;
            Invoke("CompleteLevel", delayBeforeLoading);
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
