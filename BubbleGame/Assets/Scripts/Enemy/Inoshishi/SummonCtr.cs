using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Uribou,
    Harinezumi,
}
public class SummonCtr : MonoBehaviour
{
    [SerializeField]
    private GameObject uribou;

    [SerializeField]
    private GameObject harinezumi;

    [SerializeField]
    private List<GameObject> startList = new List<GameObject>();
    
    private List<GameObject> defaultStartPosList = new List<GameObject>();
    private void Start()
    {
        foreach (var startPos in startList)
        {
            GameObject defaultPos = startPos.transform.Find("InitPos").gameObject;
            defaultStartPosList.Add(defaultPos);
        }
    }

    public void Summon(EnemyType _enemyType, int _startNum)
    {
        if (_enemyType == EnemyType.Uribou)
        {
            StageManager.Instance.AddUribouCount();
            GameObject uribouInstance= Instantiate(uribou, startList[_startNum].transform.position,Quaternion.identity);
            uribouInstance.GetComponent<TargetCtr>().SetStartPos(defaultStartPosList[_startNum]);
        }
        else if (_enemyType == EnemyType.Harinezumi)
        {
            StageManager.Instance.AddHarinezumiCount();
            GameObject harinezumiInstance = Instantiate(harinezumi, startList[_startNum].transform.position, Quaternion.identity);
            harinezumiInstance.GetComponent<TargetCtr>().SetStartPos(defaultStartPosList[_startNum]);
        }
    }
}
