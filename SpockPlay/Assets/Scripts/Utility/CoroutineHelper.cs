using System.Collections;
using UnityEngine;

public class CoroutineHelper : MonoBehaviourSingletonPersistent<CoroutineHelper>
{
    public static Coroutine IStartCoroutine(IEnumerator enumerator)
    {
        return Instance.StartCoroutine(enumerator);
    }

    public static void IStopCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            Instance.StopCoroutine(coroutine);
        }
    }
}
