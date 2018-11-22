using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput; //マルチコントローラーアセット

public enum WeaponType
{
    WeaponA = 1,
    WeaponB,
    WeaponC,
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
    #region フィールド
    /// <summary>
    /// アタッチするコンポーネント
    /// </summary>
    private Rigidbody rb;
    private PlayerStatus status;
    private PlayerAnimator animatorCtr;
    private GroundDetector groundDetector;

    //無敵かどうか
    private bool isVincible = false;

    #endregion

    #region 初期化
    void Start()
    {
        nowWeapon = null;
        animatorCtr = GetComponent<PlayerAnimator>();
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();

        int groundLayer = (1 << 9) | (1 << 10);

        groundDetector = GetComponent<GroundDetector>();
        groundDetector.Initialize(0.25f, 2.0f, 0.01f, 0.05f, groundLayer);

        nowWeaponType = WeaponType.WeaponA;
    }
    #endregion

    #region Update
    private void Update()
    {
        groundDetector.UpdateDetection();
        CheckDied();
        ChangeGravity();
    }
    #endregion

    #region 回転用メソッド
    public void Rotate(float _inputH2, float _inputV2, float _maxControllerLerance, Vector3 _prevInputPlayerPos)
    {
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
        Vector3 velocity = new Vector3(_h, 0, -_v) * Time.fixedDeltaTime;

        if (velocity.magnitude > 0)
            transform.rotation = Quaternion.LookRotation(new Vector3
    (velocity.x, 0, velocity.z));
    }
    #endregion

    #region 移動用メソッド
    public void MoveByRigidBody(float _h, float _v, float _maxControllerTolerance, Vector3 _prevPosition)
    {
        Vector3 velocity = new Vector3(_h * status.RunSpeed, 0, _v * status.RunSpeed) * Time.fixedDeltaTime;

        if (velocity.magnitude > _maxControllerTolerance)
            //現在の位置＋入力した数値の場所に移動する
            rb.MovePosition(transform.position + velocity);

        animatorCtr.SetMoveAnimation(_prevPosition);
    }
    #endregion

    #region 攻撃用メソッド
    public void ChangeWeapon()
    {
        int nextWeaponTypeNum = (int)nowWeaponType;

        nextWeaponTypeNum++;

        if (nextWeaponTypeNum == (int)WeaponType.Max)
            nextWeaponTypeNum = (int)WeaponType.WeaponA;

        WeaponType nextWeaponType = (WeaponType)Enum.ToObject(typeof(WeaponType), nextWeaponTypeNum);

        nowWeaponType = nextWeaponType;
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
                nowWeapon = null;
                break;
            default:
                nowWeapon = null;
                break;
        }

        if (nowWeapon == null)
        {
            Debug.Log("ErrorToSetWeapon");
        }

        return nowWeapon;
    }
    #endregion

    #region ジャンプ用メソッド
    public void Jump()
    {
        if (!groundDetector.IsHit)
            return;
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
        renderer = transform.Find("PlayerMesh").GetComponent<Renderer>();

        for (int i = 0; i < status.InvincibleTotalTime; i++)
        {
            renderer.enabled = !renderer.enabled;
            isVincible = true;
            yield return new WaitForSeconds(status.InvincibleInterval);
        }

        isVincible = false;
    }

    private void CheckDied()
    {
        if (status.nowHp <= 0)
        {
            //TODO:ここにプレイヤーの死亡した後の操作を入れる
            this.gameObject.SetActive(false);
        }
    }
    #endregion
}
