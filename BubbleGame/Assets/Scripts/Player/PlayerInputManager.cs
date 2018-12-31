using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput; //マルチコントローラーアセット

public class PlayerInputManager : MonoBehaviour
{
    /// <summary>
    /// FIXME:必要か？
    /// </summary>
    [SerializeField]
    private float maxControllerTolerance = 0.02f;

    [SerializeField]
    private PlayerWeaponC airWeapon;

    private PlayerController playerController;
    private PlayerStatus property;

    private int playerNum;

    /// <summary>
    /// 左スティックの入力量
    /// </summary>
    private float leftXAxisInput;
    private float leftYAxisInput;

    /// <summary>
    /// 右スティックの入力量
    /// </summary>
    private float rightXAxisInput;
    private float rightYAxisInput;

    //入力前の位置
    private Vector3 prevPlayerPos;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        property = GetComponent<PlayerStatus>();
        playerNum = property.Num;
        prevPlayerPos = transform.position;
    }

    private void Update()
    {
        if (playerController.IsDead)
            return;

        //左スティック
        leftXAxisInput = GamePad.GetAxis(GamePad.Axis.LeftStick, (GamePad.Index)playerNum).x;
        leftYAxisInput = GamePad.GetAxis(GamePad.Axis.LeftStick, (GamePad.Index)playerNum).y;

        //右スティック
        rightXAxisInput = GamePad.GetAxis(GamePad.Axis.RightStick, (GamePad.Index)playerNum).x;
        //y軸を反転させる
        rightYAxisInput = GamePad.GetAxis(GamePad.Axis.RightStick, (GamePad.Index)playerNum).y;

        //逆走するとき、スピードを低下させる
        if (rightXAxisInput * leftXAxisInput < 0 || rightYAxisInput * leftYAxisInput < 0
            || rightXAxisInput * leftYAxisInput < 0 || rightYAxisInput * leftXAxisInput < 0)
        {
            playerController.Slow();
        }
        else
        {
            playerController.ResetSlow();
        }

        playerController.MoveByRigidBody(leftXAxisInput, leftYAxisInput, maxControllerTolerance, prevPlayerPos);

        playerController.Rotate(rightXAxisInput, rightYAxisInput, maxControllerTolerance, prevPlayerPos);

        //武器の切り替えボタン
        if (GamePad.GetButtonDown(GamePad.Button.LeftShoulder, (GamePad.Index)playerNum))
        {
            if (!playerController.IsUsingAirGun)
                playerController.ChangeWeapon();
        }

        //攻撃ボタン
        if (GamePad.GetButtonDown(GamePad.Button.RightShoulder, (GamePad.Index)playerNum))
        {
            if (playerController.GetWeapon().CanAttack)
                playerController.GetWeapon().OnAttackButtonDown();
        }
        if (GamePad.GetButton(GamePad.Button.RightShoulder, (GamePad.Index)playerNum))
        {
            if (playerController.GetWeapon().CanAttack)
                playerController.GetWeapon().OnAttackButtonStay(); ;
        }
        if (GamePad.GetButtonUp(GamePad.Button.RightShoulder, (GamePad.Index)playerNum))
        {
            if (playerController.GetWeapon().CanAttack)
                playerController.GetWeapon().OnAttackButtonUp();
        }
        //攻撃ボタン2
        if (GamePad.GetButtonDown(GamePad.Button.X, (GamePad.Index)playerNum))
        {
            airWeapon.OnAttackButtonDown();
        }
        if (GamePad.GetButton(GamePad.Button.X, (GamePad.Index)playerNum))
        {
            airWeapon.OnAttackButtonStay();
        }
        if (GamePad.GetButtonUp(GamePad.Button.X, (GamePad.Index)playerNum))
        {
            airWeapon.OnAttackButtonUp();
        }
        //ジャンプボタン
        if (GamePad.GetButtonDown(GamePad.Button.A, (GamePad.Index)playerNum))
        {
            playerController.Jump();
        }

        //入力後の位置を更新させる
        prevPlayerPos = transform.position;
    }
}
