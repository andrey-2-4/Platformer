using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILifeModel
{

    private Image blackScreen;
    private Image eyes;
    private Sprite eyesClosed;
    private Sprite eyesOpened;

    private AudioClip abilitySound;
    private AudioClip deathSound;
    private AudioClip hurtSound;
    private AudioSource audioSource;

    private Image[] hearts;
    private Sprite fullHeart;
    private Sprite emptyHeart;

    public float lastUse = 0f;
    public float cooldown = 10f;

    public UILifeModel(Image blackScreen, Image eyes, Sprite eyesClosed, 
        Sprite eyesOpened, AudioClip abilitySound, AudioClip deathSound, 
        AudioClip hurtSound, AudioSource audioSource,
        Image[] hearts, Sprite fullHeart, Sprite emptyHeart, float cooldown)
    {
        this.blackScreen = blackScreen;
        this.eyes = eyes;
        this.eyesClosed = eyesClosed;
        this.eyesOpened = eyesOpened;
        this.abilitySound = abilitySound;
        this.deathSound = deathSound;
        this.hurtSound = hurtSound;
        this.audioSource = audioSource;
        this.hearts = hearts;
        this.fullHeart = fullHeart;
        this.emptyHeart = emptyHeart;
        this.cooldown = cooldown;
    }

    public void UpdateHearts(int health)
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

    public void OpenEyes()
    {
        eyes.sprite = eyesOpened;
    }

    public void CloseEyes()
    {
        eyes.sprite = eyesClosed;
    }

    public void PlayAbilitySound()
    {
        audioSource.PlayOneShot(abilitySound);
    }

    public void TurnOnBlackScreen()
    {
        blackScreen.color = new Color(0, 0, 0, 255);
    }

    public void TurnOffBlackScreen()
    {
        blackScreen.color = new Color(0, 0, 0, 0);
    }

    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathSound);
    }

    public void PlayHurtSound()
    {
        audioSource.PlayOneShot(hurtSound);
    }
}
