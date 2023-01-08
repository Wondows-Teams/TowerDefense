using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips; 
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectAudio(int idx, int volume)
    {
        audioSource.PlayOneShot(audioClips[idx], volume);
    }
}
