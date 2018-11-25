using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class MainBase : MonoBehaviour
{
    private bool hasStarted;
    private string nextSceneName;

    public abstract SceneType SceneType { get; }

    public Scene Scene
    {
        get { return gameObject.scene; }
    }
    
    protected bool ShouldChangeScene
    {
        get { return !string.IsNullOrEmpty(nextSceneName); }
    }

    protected abstract void OnFinishedInitialize();
    protected abstract void OnStart();
    protected abstract void OnUpdate();
    protected abstract void OnLateUpdate();
    protected abstract void OnFixedUpdate();
    protected abstract void OnDestroyEvent();
    protected abstract void OnApplicationQuitEvent();

    protected void SetNextScene(string nextSceneName)
    {
        this.nextSceneName = nextSceneName;
    }

    private void Awake()
    {
        if (!GameInitializer.HasInstance)
        {
            StartCoroutine(AwakeCoroutine());
            return;
        }
        
        OnFinishedInitialize();
    }

    private IEnumerator AwakeCoroutine()
    {
        GameInitializer.CreateInstance();

        yield return StartCoroutine(GameInitializer.Instance.InitializeCoroutine());
        
        OnFinishedInitialize();    
    }

    private void Update()
    {
        if (!GameInitializer.Instance.IsInitialized)
        {
            return;
        }
        
        if (!hasStarted)
        {
            OnStart();
            hasStarted = true;
        }
        
        OnUpdate();
    }

    private void LateUpdate()
    {
        if (!GameInitializer.Instance.IsInitialized)
        {
            return;
        }
        
        OnLateUpdate();

        if (ShouldChangeScene)
        {
        }
    }

    private void FixedUpdate()
    {
        if (!GameInitializer.Instance.IsInitialized)
        {
            return;
        }
        
        OnFixedUpdate();
    }

    private void OnDestroy()
    {
        OnDestroyEvent();
    }

    private void OnApplicationQuit()
    {
        OnApplicationQuitEvent();
    }
}
