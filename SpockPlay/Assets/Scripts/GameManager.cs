using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Menu,
    Game,
    Result
}
public enum AnswerState
{
    Rock,
    Paper,
    Scissors,
    Lizard,
    Spock
}
public enum GameResult
{
    Win,
    Lost,
    Tie
}
public class GameManager : MonoBehaviourSingletonPersistent<GameManager>
{
    #region PRIVATE FIELDS

    public UIHandler uIHandler;
    
    #endregion

    #region CONST FIELDS

    public const string SHOW = "SHOW";
    public const string HIDE = "HIDE";
    public const string COMPLETED = "Completed";
    public const string ROUND_WIN_MSG = "Round Win.\nMoving to next round!\nGo for it Again.";
    public const string ROUND_TIED_MSG = "Round Tied.\nMoving to next round!\nGo for it Again.";
    public const string ROUND_LOST_MSG = "Round Lost.\nGive one more shot!\n Starting from menu.";
    public const string HIGHEST_SCORE_KEY = "HighestScore";
    public const int SCORE_FOR_EACH_ROUND = 10;
    public const float MAX_TIME_FOR_ROUND = 10;
    #endregion

    #region PRIVATE METHODS

    protected override void Awake()
    {
        base.Awake();
    }
    private void InitializeGame()
    {
        uIHandler.SetGameState(GameState.Menu);
        SetGameState(GameState.Menu);
    }

    #endregion

    #region PUBLIC METHODS
    public void SetUIReference(UIHandler handler)
    {
        uIHandler = handler;
        InitializeGame();
    }
    public void SetGameState(GameState state)
    {
        uIHandler.SetGameState(state);
    }
    public GameState GetGameState()
    {
        return uIHandler.currentGameState;
    }
    #endregion
}
