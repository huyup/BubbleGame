using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
public enum ObjState
{
    OnGround,
    Floating,
    MovingByAirGun,
    MovingByTornado,
    Dizziness,
    Falling,
    Dead,
}
public enum MushroomType
{
    PoisonMushroom,
    DizzinessMushroom,
}
public class ObjController : MonoBehaviour
{
    public MushroomType MushroomType;

    public PlayerSelection PlayerSelectionWhoPushed { get; private set; }

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
    private BehaviorTree think;

    [SerializeField]
    private NavMeshAgent agent;

    private float initWanderSpeed;
    private float initAttackSpeed;

    [SerializeField]
    private GameObject collisionEff;

    [SerializeField]
    private BehaviorsCtr behaviorCtr;

    [SerializeField]
    private GameObject destroyEffWhenOut;

    [SerializeField]
    private GameObject dizzinessMushroomEff;

    [SerializeField]
    private GameObject poisonMushrommEff;

    [SerializeField]
    private UIBase uiCtr;

    private int bossNowHp;

    private Vector3 objBoxColliderDefaultSize;

    private Rigidbody rb;

    private Vector3 tornadoDestination;

    private float takeInSpeed;

    private float stopDistanceByTornado;

    private bool isRotateting;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        NowHp = status.MaxHp;
        bossNowHp = status.BossHp;
        if (status.Type == ObjType.Uribou || status.Type == ObjType.Harinezemi)
        {
            initWanderSpeed = (float)wander.GetVariable("WanderSpeed").GetValue();
            initAttackSpeed = (float)attack.GetVariable("RunSpeed").GetValue();
            agent.speed = initWanderSpeed;
        }
        else if (status.Type == ObjType.Obj)
        {
            if (GetComponent<BoxCollider>())
                objBoxColliderDefaultSize = GetComponent<BoxCollider>().size;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (ObjState == ObjState.MovingByTornado)
        {
            TakeInByTornado();
        }

        if (isRotateting)
        {
            int a = 240;
            transform.Rotate(new Vector3(Time.fixedDeltaTime * a, Time.fixedDeltaTime * a, Time.fixedDeltaTime * a));
        }


        if (status.Type == ObjType.Inoshishi)
        {
            if (bossNowHp <= 0)
            {
                behaviorCtr.DisableBehaviors();
                Destroy(this.gameObject);
                uiCtr.DrawClearText();
            }
        }
        if (status.Type != ObjType.Inoshishi)
        {
            if (ObjState == ObjState.OnGround)
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
                if (ObjState != ObjState.Dead)
                {
                    if (GetComponent<BoxCollider>())
                        GetComponent<BoxCollider>().isTrigger = true;
                }
            }
        }

