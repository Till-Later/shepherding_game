using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class audioManager : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip audioClipGameplay;
    public AudioClip audioClipDeath;
    public AudioClip audioClipSad;
    public AudioClip audioClipHappy;

    void Start() {
        audioSource.Play();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.D)) {
            Death();
        } else if (Input.GetKeyDown(KeyCode.H)) {
            Happy();
        } else if (Input.GetKeyDown(KeyCode.S)) {
            Sad();
        }
    }

    public void Death() {
        audioSource.clip = audioClipDeath;
        audioSource.Play();
    }
    public void Happy() {
        audioSource.clip = audioClipSad;
        audioSource.Play();
    }
    public void Sad() {
        audioSource.clip = audioClipHappy;
        audioSource.Play();
    }

}
