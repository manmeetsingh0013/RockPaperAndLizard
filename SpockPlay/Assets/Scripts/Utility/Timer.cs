using System.Collections;
using UnityEngine;

public class Timer : GetBase<Timer>
{
    private System.Action OnTick;
    private System.Action OnComplete;
    private Coroutine coroutine;
    private bool repeat;

    public Timer() { }

    public Timer StartTimer(float time, bool repeat = false, System.Action OnTick = null, System.Action OnComplete = null)
    {
        this.OnTick = OnTick;
        this.OnComplete = OnComplete;
        this.repeat = repeat;
        coroutine = CoroutineHelper.IStartCoroutine(IStartTimer(time));
        return this;
    }

    private IEnumerator IStartTimer(float time)
    {
        float endTime = Time.realtimeSinceStartup + time;
        while (Time.realtimeSinceStartup < endTime)
        {
            OnTick?.Invoke();
            yield return null;
        }
        OnComplete?.Invoke();
        if (repeat)
        {
            coroutine = CoroutineHelper.IStartCoroutine(IStartTimer(time));
        }
    }

    public void EndTimer()
    {
        CoroutineHelper.IStopCoroutine(coroutine);
        repeat = false;
    }

    public static void DelayedExecute(float time, System.Action OnComplete)
    {
        CoroutineHelper.IStartCoroutine(IDelayedExecute(time, OnComplete));
    }

    private static IEnumerator IDelayedExecute(float time, System.Action OnComplete)
    {
        yield return new WaitForSecondsRealtime(time);
        OnComplete?.Invoke();
    }
}
