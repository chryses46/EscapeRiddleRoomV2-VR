using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actors
{
    public class MainCameraDetails : MonoBehaviour
    {
        private Vector3 currentPosition;
        private Quaternion currentRotation;
        private Vector3 cachedPosition;
        private Quaternion cachedRotation;

        void Update()
        {
            currentPosition = gameObject.GetComponent<Transform>().position;
            currentRotation = gameObject.GetComponent<Transform>().rotation;
        }

        public Vector3 GetCurrentPosition()
        {
            return currentPosition;
        }

        public Quaternion GetCurrentRotation()
        {
            return currentRotation;
        }
    }
}

