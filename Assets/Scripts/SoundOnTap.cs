using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOnTap : MonoBehaviour
{
    public AudioSource auSource;
    public AudioClip audioClip;
    
    private void Awake()
    {
        auSource = Camera.main.GetComponent<AudioSource>();
       GetComponent<Button>().onClick.AddListener( ()=>auSource.PlayOneShot(audioClip));
    }
}
