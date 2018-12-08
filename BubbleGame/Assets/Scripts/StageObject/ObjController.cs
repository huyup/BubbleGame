using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjController : MonoBehaviour
{
    private ObjStatus status;

    [SerializeField]
    private ObjFloatByContain floatByContain;

    [SerializeField]
    private ObjFloatByDamage floatByDamage;

    [SerializeField]
    private BubbleDamageEff bubbleDamageEff;
    
    /// <summary>
    /// 落下中かどうか
    /// </summary>
    [SerializeField]
    public bool IsFalling = false;

    /// <summary>
    /// 落下中かどうか
    /// </summary>
    [SerializeField]
    public bool IsPushingByAirGun { get; private set; }

    [SerializeField]
    private int nowHp;

	// Use this for initialization
	void Start ()
	{
	    status = GetComponent<ObjStatus>();
	    nowHp = status.MaxHp;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (nowHp < 20)
        {
            bubbleDamageEff.StopEmitter();
            floatByDamage.CreateBubbleByDamage();
        }
        else
        {
            bubbleDamageEff.ChangeEmitterOnUpdate(status.MaxHp, nowHp);
        }

        if (IsFalling)
            GetComponent<BoxCollider>().isTrigger = true;
    }
    public void Damage(int _power)
    {
        if (nowHp > 0)
            nowHp -= _power;
    }

    public void OnReset()
    {
        IsPushingByAirGun = false;
        nowHp = status.MaxHp;
        floatByContain.ResetFloatFlag();
        floatByDamage.ResetFloatFlag();
        bubbleDamageEff.ResetEmitter();
    }
    public void AddForceByPush(Vector3 _direction)
    {
        IsPushingByAirGun = true;
        GetComponent<Rigidbody>().velocity = _direction;
    }
}
