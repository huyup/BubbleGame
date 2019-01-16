using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType
{
    WeaponA = 1,
    WeaponB,
    WeaponC,
    WeaponD,
    Max,
}
public class PlayerController : MonoBehaviour
{
    private WeaponType nowWeaponType;
    private PlayerWeapon nowWeapon;

    [SerializeField]
    private PlayerWeaponA weaponA;

    [SerializeField]
    private PlayerWeaponB weaponB;

    [SerializeField]
    private PlayerWeaponC weaponC;

    [SerializeField]
    private PlayerWeaponD weaponD;
    /// <summary>
    /// アタッチするコンポーネント
    /// </summary>
    private Rigidbody rb;
    private PlayerStatus status;
    private PlayerAnimatorCtr animatorCtr;
    private GroundDetector groundDetector;
    private PlayerRescueTriggerCtr rescueTriggerCtr;
    private bool isVincible = false;
    private bool isSlow = false;

    private bool canJump = false;
    private bool canMove = false;
    private bool canRotate = false;
    private bool canJumpAttack = false;
    private bool canUseGravity = true;

    public bool IsDead { get; private set; }

    #region 初期化
    void Start()
    {
        nowWeapon = weaponA;
        animatorCtr = GetComponent<PlayerAnimatorCtr>();
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
        rescueTriggerCtr = transform.Find("RescueTrigger").GetComponent<PlayerRescueTriggerCtr>();

        int bubbleLayer = (1 << 10) | (1 << 0);
        
        int groundLayer = (1 << 9) | (1 << 12) | (1 << 16);
        groundDetector = GetComponent<GroundDetector>();
        groundDetector.Initialize(0.5f, 2.0f, 0.01f, 0.08f, groundLayer);

        nowWeaponType = WeaponType.WeaponA;
        isSlow = false;
        canJump = true;
        canRotate = true;
        canMove = true;
    }
    #endregion

