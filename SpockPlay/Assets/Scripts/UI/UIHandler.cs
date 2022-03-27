using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    #region EXPOSED FIELDS
    [SerializeField] List<GameStates> m_allGameStates;
    #endregion

    #region PUBLIC FIELDS
    public GameState currentGameState { get; private set; }
    #endregion

    private void Start()
    {
        GameManager.Instance.SetUIReference(this);
    }
    public void SetGameState(GameState state)
    {
        m_allGameStates[(int)currentGameState].ExitGameState();
        currentGameState = state;
        m_allGameStates[(int)currentGameState].EnterGameState();
    }
}
