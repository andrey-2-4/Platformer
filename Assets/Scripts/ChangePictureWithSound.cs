using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePictureWithSound : MonoBehaviour
{
    [SerializeField] private float delay = 1f;

    [SerializeField] private Image image;
    bool newSpriteSelected = false;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private AudioSource audioSource;

    public void SetNewImageWithSound()
    {
        audioSource.Play();
        if (!newSpriteSelected)
        {
            image.sprite = sprite;
            newSpriteSelected = true;
        } 
        else
        {
            image.sprite = sprite2;
            newSpriteSelected = false;
        }
        Invoke("Delay", delay);
    }

    private void Delay()
    {

    }
}
