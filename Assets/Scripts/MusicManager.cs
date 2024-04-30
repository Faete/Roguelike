using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioClip dungeonMusic;
    [SerializeField] AudioClip bossMusic;
    private AudioSource source;

    void Start(){
        source = GetComponent<AudioSource>();
        source.clip = dungeonMusic;
        source.Play();
    }

    void Update(){
        source.volume = PlayerPrefs.GetFloat("Volume");
    }

    public void BossRoomEnter(){
        source.Stop();
        source.clip = bossMusic;
        source.Play();
    }
}
