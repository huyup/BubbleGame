using System.Collections.Generic;
using UnityEngine;

public class StageMain : MainBase
{
    public override SceneType SceneType
    {
        get { return SceneType.Stage; }
    }

    protected override void OnFinishedInitialize()
    {
    }
    protected override void OnStart()
    {
        StageManager.CreateInstance();
    }

    protected override void OnUpdate()
    {
        Debug.Log("Num" + StageManager.Instance.GetAllEnemyCount());
    }

    protected override void OnLateUpdate()
    {
    }

    protected override void OnFixedUpdate()
    {
    }

    protected override void OnDestroyEvent()
    {
    }

    protected override void OnApplicationQuitEvent()
    {
    }
}
