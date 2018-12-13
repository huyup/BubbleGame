using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

public enum ObjState
{
    Idle,
    Floating,
    MovingByAirGun,
    Falling,
}
public class ObjController : MonoBehaviour
{
    public ObjState ObjState { get; private set; }

    [SerializeField]
    private ObjStatus status;

    [SerializeField]
    private ObjFloatByContain floatByContain;

    [SerializeField]
    private ObjFloatByDamage floatByDamage;

    [SerializeField]
    private BubbleDamageEff bubbleDamageEff;

    [SerializeField]
    private int nowHp;

    [SerializeField]
    private BehaviorTree wander;

    [SerializeField]
    private GUIStyle fontStyle;
    // Use this for initialization
    void Start()
    {
        nowHp = status.MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjState == ObjState.Idle)
        {
            if (nowHp < status.HpToFloat)
            {
                bubbleDamageEff.StopEmitter();
                floatByDamage.CreateBubbleByDamage();
            }
            else
            {
                bubbleDamageEff.ChangeEmitterOnUpdate(status.MaxHp, nowHp);
            }
        }
        else
            GetComponent<BoxCollider>().isTrigger = true;

        if (status.Type == ObjType.Enemy)
            SetSpeedByDamage();
    }

    private void SetSpeedByDamage()
    {
        wander.SetVariableValue("WalkSpeed", 1);
    }
    public void SetObjState(ObjState _state)
    {
        ObjState = _state;
    }
    public void Damage(int _power)
    {
        if (nowHp > 0)
            nowHp -= _power;
    }
    public void OnReset()
    {
        ObjState = ObjState.Idle;
        nowHp = status.MaxHp;
        GetComponent<BoxCollider>().isTrigger = false;
        floatByContain.ResetFloatFlag();
        floatByDamage.ResetFloatFlag();
        bubbleDamageEff.ResetEmitter();
    }
    public void AddForceByPush(Vector3 _direction)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        ObjState = ObjState.MovingByAirGun;
        GetComponent<Rigidbody>().velocity = _direction;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 200), "ObjState:" + ObjState, fontStyle);
    }
}
