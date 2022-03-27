using UnityEngine;
using UnityEngine.UI;

public class Opponent : MonoBehaviour
{
    #region EXPOSED FIELD

    [SerializeField] GameObject m_answerPanel;
    [SerializeField] Text m_answerText;

    #endregion

    #region PRIVATE FIELDS

    AnswerState answerState;

    #endregion
    public AnswerState PerformHandAndReturnAnswer()
    {
        answerState = (AnswerState)(Random.Range((int)AnswerState.Rock, (int)AnswerState.Spock));
        m_answerText.text = answerState.ToString();
        m_answerPanel.SetActive(true);
        return answerState; 
    }
    public void HideAnswerPanel()
    {
        m_answerPanel.SetActive(false);
    }
}
