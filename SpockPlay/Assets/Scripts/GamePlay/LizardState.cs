using UnityEngine;

public class LizardState : ButtonAnswerStates
{
    #region OVERRIDE METHODS
    public override void ButtonPressed(AnswerState opponentAnswer)
    {
        Debug.Log("Answer pressed " + m_currentAnswerState);
        gameResult = (opponentAnswer.Equals(AnswerState.Paper) || opponentAnswer.Equals(AnswerState.Spock)) ? GameResult.Win : opponentAnswer.Equals(m_currentAnswerState) ? GameResult.Tie : GameResult.Lost;
    }

    public override void ButtonReset()
    {

    }
    #endregion
}
