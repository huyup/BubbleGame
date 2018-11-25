using UnityEngine.Events;

public class StageClearEvent : UnityEvent
{
}

public class StageManager : Singleton<StageManager>
{
    private StageClearEvent stageClearEvent = new StageClearEvent();

    public StageClearEvent StageClearEvent
    {
        get { return stageClearEvent; }
    }

    public void CallClear()
    {
        stageClearEvent.Invoke();
    }
}
