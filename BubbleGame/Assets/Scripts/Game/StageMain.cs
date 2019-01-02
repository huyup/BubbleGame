using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using NaughtyAttributes;
public class StageMain : MainBase
{
    [SerializeField]
    private GameObject itemPrehab;

    [SerializeField]
    private List<GameObject> itemBirthPoints = new List<GameObject>();

    [SerializeField]
    private List<GameObject> players = new List<GameObject>();


    private GameObject itemInstance;
    
    
    public void CreateItemInRandomPoint()
    {
        if (itemInstance)
            return;

        int randomPointNum =  Random.Range(0, itemBirthPoints.Count);
        
        itemInstance = Instantiate(itemPrehab) as GameObject;
        itemInstance.transform.position = itemBirthPoints[randomPointNum].transform.position;
    }

    public override SceneType SceneType
    {
        get { return SceneType.Stage; }
    }

    protected override void OnFinishedInitialize()
    {

    }

    [RuntimeInitializeOnLoadMethod]
    protected override void OnStart()
    {
        StageManager.CreateInstance();
        CreateItemInRandomPoint();
        GlobalVariables.Instance.SetVariableValue("Players", players);
    }

    protected override void OnUpdate()
    {
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
