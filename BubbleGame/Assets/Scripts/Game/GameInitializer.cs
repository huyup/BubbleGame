using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitializer : Singleton<GameInitializer>
{
    public bool IsInitialized { get; private set; }

    public IEnumerator InitializeCoroutine()
    {
        // 初期化呼ぶ
        yield return null;

        IsInitialized = true;
    }
    
    protected override void OnCreated()
    {
        base.OnCreated();
        GameSetting.CreateInstance();
    }

    protected override void OnDisposed()
    {
        // NOTE: 初期化と逆順に破棄する
        base.OnDisposed();
    }
}
