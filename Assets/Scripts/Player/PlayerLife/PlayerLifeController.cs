using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLifeController : MonoBehaviour
{
    [SerializeField] private Image blackScreen;

    [SerializeField] private Image eyes;
    [SerializeField] private Sprite eyesClosed;
    [SerializeField] private Sprite eyesOpened;

    [SerializeField] private AudioClip abilitySound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private Rigidbody2D playerBody;
    private Animator animator;

    private float lastUse = 0f;
    [SerializeField] private float cooldown = 10f;

    private PlayerLifeModel playerLifeModel;
    private UILifeModel uiLifeModel;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerLifeModel = new PlayerLifeModel(playerBody, animator);

        uiLifeModel = new UILifeModel(blackScreen, eyes, eyesClosed, eyesOpened,
            abilitySound, deathSound, hurtSound, audioSource, hearts, 
            fullHeart, emptyHeart, cooldown);
        uiLifeModel.UpdateHearts(playerLifeModel.Health);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time - uiLifeModel.lastUse > uiLifeModel.cooldown)
        {
            uiLifeModel.OpenEyes();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                uiLifeModel.lastUse = Time.time;
                // Возможно только в MonoBehaviour классе
                StartCoroutine(GetBlind());
            }
        }
        else
        {
            uiLifeModel.CloseEyes();
        }
    }

    private IEnumerator GetBlind()
    {
        uiLifeModel.PlayAbilitySound();
        // 7 - Player layer
        // 8 - Skeleton layer
        playerLifeModel.TurnOffCollision();
        uiLifeModel.TurnOnBlackScreen();
        yield return new WaitForSeconds(3);
        playerLifeModel.TurnOnCollision();
        uiLifeModel.TurnOffBlackScreen();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            playerLifeModel.Die();
            uiLifeModel.PlayDeathSound();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Skeleton"))
        {
            GetAttacked();
            Debug.Log("Skeleton");
            uiLifeModel.UpdateHearts(playerLifeModel.Health);
        }
    }

    private IEnumerator GetHurt()
    {
        uiLifeModel.PlayHurtSound();
        // 7 - Player layer
        // 8 - Skeleton layer
        // Physics2D.IgnoreLayerCollision(7, 8, true);
        // 1 - layer of Player_GetHurt in Animator
        playerLifeModel.TurnOnHurtAnimation();
        yield return new WaitForSeconds(1f);
        // Physics2D.IgnoreLayerCollision(7, 8, false);
        playerLifeModel.TurnOffHurtAnimation();
    }

    private void GetAttacked()
    {
        --playerLifeModel.Health;
        if (playerLifeModel.Health <= 0)
        {
            playerLifeModel.Die();
            uiLifeModel.PlayDeathSound();
        }
        else
        {
            // Возможно только в MonoBehaviour классе
            StartCoroutine(GetHurt());
        }
    }

    // Executes in the end of the death animation (which is triggered by "death")
    // animator.SetTrigger("death");
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
