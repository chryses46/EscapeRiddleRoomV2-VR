using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Actors;
using Game.Core;
using Cinemachine;

namespace Game.Puzzles
{
    public class CandleCamera : MonoBehaviour
    {   
        [Tooltip("The entry blend list camera for this puzzle's camera.")]
        [SerializeField] private CinemachineBlendListCamera entryBlendListCamera;
        [Tooltip("The exit blend list camera for this puzzle's camera.")]
        [SerializeField] private CinemachineBlendListCamera exitBlendListCamera;
        [Tooltip("The first vcam in the entry blend list.")]
        [SerializeField] private CinemachineVirtualCamera firstVcam;
        [Tooltip("The second vcam in the entry blend list.")]
        [SerializeField] private CinemachineVirtualCamera secondVcam;
        [Tooltip("The first vcam in the exit blend list.")]
        [SerializeField] private CinemachineVirtualCamera thirdVcam;
        [Tooltip("The last vcam in the exit blend list.")]
        [SerializeField] private CinemachineVirtualCamera fourthVcam;
        [Tooltip("The main camera.")]
        [SerializeField] private Camera mainCamera;

        Vector3 mainCamPositionCache;
        Quaternion mainCamRotationCache;
        Camera dollyCam;
        private bool cameraAction = false;

        public void GoToCandleCamera()
        {
            // First clone the first person camera's position and rotation
            mainCamPositionCache = mainCamera.GetComponent<MainCameraDetails>().GetCurrentPosition();
            mainCamRotationCache = mainCamera.GetComponent<MainCameraDetails>().GetCurrentRotation();

            dollyCam = Instantiate(mainCamera, mainCamPositionCache, mainCamRotationCache);

            // Clean up the clone's components
            Destroy(dollyCam.GetComponent<FlareLayer>());
            Destroy(dollyCam.GetComponent<CameraDetectInteract>());
            Destroy(dollyCam.GetComponent<MainCameraDetails>());

            // Rename Clone
            dollyCam.name = "dollyCam";

            // Add CinemachineBrain to dollyCam
            dollyCam.gameObject.AddComponent<CinemachineBrain>();

            // Set Vcam to dollyCam pos and rot
            firstVcam.transform.position = dollyCam.transform.position;
            firstVcam.transform.rotation = dollyCam.transform.rotation;

            // Disable the mainCamera
            mainCamera.gameObject.SetActive(false);

            // Move that camera from it's position to the desired angle
            entryBlendListCamera.gameObject.SetActive(true);

            StartCoroutine(PostEntryBlendAction());

            // Set the game state to Puzzle 
            FindObjectOfType<StateMachine>().SetGameState("Puzzle");
        }

        public void ReturnCameraControl()
        {
            entryBlendListCamera.gameObject.SetActive(false);
            
            // Set vCams of exitBlendList
            thirdVcam.transform.position = secondVcam.transform.position;
            thirdVcam.transform.rotation = secondVcam.transform.rotation;

            fourthVcam.transform.position = mainCamPositionCache;
            fourthVcam.transform.rotation = mainCamRotationCache;
            
            // Enable BlendListCamera, beginning 
            exitBlendListCamera.gameObject.SetActive(true);
            
            StartCoroutine(PostExitBlendAction());

            FindObjectOfType<StateMachine>().SetGameState("Play");
        }

        IEnumerator PostEntryBlendAction()
        {
            // Lock controls while blending
            FindObjectOfType<InputManager>().SetIsControlLockedOut(true);

            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => !entryBlendListCamera.IsBlending);

            // Release controls after blending
            FindObjectOfType<InputManager>().SetIsControlLockedOut(false);
            // Disable the entryBlendListCamera
            entryBlendListCamera.gameObject.SetActive(false);
            // Select default candle after transition
            gameObject.GetComponent<CandlePuzzle>().SetCandleHighlighted();
        }

        IEnumerator PostExitBlendAction()
        {
            // Lock controls while blending
            FindObjectOfType<InputManager>().SetIsControlLockedOut(true);

            yield return new WaitForSeconds(1);
            yield return new WaitUntil(() => !exitBlendListCamera.IsBlending);

            // Release controls after blending
            FindObjectOfType<InputManager>().SetIsControlLockedOut(false);
            // Disable the exitBlendListCamera
            exitBlendListCamera.gameObject.SetActive(false);
             // Enable maincam
            mainCamera.gameObject.SetActive(true);
            // Destroy dollyCam
            Destroy(dollyCam.gameObject);

        }
    }
}

