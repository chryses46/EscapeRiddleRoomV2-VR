using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class AudioController : MonoBehaviour
    {
        [Tooltip("Door open sound clip.")]
        [SerializeField] private AudioClip doorOpening;
        [Tooltip("Door close sound clip.")]
        [SerializeField] private AudioClip doorClosing;

        AudioSource audioSource;

        void Awake()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        public void PlayDoorOpen()
        {
            if(!doorOpening)
            {
                Debug.Log("Please set a sound effect for Door Opening on the Audio Controller.");
            }
            else
            {
               audioSource.PlayOneShot(doorOpening); 
            }
        }
        public void PlayDoorClose()
        {
            if(!doorClosing)
            {
                Debug.Log("Please set a sound effect for Door Closing on the Audio Controller.");
            }
            else
            {
               audioSource.PlayOneShot(doorClosing); 
            }
        }

    }
}

