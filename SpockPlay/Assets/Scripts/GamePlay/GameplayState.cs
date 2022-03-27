using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayState : GameStates
{
    #region EXPOSED FIELDS
    [Space(10)]
    [Header("All answer state refeneces")]
    [SerializeField] List<ButtonAnswerStates> m_allAnswerStates;

    [Space(10)]
    [SerializeField] Opponent m_opponent;
    [SerializeField] Slider m_slider;

    [Space(10)]
    [SerializeField] GameObject m_answerPanel;
    [SerializeField] GameObject m_RoundPanel;
    [SerializeField] GameObject m_blocker;

    [Space(10)]
    [SerializeField] Text m_answerText;
    [SerializeField] Text m_timerText;
    [SerializeField] Text m_ResultText;
    #endregion

    Coroutine resultCompletionCoroutine;
    GameResult currentRoundResult;
    Timer timer;
    int roundWinCount = 0;

    #region OVERRIDE METHODS

    public override void EnterGameState()
    {
        gameObject.SetActive(true);
        timer = Timer.Get();
        GameRoundStart();
    }
    public override void ExitGameState()
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region PRIVATE METHODS

    private void GameRoundStart()
    {
        m_RoundPanel.SetActive(false);
        m_blocker.SetActive(false);
        m_opponent.HideAnswerPanel();
        m_answerPanel.SetActive(false);
        float startTime = Time.realtimeSinceStartup;
        timer.StartTimer(GameManager.MAX_TIME_FOR_ROUND,
            OnTick: () => ShowTime(GameManager.MAX_TIME_FOR_ROUND - (Time.realtimeSinceStartup - startTime)),
            OnComplete: TimeUp);
    }

    private void ShowTime(float time)
    {
        m_slider.value =1- (time/ GameManager.MAX_TIME_FOR_ROUND);
        m_timerText.text = TimeSpan.FromSeconds((double)time).ToString(@"ss");
    }
    private void TimeUp()
    {
        m_timerText.text = GameManager.COMPLETED;
        currentRoundResult = GameResult.Lost;
        CoroutineHelper.IStopCoroutine(resultCompletionCoroutine);
        CoroutineHelper.IStartCoroutine(OnResultCompletion());
    }
    private void GameEnd()
    {
        SetScore();
        GameManager.Instance.SetGameState(GameState.Menu);
    }
    private void SetScore()
    {
        int previousScore = PlayerPrefs.GetInt(GameManager.HIGHEST_SCORE_KEY);
        int score = roundWinCount * GameManager.SCORE_FOR_EACH_ROUND;
        if(score > previousScore)
            PlayerPrefs.SetInt(GameManager.HIGHEST_SCORE_KEY, score);
    }
    private IEnumerator OnResultCompletion()
    {
        yield return new WaitForSeconds(1f);
        m_RoundPanel.SetActive(true);
        if (currentRoundResult.Equals(GameResult.Lost))
        {
            m_ResultText.text = GameManager.ROUND_LOST_MSG;
            yield return new WaitForSeconds(3);
            GameEnd();
        }
        else
        {
            m_ResultText.text = currentRoundResult.Equals(GameResult.Win) ? GameManager.ROUND_WIN_MSG : GameManager.ROUND_TIED_MSG;
            roundWinCount = currentRoundResult.Equals(GameResult.Win) ? ++roundWinCount : roundWinCount;
            yield return new WaitForSeconds(3);
            GameRoundStart();
        }
    }
    private void OnApplicationQuit()
    {
        SetScore();
    }
    #endregion

    #region PUBLIC METHODS
    public void ButtonClicked(int state)
    {
        timer.EndTimer();
        m_blocker.SetActive(true);
        m_answerText.text = ((AnswerState)state).ToString();
        m_answerPanel.SetActive(true);
        m_allAnswerStates[state].ButtonPressed(m_opponent.PerformHandAndReturnAnswer());
        currentRoundResult = m_allAnswerStates[state].GetGameResult();
        CoroutineHelper.IStopCoroutine(resultCompletionCoroutine);
        resultCompletionCoroutine= CoroutineHelper.IStartCoroutine(OnResultCompletion());
    }
    #endregion
}