        if (status.Type == ObjType.Inoshishi)
        {
            think.SetVariableValue("Hp", bossNowHp);
            summon.SetVariableValue("Hp", bossNowHp);
        }
        if (status.Type == ObjType.Uribou)
            SetSpeedByDamage(NowHp, status.MaxHp);
    }

    #region 泡関連機能
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
    #endregion

    #region ダメージ
    public void DeadWhenOut()
    {
        if (status.Type == ObjType.Inoshishi)
        {
            uiCtr.DrawClearText();
        }
        else if (status.Type != ObjType.Obj)
        {
            GameObject destroyEffInstance = Instantiate(destroyEffWhenOut) as GameObject;

            destroyEffInstance.transform.position = transform.position + new Vector3(0, 8, 0);

            destroyEffInstance.GetComponent<ParticleSystem>().Play();

            StageManager.Instance.RemoveEnemyCount(status.Type);
            Destroy(this.gameObject);
        }
    }
    public void DamageByBubble(int _power)
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
    public int GetBossNowHp()
    {
        return bossNowHp;
    }
    public void DamageByCollision(int _power)
    {
        if (status.Type == ObjType.Inoshishi)
        {
            if (bossNowHp > 0)
                bossNowHp -= _power;
        }
    }
    public void DamageByPoison(int _power)
    {
        if (status.Type == ObjType.Inoshishi)
        {
            if (bossNowHp <= 10)
                return;

            if (bossNowHp > 10)
                bossNowHp -= _power;
            else
            {
                bossNowHp = 10;
            }
        }
    }

    #endregion

    #region 衝突関連機能
    public void HitBossEff(Vector3 _initPos)
    {
        GameObject collisionInstance = Instantiate(collisionEff);
        collisionInstance.transform.position = _initPos;
        ParticleSystem[] particleSystems = collisionEff.GetComponentsInChildren<ParticleSystem>();

        foreach (var particle in particleSystems)
        {
            if (!particle.isPlaying)
                particle.Play();
        }
    }

    public void MushroomCrashWithHead(Vector3 _crashPointPos)
    {
        Destroy(this.gameObject);

        if (MushroomType == MushroomType.PoisonMushroom)
        {
            GameObject crashEff = Instantiate(poisonMushrommEff) as GameObject;

            crashEff.transform.position = _crashPointPos;

            var crashEffList = crashEff.GetComponentsInChildren<ParticleSystem>();

            foreach (var eff in crashEffList)
            {
                eff.Play();
            }
        }
        else if (MushroomType == MushroomType.DizzinessMushroom)
        {
            GameObject crashEff = Instantiate(dizzinessMushroomEff) as GameObject;

            crashEff.transform.position = _crashPointPos;

            var crashEffList = crashEff.GetComponentsInChildren<ParticleSystem>();

            foreach (var eff in crashEffList)
            {
                eff.Play();
            }
        }
    }

    public void ObjCrash()
    {
        rb.isKinematic = false;
        rb.AddForce(Vector3.forward * 40, ForceMode.VelocityChange);
    }

    public void StoneCrash()
    {
        Debug.Log("StoneCrash");
        GetComponent<BoxCollider>().isTrigger = false;
        GetComponent<BoxCollider>().size = objBoxColliderDefaultSize;
        rb.AddForce(Vector3.forward * 17, ForceMode.VelocityChange);
        rb.useGravity = true;
        SetObjState(ObjState.Dead);
    }
    public void EnemyCrash(Vector3 _hitPos)
    {
        if (GetComponent<BoxCollider>())
        {
            GetComponent<BoxCollider>().isTrigger = false;
        }

        Vector3 direction = (_hitPos - transform.position).normalized * -1;

        rb.AddForce(direction * 50, ForceMode.VelocityChange);
        rb.AddForce(Vector3.up * 50, ForceMode.VelocityChange);
        rb.freezeRotation = false;

        isRotateting = true;

        transform.DOScale(Vector3.zero, 2);
        SetObjState(ObjState.Dead);

        transform.gameObject.layer = 21;/*GroundOnly*/

        StartCoroutine(DelayDestroyByCrash());
    }
    IEnumerator DelayDestroyByCrash()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
        StageManager.Instance.RemoveEnemyCount(status.Type);
    }
    #endregion

    #region 他
    public void OnReset()
    {

        ObjState = ObjState.OnGround;

        if (GetComponent<BoxCollider>())
        {
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<BoxCollider>().size = objBoxColliderDefaultSize;
        }

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        NowHp = status.MaxHp;
        floatByContain.ResetFloatFlag();
        floatByDamage.ResetFloatFlag();
        bubbleDamageEff.ResetEmitter();

        if (status.Type != ObjType.Obj)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            agent.enabled = true;
            behaviorCtr.RestartBehaviors();
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().useGravity = true;
        }

        tornadoDestination = Vector3.zero;
        takeInSpeed = 0;
        stopDistanceByTornado = 0;
    }
    #endregion

    #region 吸い込み
    public void SetTakeInParameter(Vector3 _destination, float _takeInSpeedByTornado, float _stopDistanceByTornado)
    {
        tornadoDestination = _destination;
        takeInSpeed = _takeInSpeedByTornado;
        stopDistanceByTornado = _stopDistanceByTornado;
    }

    public void TakeInByTornado()
    {
        var destinationPositionInBubbleHeight = new Vector3(tornadoDestination.x, transform.position.y, tornadoDestination.z);

        if (Vector3.Distance(destinationPositionInBubbleHeight, transform.position) < stopDistanceByTornado)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            var direction = (destinationPositionInBubbleHeight - transform.position).normalized;
            rb.velocity = direction * Time.fixedDeltaTime * 60 * takeInSpeed;
        }
    }


    #endregion

    #region 空気砲
    public void AddForceByPush(Vector3 _direction, PlayerSelection _player)
    {
        PlayerSelectionWhoPushed = _player;
        GetComponent<BoxCollider>().isTrigger = false;
        rb.velocity = Vector3.zero;
        ObjState = ObjState.MovingByAirGun;
        rb.velocity = _direction;
    }
    #endregion

}
