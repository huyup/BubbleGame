using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;
public enum ObjState
{
    OnGround,
    Floating,
    MovingByAirGun,
    Dizziness,
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
    public int NowHp { get; private set; }

    [SerializeField]
    private BehaviorTree wander;

    [SerializeField]
    private BehaviorTree attack;

    [SerializeField]
    private BehaviorTree summon;

    [SerializeField]
    private NavMeshAgent agent;

    private float initWanderSpeed;
    private float initAttackSpeed;

    [SerializeField]
    private GameObject collisionEff;

    [SerializeField]
    private BehaviorsCtr behaviorCtr;

    [SerializeField]
    private GameObject destroyEff;

    [SerializeField]
    private UIBase uiCtr;
    
    // Use this for initialization
    void Start()
    {
        NowHp = status.MaxHp;
        if (status.Type == ObjType.Uribou || status.Type == ObjType.Harinezemi)
        {
            initWanderSpeed = (float)wander.GetVariable("WanderSpeed").GetValue();
            initAttackSpeed = (float)attack.GetVariable("RunSpeed").GetValue();
            agent.speed = initWanderSpeed;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (ObjState == ObjState.Floating && status.Type != ObjType.Obj)
        {
            if (status.Type == ObjType.Inoshishi)
            {
                StartCoroutine(DebugSetClear());
            }
            behaviorCtr.DisableBehaviors();
        }

        if (status.Type == ObjType.Inoshishi)
            summon.SetVariableValue("Hp", NowHp);

        if (ObjState == ObjState.OnGround || ObjState == ObjState.Dizziness)
        {
            if (NowHp < status.HpToFloat)
            {
                bubbleDamageEff.StopEmitter();
                floatByDamage.CreateBubbleByDamage();
            }
            else
            {
                bubbleDamageEff.ChangeEmitterOnUpdate(status.MaxHp, NowHp);
            }
        }
        else
        {
            if (GetComponent<BoxCollider>())
                GetComponent<BoxCollider>().isTrigger = true;
        }

        if (status.Type == ObjType.Uribou)
            SetSpeedByDamage(NowHp, status.MaxHp);

    }

    IEnumerator DebugSetClear()
    {
        yield return new WaitForSeconds(10);
        uiCtr.DrawClearText();
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
        if (status.Type == ObjType.Inoshishi)
        {
            if (NowHp > 0 && ObjState == ObjState.Dizziness)
                NowHp -= _power / 5;
        }
        else
        {
            if (NowHp > 0)
                NowHp -= _power;
        }

    }

    public void Collision(Vector3 _initPos)
    {
        GameObject collisionInstance = Instantiate(collisionEff);
        collisionInstance.transform.position = _initPos;
        collisionInstance.GetComponent<ParticleSystem>().Play();
    }

    public void Dead()
    {
        if (status.Type == ObjType.Inoshishi)
        {
            uiCtr.DrawClearText();
        }
        else if (status.Type != ObjType.Obj)
        {
            GameObject destroyEffInstance = Instantiate(destroyEff) as GameObject;

            destroyEffInstance.transform.position = transform.position + new Vector3(0, 8, 0);

            destroyEffInstance.GetComponent<ParticleSystem>().Play();

            StageManager.Instance.RemoveEnemyCount(status.Type);
            Destroy(this.gameObject);
        }
    }

    public void OnReset()
    {
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().velocity = Vector3.zero;

        ObjState = ObjState.OnGround;
        NowHp = status.MaxHp;
        GetComponent<BoxCollider>().isTrigger = false;
        floatByContain.ResetFloatFlag();
        floatByDamage.ResetFloatFlag();
        bubbleDamageEff.ResetEmitter();

        if (status.Type != ObjType.Obj)
        {
            agent.enabled = true;
            behaviorCtr.RestartBehaviors();
        }
    }
    public void AddForceByPush(Vector3 _direction)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        ObjState = ObjState.MovingByAirGun;
        GetComponent<Rigidbody>().velocity = _direction;
    }
}
