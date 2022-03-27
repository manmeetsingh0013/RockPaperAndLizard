using UnityEngine;
using UnityEngine.UI;

public class GameMenuState : GameStates
{
    [SerializeField] Text m_scoreText;

    #region OVERRIDE METHODS
    public override void EnterGameState()
    {
        m_scoreText.text = PlayerPrefs.HasKey(GameManager.HIGHEST_SCORE_KEY) ? PlayerPrefs.GetInt(GameManager.HIGHEST_SCORE_KEY).ToString() : "--";
        gameObject.SetActive(true);
    }
    public override void ExitGameState()
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region PUBLIC METHODS

    public void OnPlayClicked()
    {
        GameManager.Instance.SetGameState(GameState.Game);
    }

    #endregion
}
