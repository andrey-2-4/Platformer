using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMovementModel
{
    private AudioClip jumpSound;
    private AudioClip[] stepSounds;
    private AudioSource stepsAudioSource;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public UIMovementModel(AudioClip jumpSound, AudioClip[] stepSounds, AudioSource stepsAudioSource, SpriteRenderer spriteRenderer, Animator animator)
    {
        this.jumpSound = jumpSound;
        this.stepSounds = stepSounds;
        this.stepsAudioSource = stepsAudioSource;
        this.spriteRenderer = spriteRenderer;
        this.animator = animator;
    }

    public void MoveLeft()
    { 
        spriteRenderer.flipX = true;
    }

    public void MoveRight()
    {
        spriteRenderer.flipX = false;
    }

    public void SetAnimationState(int state)
    {
        animator.SetInteger("state", state);
    }

    public void Step()
    {
        AudioClip clip = GetRandomStepSound();
        stepsAudioSource.PlayOneShot(clip);
    }

    private AudioClip GetRandomStepSound()
    {
        return stepSounds[UnityEngine.Random.Range(0, stepSounds.Length)];
    }
}
