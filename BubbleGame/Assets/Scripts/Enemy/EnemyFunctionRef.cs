using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFunctionRef : MonoBehaviour
{
    [SerializeField]
    private EnemyMove enemyMove;

    [SerializeField]
    private EnemyCommonParameter enemyCommonParameter;

    [SerializeField]
    private EnemyController enemyController;

    [SerializeField]
    private EnemyFloatByContain enemyFloatByContain;

    [SerializeField]
    private EnemyFloatByDamage enemyFloatByDamage;

    public EnemyMove GetEnemyMove()
    {
        return enemyMove;
    }
    public EnemyCommonParameter GetEnemyStatus()
    {
        return enemyCommonParameter;
    }
    public EnemyController GetEnemyController()
    {
        return enemyController;
    }
    public EnemyFloatByContain GetEnemyFloatByContain()
    {
        return enemyFloatByContain;
    }
    public EnemyFloatByDamage GetEnemyFloatByDamage()
    {
        return enemyFloatByDamage;
    }
}
