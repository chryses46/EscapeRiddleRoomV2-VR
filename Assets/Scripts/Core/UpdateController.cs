using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class UpdateController : MonoBehaviour
    {
        InputManager inputManager;
        void Awake()
        {
           inputManager = gameObject.GetComponent<InputManager>();
        }
        
        void Update()
        {
            inputManager.HandleInput();
        }
    }
}

