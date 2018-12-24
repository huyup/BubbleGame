using System.Collections.Generic;
using UnityEngine.Events;

public class StageClearEvent : UnityEvent
{
}

public class StageManager : Singleton<StageManager>
{
    private StageClearEvent stageClearEvent = new StageClearEvent();

    public int UribouCount { get; private set; }
    public int HarinezumiCount { get; private set; }


    public void AddUribouCount()
    {
        UribouCount++;
    }

    public void AddHarinezumiCount()
    {
        HarinezumiCount++;
    }

    public void RemoveEnemyCount(ObjType _type)
    {
        if (_type == ObjType.Harinezemi)
            HarinezumiCount--;
        else if (_type == ObjType.Uribou)
            UribouCount--;

    }

    public int GetAllEnemyCount()
    {
        return UribouCount + HarinezumiCount;
    }
    public StageClearEvent StageClearEvent
    {
        get { return stageClearEvent; }
    }

    public void CallClear()
    {
        stageClearEvent.Invoke();
    }
}
