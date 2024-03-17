using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioSource audioSource;

    private int health = 3;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private Rigidbody2D playerBody;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Skeleton"))
        {
            GetAttacked();
            Debug.Log("Skeleton");
            updateHearts();
        }
    }

    private void updateHearts()
    {
        foreach (Image heart in hearts)
        {
            heart.sprite = emptyHeart;
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }

    private IEnumerator GetHurt()
    {
        // 7 - Player layer
        // 8 - Skeleton layer
        Physics2D.IgnoreLayerCollision(7, 8, true);
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

    private void GetAttacked()
    {
        --health;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(GetHurt());
        }
    }

    private void Die()
    {
        playerBody.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
        audioSource.PlayOneShot(deathSound);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
