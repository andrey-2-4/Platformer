using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private Image blackScreen;

    [SerializeField] private Image eyes;
    [SerializeField] private Sprite eyesClosed;
    [SerializeField] private Sprite eyesOpened;

    [SerializeField] private AudioClip abilitySound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioSource audioSource;

    private int health = 3;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private Rigidbody2D playerBody;
    private Animator animator;

    private float lastUse = 0f;
    [SerializeField] private float cooldown = 10f;

    // Start is called before the first frame update
    void Start()
    {
        health = PlayerPrefs.GetInt("health", 3);
        if (health < 1 || health > 3)
        {
            health = 3;
        }
        updateHearts();
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time - lastUse > cooldown)
        {
            eyes.sprite = eyesOpened;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                lastUse = Time.time;
                StartCoroutine(GetBlind());
            }
        }
        else
        {
            eyes.sprite = eyesClosed;
        }
    }

    private IEnumerator GetBlind()
    {
        audioSource.PlayOneShot(abilitySound);
        // 7 - Player layer
        // 8 - Skeleton layer
        Physics2D.IgnoreLayerCollision(7, 8, true);
        blackScreen.color = new Color(0, 0, 0, 255);
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(7, 8, false);
        blackScreen.color = new Color(0, 0, 0, 0);
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
        audioSource.PlayOneShot(hurtSound);
        // 7 - Player layer
        // 8 - Skeleton layer
        Physics2D.IgnoreLayerCollision(7, 8, true);
        // 1 - layer of Player_GetHurt in Animator
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        Physics2D.IgnoreLayerCollision(7, 8, false);
        GetComponent<Animator>().SetLayerWeight(1, 0);
    }

    private void GetAttacked()
    {
        --health;
        PlayerPrefs.SetInt("health", health);
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
        health = 3;
        PlayerPrefs.SetInt("health", health);
        playerBody.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
        audioSource.PlayOneShot(deathSound);
    }

    // Executes in the end of the death animation (which is triggered by "death")
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
