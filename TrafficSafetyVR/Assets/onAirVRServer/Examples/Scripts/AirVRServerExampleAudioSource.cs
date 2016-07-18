using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class AirVRServerExampleAudioSource : MonoBehaviour {
    private AudioSource[] _audioSources;

    void Awake() {
        _audioSources = GetComponentsInChildren<AudioSource>();
    }

    public void Play() {
        foreach (AudioSource audioSource in _audioSources) {
            audioSource.Play();
        }
    }

    public void Stop() {
        foreach (AudioSource audioSource in _audioSources) {
            audioSource.Stop();
        }
    }
}
