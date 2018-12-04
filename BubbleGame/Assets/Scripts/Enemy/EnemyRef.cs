using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// TODO:ここのrefを使いたい
/// </summary>
public class EnemyRef : MonoBehaviour
{
    [SerializeField] private EnemyMove enemyMove;
    [SerializeField] private EnemyController controller;
    [SerializeField] private Rigidbody rb;
    public EnemyMove GetMove()
    {
        return enemyMove;
    }

    public EnemyController GetController()
    {
        return controller;
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }
}
