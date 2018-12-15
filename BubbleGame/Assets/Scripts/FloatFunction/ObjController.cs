using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public enum ObjState
{
    OnGround,
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
    private BehaviorTree attack;

    [SerializeField]
    private GUIStyle fontStyle;

    [SerializeField]
    private NavMeshAgent agent;

    private float initWanderSpeed;
    private float initAttackSpeed;

    [SerializeField]
    private UribouAnimatorCtr uribouAnimator;
    // Use this for initialization
    void Start()
    {
        nowHp = status.MaxHp;

        if (status.Type == ObjType.Uribou)
        {
            nowHp = status.MaxHp;
            initWanderSpeed = (float)wander.GetVariable("WanderSpeed").GetValue();
            initAttackSpeed = (float)attack.GetVariable("RunSpeed").GetValue();
            agent.speed = initWanderSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjState == ObjState.OnGround)
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

        if (status.Type == ObjType.Uribou)
            SetSpeedByDamage(nowHp, status.MaxHp);
    }
    private void SetSpeedByDamage(int _nowHp, int _maxHp)
    {
        var rate = (_nowHp * 100 / _maxHp);

        if ((bool)wander.GetVariable("IsWandering").GetValue())
            agent.speed = initWanderSpeed * (rate * 0.01f);

        if ((bool)attack.GetVariable("IsAttacking").GetValue())
            agent.speed = initAttackSpeed * (rate * 0.01f);


    }
    public void SetObjState(ObjState _state)
    {
        ObjState = _state;
    }
    public void Damage(int _power)
    {
        //if (nowHp > 0)
        //    nowHp -= _power;
    }
    public void OnReset()
    {
        ObjState = ObjState.OnGround;
        nowHp = status.MaxHp;
        GetComponent<BoxCollider>().isTrigger = false;
        floatByContain.ResetFloatFlag();
        floatByDamage.ResetFloatFlag();
        bubbleDamageEff.ResetEmitter();

        if (status.Type == ObjType.Uribou)
        {
            uribouAnimator.SetDownAnimation();
        }
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
