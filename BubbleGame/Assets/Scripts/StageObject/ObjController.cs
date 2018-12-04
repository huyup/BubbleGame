using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjController : MonoBehaviour
{
    private ObjStatus status;

    [SerializeField]
    private ObjFloatByDamage floatByDamage;

    [SerializeField]
    private BubbleDamageEff bubbleDamageEff;
    
    /// <summary>
    /// 落下中かどうか
    /// </summary>
    [SerializeField]
    public bool IsFalling = false;
    
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

    }
    public void Damage(int _power)
    {
        if (nowHp > 0)
            nowHp -= _power;
    }
    
}
