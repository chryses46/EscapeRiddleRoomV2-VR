using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class InputManager : MonoBehaviour
    {
        StateMachine stateMachine;

        [Tooltip("The name of the interact button")]
        [SerializeField] private string interactButtonName = "interact";
        [Tooltip("The value of the puzzle interact button")]
        [SerializeField] private string puzzleInteractButtonName = "interact";
        [Tooltip("The value of the interact button")]
        [SerializeField] private int interactButtonValue = 0;
        [Tooltip("The value of the puzzle interact button")]
        [SerializeField] private int puzzleInteractButtonValue = 0;
        [Tooltip("The value of the puzzle move button(s)")]
        [SerializeField] private int puzzleMoveButtonVale = 1;
        [Tooltip("The name of the escape button")]
        [SerializeField] private string escapeButtonName = "escape";
        [Tooltip("The value of the escape button")]
        [SerializeField] private int escapeButtonValue = 9;
        
        private bool isControlLockedOut = false;

        void Awake()
        {
            stateMachine = gameObject.GetComponent<StateMachine>();
        }

        public ButtonPress HandleInput()
        {
            if(!isControlLockedOut)
            {
               if(Input.GetButtonDown("Fire1"))
                {
                    ButtonPress button = new ButtonPress(interactButtonName, interactButtonValue);
                    return button;
                } 

                if(Input.GetButtonDown("Cancel"))
                {
                    ButtonPress button = new ButtonPress(escapeButtonName, escapeButtonValue);
                    return button;
                } 

                return null;
            }
            
            if(stateMachine.GetGameState() == "Puzzle")
            {
                if(Input.GetButtonDown("Fire1"))
                {
                 ButtonPress button = new ButtonPress(puzzleInteractButtonName,puzzleInteractButtonValue);
                 return button;
                }
                

            }

            return null;
        }
        
        public bool GetIsControlLockedOut()
        {
            return isControlLockedOut;
        }

        public void SetIsControlLockedOut(bool isLocked)
        {
            isControlLockedOut = isLocked;
        }


        public class ButtonPress
        {
            private string name;

            private int value;

            public ButtonPress(String name, int value)
            {
                this.name = name;
                this.value = value;
            }

            public string GetName()
            {
                return name;
            }

            public int GetValue()
            {
                return value;
            }
        }
    }
}