using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core;

namespace Game.Actors
{
    public class InteractiveObject : MonoBehaviour
    {

        private const int CORRESPONDING_BUTTON_VAL = 0;

        [Tooltip("Verb that occurs when interacted with.")]
        [SerializeField] private string message = "Do interactable thing";
        
        InputManager inputManager;

        private bool canInteract = false;

        private bool hasInteracted = false;

        void Awake()
        {
            inputManager = FindObjectOfType<InputManager>();
        }

        public void Update()
        {
            if(canInteract)
            {
                DisplayInteractMessage();

                if(inputManager.HandleInput() != null && inputManager.HandleInput().GetValue() == CORRESPONDING_BUTTON_VAL)
                {
                    hasInteracted = true;
                }
            }
        }

        private void DisplayInteractMessage()
        {
            Debug.Log("Press E to " + message);
        }

        public void SetInteractible()
        {
            canInteract = !canInteract;
        }

        public bool GetInteracted()
        {
            return hasInteracted;
        }

        public void Interacted()
        {
            hasInteracted = !hasInteracted;
        }

        public void SetInteractMessage(string message)
        {
            this.message = message;
        }

    }
}