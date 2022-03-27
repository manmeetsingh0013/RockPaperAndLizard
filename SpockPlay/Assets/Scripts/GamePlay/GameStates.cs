using UnityEngine;

public abstract class GameStates : MonoBehaviour
{
    public virtual void EnterGameState() { }

    public virtual void ExitGameState() { }
}
