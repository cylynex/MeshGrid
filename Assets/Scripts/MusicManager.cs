using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    
    private AudioSource bgMusic;

    void Awake() {
        bgMusic = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    void OnLevelWasLoaded(int level) {
        if (bgMusic) {
            bgMusic.loop = true;
            bgMusic.Play();
        }
    }

    public void ChangeVolume(float volume) {
        bgMusic.volume = volume;
    }
}
