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

    private PlayerController playerController;
    private PlayerAnimator playerAnimator;
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
        playerAnimator = GetComponent<PlayerAnimator>();

        property = GetComponent<PlayerStatus>();
        playerNum = property.Num;
        prevPlayerPos = transform.position;
    }

    private void Update()
    {
        //左スティック
        leftXAxisInput = GamePad.GetAxis(GamePad.Axis.LeftStick, (GamePad.Index)playerNum).x;
        leftYAxisInput = GamePad.GetAxis(GamePad.Axis.LeftStick, (GamePad.Index)playerNum).y;

        //Debug.Log("leftXAxisInput" + leftXAxisInput);
        //Debug.Log("leftYAxisInput" + leftYAxisInput);

        //右スティック
        rightXAxisInput = GamePad.GetAxis(GamePad.Axis.RightStick, (GamePad.Index)playerNum).x;
        //y軸を反転させる
        rightYAxisInput = GamePad.GetAxis(GamePad.Axis.RightStick, (GamePad.Index)playerNum).y;

        if (rightXAxisInput * leftXAxisInput < 0 || rightYAxisInput * leftYAxisInput < 0
            || rightXAxisInput * leftYAxisInput < 0 || rightYAxisInput * leftXAxisInput < 0 )
        {
            playerController.Slow();
        }
        else
        {
            playerController.ResetSlow();
        }

        playerController.MoveByRigidBody(leftXAxisInput, leftYAxisInput, maxControllerTolerance, prevPlayerPos);

        playerController.Rotate(rightXAxisInput, rightYAxisInput, maxControllerTolerance, prevPlayerPos);

        //攻撃ボタン
        if (GamePad.GetButtonDown(GamePad.Button.RightShoulder, (GamePad.Index)playerNum))
        {
            playerController.GetWeapon().OnAttackButtonDown();
        }
        if (GamePad.GetButton(GamePad.Button.RightShoulder, (GamePad.Index)playerNum))
        {
            playerController.GetWeapon().OnAttackButtonStay(); ;
        }
        if (GamePad.GetButtonUp(GamePad.Button.RightShoulder, (GamePad.Index)playerNum))
        {
            playerController.GetWeapon().OnAttackButtonUp();
        }

        //武器の切り替えボタン
        if (GamePad.GetButtonDown(GamePad.Button.LeftShoulder, (GamePad.Index)playerNum))
        {
            playerController.ChangeWeapon();
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
