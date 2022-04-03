using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager 
{
    private static GameStateManager _instance;
    public static GameStateManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameStateManager();
            }
            return _instance;
        }
    }

    public GameState CurrentGameState { get; private set; }

    public delegate void GameStateChangeHandler(GameState newGameState);
    public event GameStateChangeHandler OnGameStateChanged;

    //constructor
    private GameStateManager()
    {

    }
    // set game state
    public void SetState(GameState newGameState)
    {
        if (newGameState == CurrentGameState)
        {
            return;
        }
        CurrentGameState = newGameState;
        // check if this function null or not
        OnGameStateChanged?.Invoke(newGameState);
    }
}
