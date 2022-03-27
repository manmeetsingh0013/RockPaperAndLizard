using UnityEngine;

public abstract class ButtonAnswerStates : MonoBehaviour
{
    public AnswerState m_currentAnswerState;
    public GameResult gameResult;

    public virtual void ButtonPressed(AnswerState opponentAnswer) { }

    public virtual void ButtonReset() { }

    public GameResult GetGameResult()
    {
        return gameResult;
    }
        
}
