using System.Collections.Generic;
using UnityEngine;

public class StageMain : MainBase
{
    [SerializeField]
    private CameraCtr cameraCtr;

    [SerializeField]
    private List<Transform> players;
    public override SceneType SceneType
    {
        get { return SceneType.Stage; }
    }

    protected override void OnFinishedInitialize()
    {
        Debug.Log("OnFinishedInitialize");
        foreach (var player in players)
        {
            player.GetComponent<PlayerController>().OnInitialize();
            player.GetComponent<PlayerInputManager>().OnInitialize();
        }
    }
    protected override void OnStart()
    {
        Debug.Log("Start");
        foreach (var player in players)
        {
            player.GetComponent<PlayerController>().OnStart();
            player.GetComponent<PlayerInputManager>().OnStart();
        }
    }

    protected override void OnUpdate()
    {
        cameraCtr.OnUpdate();
        foreach (var player in players)
        {
            player.GetComponent<PlayerController>().OnUpdate();
            player.GetComponent<PlayerInputManager>().OnUpdate();
        }
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
