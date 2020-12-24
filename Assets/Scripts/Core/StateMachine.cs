using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
    public class StateMachine : MonoBehaviour
    {
        private enum GameState {PLAY, PUZZLE, PAUSE};

        GameState currentGameState = GameState.PLAY;

        public string GetGameState()
        {
            switch(currentGameState)
            {
                case GameState.PLAY:
                    return "Play";
                case GameState.PUZZLE:
                    return "Puzzle";
                case GameState.PAUSE:
                    return "Pause";
            }

            return null;
        }

        public void SetGameState(string requestedState)
        {
            if(requestedState == "Play")
            {
                currentGameState = GameState.PLAY;
                Debug.Log("The game state has been set to PLAY.");
            }
            else if(requestedState == "Puzzle")
            {
                currentGameState = GameState.PUZZLE;
                Debug.Log("The game state has been set to PUZZLE.");
            }
            else if(requestedState == "Pause")
            {
                currentGameState = GameState.PAUSE;
                Debug.Log("The game state has been set to PAUSE.");
            }
            else
            {
                Debug.Log("The given string " + requestedState + "is not a valid state.");
            }
        }
    }
}

