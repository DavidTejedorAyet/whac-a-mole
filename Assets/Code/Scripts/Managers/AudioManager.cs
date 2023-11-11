using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip[] painSounds;
    public AudioClip hammerHitSound;

    private AudioSource backgroundAudioSource;
    private AudioSource audioSource;
    public static AudioManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        backgroundAudioSource = GetComponents<AudioSource>()[0];
        audioSource = GetComponents<AudioSource>()[1];
    }

    public void PlayPainAudio() {
        int randomIndex = Random.Range(0, painSounds.Length);
        audioSource.PlayOneShot(painSounds[randomIndex]);
    }

    public void PlayHammerHitAudio() {
        audioSource.PlayOneShot(hammerHitSound);
    }
}
