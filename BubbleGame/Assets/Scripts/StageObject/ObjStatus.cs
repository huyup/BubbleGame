using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjStatus : MonoBehaviour
{

    /// <summary>
    /// 最大Hp
    /// </summary>
    [SerializeField]
    private int maxHp = 100;
    public int MaxHp
    {
        get { return maxHp; }
        private set
        {
            if (maxHp > 0 && maxHp != 0)
                maxHp = value;
        }
    }
}
