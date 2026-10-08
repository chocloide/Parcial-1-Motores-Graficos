using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //SerializeField Permite que la variable sea visible en el inspector y que sea privada
    [SerializeField] private AudioClip[] audios; 
    private AudioSource audioSource;
    
    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(int id , float volumen = 0.8f){
        audioSource.PlayOneShot(audios[id], volumen);
    }


}
