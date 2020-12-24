using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;


namespace Game.Actors
{
    public class Door : MonoBehaviour
    {
        [Tooltip("The parent object of the door; the pivot point.")]
        [SerializeField] private GameObject pivotPoint;

        [SerializeField] private string doorCloseMessage;
        [SerializeField] private string doorOpenMessage;

        AudioController audioController;
        InteractiveObject interactiveObject;

        bool isDoorOpen = false;

        void Awake()
        {
            audioController = FindObjectOfType<AudioController>();
            interactiveObject = gameObject.GetComponent<InteractiveObject>();
        }

        void Update()
        {
            if(interactiveObject.GetInteracted())
            {
                InteractAction();
                interactiveObject.Interacted();
            }
        }
        public void InteractAction()
        {
            if(!isDoorOpen)
            {
                //Open door
                audioController.PlayDoorOpen();
                pivotPoint.GetComponent<Animator>().SetTrigger("doorOpen");
                interactiveObject.SetInteractMessage(doorCloseMessage);
                isDoorOpen = true;
            }
            else
            {
                //Close door
                audioController.PlayDoorClose();
                pivotPoint.GetComponent<Animator>().SetTrigger("doorClose");
                interactiveObject.SetInteractMessage(doorOpenMessage);
                isDoorOpen = false;
            } 
        }
    }
}

