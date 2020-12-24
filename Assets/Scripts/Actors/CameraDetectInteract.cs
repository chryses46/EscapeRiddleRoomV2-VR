using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actors
{
    public class CameraDetectInteract : MonoBehaviour
    {
        [SerializeField] private int interactiveDistance = 8;

        int layerMask = 1 << 8;
        bool interactFound = false;
        InteractiveObject currentInteracive;
        void FixedUpdate()
        {
                Vector3 fwd = transform.TransformDirection(Vector3.forward);

                RaycastHit hit;

                if (Physics.Raycast(transform.position, fwd, out hit, interactiveDistance, layerMask))
                {
                    if(!interactFound)
                    {
                        interactFound = true;
                        currentInteracive = hit.transform.gameObject.GetComponent<InteractiveObject>();
                        currentInteracive.SetInteractible();
                    }
                }
                else
                {
                    if(currentInteracive)
                    {
                        currentInteracive.SetInteractible();
                        currentInteracive = null;
                    }
                    
                    interactFound = false;
                }
        }
    }
}

