using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    [SerializeField] AudioClip[] bgTracks;
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        PlayNextSong();
    }

    private void PlayNextSong()
    {
        AudioClip clip = GetRandomBGTrack();
        audioSource.PlayOneShot(clip);
        Invoke("PlayNextSong", clip.length);
    }

    private AudioClip GetRandomBGTrack()
    {
        return bgTracks[UnityEngine.Random.Range(0, bgTracks.Length)];
    }
}
