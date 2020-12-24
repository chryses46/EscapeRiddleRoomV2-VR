using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Actors;
using Game.Core;
using UnityStandardAssets.Characters.FirstPerson;
using System;

namespace Game.Puzzles
{
    public class CandlePuzzle : MonoBehaviour
    {
        [SerializeField] private static GameObject firstCandle;
        [SerializeField] private static GameObject secondCandle;
        [SerializeField] private static GameObject thirdCandle;
        [SerializeField] private static GameObject fourthCandle;

        [SerializeField] FirstPersonController firstPersonController;
        InteractiveObject interactiveObject;
        StateMachine stateMachine;
        InputManager inputManager;
        private const int ESC_BUTTON_VAL = 9;
        private const int INTERACT_BUTTON_VAL = 0;
        private const int MOVE_BUTTON_VAL = 1;
        private bool candleIsChosen = false;
        private bool candleHighlighted = false;
        private Transform[] candlePositions = new Transform[4]
        {
            firstCandle.transform,
            secondCandle.transform,
            thirdCandle.transform,
            fourthCandle.transform
        };

        Transform currentCandlePosition;
        
        void Awake()
        {
            interactiveObject = gameObject.GetComponent<InteractiveObject>();
            inputManager = FindObjectOfType<InputManager>();
            stateMachine = FindObjectOfType<StateMachine>();
            currentCandlePosition = candlePositions[0];

        }

        void Update()
        {
            if(interactiveObject.GetInteracted())
            {
                InteractAction();
                firstPersonController.ToggleMovementLocked();
            }

            if(stateMachine.GetGameState() == "Puzzle")
            {
                if(!candleIsChosen && inputManager.HandleInput() !=null && inputManager.HandleInput().GetValue() == ESC_BUTTON_VAL)
                {
                    EscapeAction();
                    firstPersonController.ToggleMovementLocked();
                }

                if(candleHighlighted)
                {

                    if(inputManager.HandleInput() != null && inputManager.HandleInput().GetValue() == MOVE_BUTTON_VAL)
                    {
                        //move the cursor/ hand
                    }
                  if(inputManager.HandleInput() !=null && inputManager.HandleInput().GetValue() == INTERACT_BUTTON_VAL)
                    {
                        CandleConfirm();
                    }  
                }
            }
        }

        private void CandleConfirm()
        {
            Debug.Log("Select the candle");
            candleIsChosen = !candleIsChosen;
        }

        private void CandleMove()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            if(Mathf.Abs(verticalInput) > 0 && Mathf.Abs(horizontalInput) > 0)
            {
                if(Mathf.Abs(verticalInput) > Mathf.Abs(horizontalInput))
                {
                    horizontalInput = 0;
                }
                else
                {
                    verticalInput = 0;
                }
            }
        }

        public void InteractAction()
        {
            gameObject.GetComponent<CandleCamera>().GoToCandleCamera();
            interactiveObject.Interacted();
            interactiveObject.SetInteractible();
        }

        public void EscapeAction()
        {
            gameObject.GetComponent<CandleCamera>().ReturnCameraControl();
            interactiveObject.SetInteractible();
        }

        public void SetCandleHighlighted()
        {
            candleHighlighted = !candleHighlighted;
        }
    }
}

