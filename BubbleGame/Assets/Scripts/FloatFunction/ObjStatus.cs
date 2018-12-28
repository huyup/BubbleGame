using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjType
{
    Obj,
    Uribou,
    Harinezemi,
    Inoshishi
}
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
    }

    /// <summary>
    /// 浮上するときのHp
    /// </summary>
    [SerializeField]
    private int hpToFloat = 100;
    public int HpToFloat
    {
        get { return hpToFloat; }
    }

    /// <summary>
    /// タイプ
    /// </summary>
    [SerializeField] public ObjType Type;

    /// <summary>
    /// ボスの最大Hp
    /// </summary>
    [SerializeField]
    private int bossHp = 100;
    public int BossHp
    {
        get { return bossHp; }
    }
}