    #region Update
    private void Update()
    {
        groundDetector.UpdateDetection();
        

        CheckDied();
        if (canUseGravity)
        {
            rb.useGravity = true;
            ChangeGravity();
        }
        else
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
        }
        CheckCanJumpAttack();

    }
    #endregion

    #region 回転用メソッド
    public void Rotate(float _inputH2, float _inputV2, float _maxControllerLerance, Vector3 _prevInputPlayerPos)
    {
        if (!canRotate)
            return;
        //最新の位置-入力前の位置=方向
        Vector3 direction = transform.position - _prevInputPlayerPos;
        //最新の位置-入力前の位置=方向
        Vector3 directionXZ = direction - new Vector3(0, direction.y, 0);

        if (directionXZ.magnitude > 0)
        {
            RotateCharacterByTwoAxis(directionXZ, _inputH2, _inputV2, _maxControllerLerance);
        }
        else
        {
            //動かないときでも方向転換できるように
            RotateCharacterByAxis2(_inputH2, _inputV2);
        }
    }

    private void RotateCharacterByTwoAxis(Vector3 _direction, float _inputH2, float _inputV2, float _maxControllerLerance)
    {
        if ((_direction.magnitude > _maxControllerLerance) &&
    (Mathf.Abs(_inputH2) <= 0.001f && Mathf.Abs(_inputV2) <= 0.001f))
            RotateCharacterByAxis1(_direction);
        else
            RotateCharacterByAxis2(_inputH2, _inputV2);
    }

    private void RotateCharacterByAxis1(Vector3 _direction)
    {
        transform.rotation = Quaternion.LookRotation(new Vector3
(_direction.x, 0, _direction.z));
    }

    private void RotateCharacterByAxis2(float _h, float _v)
    {

        //なぜか右のスティックが上下反転しているため、
        //vの値をマイナスにした
        Vector3 velocity = new Vector3(_h, 0, _v) * Time.fixedDeltaTime;

        if (velocity.magnitude > 0)
            transform.rotation = Quaternion.LookRotation(new Vector3
    (velocity.x, 0, velocity.z));
    }
    #endregion

    #region 移動用メソッド
    public void MoveByRigidBody(float _h, float _v, float _maxControllerTolerance, Vector3 _prevPosition)
    {
        if (!canMove)
            return;
        if (!isSlow)
        {
            Vector3 velocity = new Vector3(_h * status.RunSpeed, 0, _v * status.RunSpeed) * Time.fixedDeltaTime;

            if (velocity.magnitude > _maxControllerTolerance)
                //現在の位置＋入力した数値の場所に移動する
                rb.MovePosition(transform.position + velocity);
        }
        else
        {
            Vector3 velocity = new Vector3(_h * status.BackRunSpeed, 0, _v * status.BackRunSpeed) * Time.fixedDeltaTime;

            if (velocity.magnitude > _maxControllerTolerance)
                //現在の位置＋入力した数値の場所に移動する
                rb.MovePosition(transform.position + velocity);
        }

        animatorCtr.SetMoveAnimation(_prevPosition);
    }
    #endregion

    #region 攻撃用メソッド
    public void ChangeWeapon()
    {
        if (nowWeapon == null)
            return;

        if (GetWeapon())
        {
            GetWeapon().OnChange();
        }
        if (status.WeaponSelection == WeaponSelection.Bubble)
        {
            WeaponType tmpWeaponSelection = nowWeaponType;
            if (tmpWeaponSelection == WeaponType.WeaponA)
                nowWeaponType = WeaponType.WeaponB;
            if (tmpWeaponSelection == WeaponType.WeaponB)
                nowWeaponType = WeaponType.WeaponA;
        }
        if (status.WeaponSelection == WeaponSelection.AirGun)
        {
            WeaponType tmpWeaponSelection = nowWeaponType;
            if (tmpWeaponSelection == WeaponType.WeaponC)
                nowWeaponType = WeaponType.WeaponD;
            if (tmpWeaponSelection == WeaponType.WeaponD)
                nowWeaponType = WeaponType.WeaponC;
        }
    }

    private void CheckCanJumpAttack()
    {
        switch (nowWeaponType)
        {
            case WeaponType.WeaponA:
                canJumpAttack = false;
                break;
            case WeaponType.WeaponB:
                canJumpAttack = false;
                break;
            case WeaponType.WeaponC:
                canJumpAttack = true;
                break;
            case WeaponType.WeaponD:
                canJumpAttack = true;
                break;
            default:
                canJumpAttack = false;
                break;
        }

        if (!canJumpAttack)
        {
            if (groundDetector.IsHit)
                ResetAttack();
            else
            {
                BanAttack();
            }
        }
        else
        {
            ResetAttack();
        }
    }

    public WeaponType GetNowWeaponType()
    {
        return nowWeaponType;
    }
    public void UseAirGun()
    {
        status.SetWeaponSelection(WeaponSelection.AirGun);
        GetWeapon().OnChange();
        nowWeaponType = WeaponType.WeaponC;

        Invoke("DelayDisableAirGun", status.AirGunLastTime);
    }
    public void DisableAirGun()
    {
        status.SetWeaponSelection(WeaponSelection.Bubble);
        GetWeapon().OnChange();
        nowWeaponType = WeaponType.WeaponA;
        //if (stageMain)
        //    stageMain.CreateItemInRandomPoint();
    }
    private void DelayDisableAirGun()
    {
        weaponC.OnAttackButtonUp();
    }
    public PlayerWeapon GetWeapon()
    {
        switch (nowWeaponType)
        {
            case WeaponType.WeaponA:
                nowWeapon = weaponA;
                break;
            case WeaponType.WeaponB:
                nowWeapon = weaponB;
                break;
            case WeaponType.WeaponC:
                {
                    nowWeapon = weaponC;
                    nowWeapon.GetComponent<PlayerWeaponC>().UseShootSupportLine();
                    break;
                }
            case WeaponType.WeaponD:
                nowWeapon = weaponD;
                break;
            default:
                nowWeapon = null;
                break;
        }
        return nowWeapon;
    }

    public void PullBack()
    {
        Vector3 backVelocity = transform.forward * -1;
        rb.velocity = backVelocity * Time.fixedDeltaTime * 60 * status.PullBackSpeed;
    }
    #endregion

    #region ジャンプ用メソッド
    public void Jump()
    {
        if (!groundDetector.IsHit || !canJump)
        {
            return;
        }

        rb.velocity = Vector3.zero;
        Vector3 velocity = transform.up * status.JumpPower * Time.fixedDeltaTime * 60;
        rb.velocity = velocity;
    }
    private void ChangeGravity()
    {
        //重力を増やせる
        rb.velocity += Physics.gravity * status.FactorToCalGravity * Time.fixedDeltaTime * 60;
    }

    #endregion

    #region ダメージ用
    public void Damage()
    {
        if (isVincible || status.nowHp <= 0)
            return;
        if (status.nowHp > 0)
            status.nowHp--;

        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        Renderer renderer;
        renderer = transform.Find("MeshList").Find("PlayerMesh").GetComponent<Renderer>();

        for (int i = 0; i < status.InvincibleTotalTime; i++)
        {
            renderer.enabled = !renderer.enabled;
            isVincible = true;
            yield return new WaitForSeconds(status.InvincibleLastTime);
        }

        isVincible = false;
    }

    public void Revival()
    {
        Debug.Log("Revival" + transform.name);
        animatorCtr.SetRevivalAnimation();
        StartCoroutine(DelayResetPlayer());
    }
    IEnumerator DelayResetPlayer()
    {
        yield return new WaitForSeconds(status.RevivalAnimationTime);
        animatorCtr.SetOffFlagWhenRevival();
        status.nowHp = status.MaxHp;
        IsDead = false;
        rb.isKinematic = false;
    }


    public void Rescue()
    {
        Debug.Log("Rescue");
        if (rescueTriggerCtr.IsPlayerInResucePoint)
        {
            Debug.Log("rescueTriggerCtr.DeadPlayer" + rescueTriggerCtr.DeadPlayer.name);
            rescueTriggerCtr.DeadPlayer.GetComponent<PlayerController>().Revival();
        }
    }
    private void CheckDied()
    {
        if (status.nowHp <= 0)
        {
            Debug.Log("Dead");
            //TODO:ここにプレイヤーの死亡した後の操作を入れる
            IsDead = true;
            rb.isKinematic = true;
            animatorCtr.SetDeadAnimation();
        }
    }
    #endregion

    #region 禁止機能・スピード低下機能

    public void BanGravity()
    {
        canUseGravity = false;
    }

    public void BanRotate()
    {
        canRotate = false;
    }

    public void ResetRotate()
    {
        canRotate = true;
    }

    public void Slow()
    {
        isSlow = true;
    }

    public void ResetSlow()
    {
        isSlow = false;
    }

    public void BanMove()
    {
        canMove = false;
    }

    public void BanJump()
    {
        canJump = false;
    }

    public void BanAttack()
    {
        GetWeapon().BanAttack();
    }

    public void ResetMove()
    {
        canMove = true;
    }

    public void ResetJump()
    {
        canJump = true;
    }

    public void ResetAttack()
    {
        GetWeapon().ResetAttack();
    }

    public void ResetGravity()
    {
        canUseGravity = true;
    }
    #endregion

}
