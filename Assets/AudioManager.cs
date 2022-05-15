using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audios;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = audios[Random.RandomRange(0, audios.Count)];
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
