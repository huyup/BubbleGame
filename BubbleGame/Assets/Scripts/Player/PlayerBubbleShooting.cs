using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput; //マルチコントローラーアセット

public class PlayerBubbleShooting : MonoBehaviour
{
    #region フィールド
    /// <summary>
    /// アタッチするオブジェ
    /// </summary>
    private BubbleProperty bubbleProperty;
    private Rigidbody rb;
    private PlayerStatus status;
    private Animator animator;

    /// <summary>
    /// 泡のオブジェ
    /// </summary>
    [SerializeField]
    private GameObject bubbleSet;

    private List<GameObject> bubbles = new List<GameObject>();

    /// <summary>
    /// 泡の出現参照オブジェ
    /// </summary>
    private GameObject bubbleStartObj;
    private Vector3 bubbleStartPos;

    private float spaceKeyStorage = 0.0f;
    private bool canSetBubbleStartPos = false;
    private bool isPushed = false;
    #endregion

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        status = GetComponent<PlayerStatus>();
        bubbleStartObj = transform.Find("BubbleStartObj").gameObject;
        animator = GetComponent<Animator>();

        bubbleProperty = bubbleSet.transform.Find("Bubble").GetComponent<BubbleProperty>();
    }
    // Update is called once per frame
    void Update()
    {
        //泡の発射位置を更新させる
        bubbleStartPos = bubbleStartObj.transform.position;
    }

    public void SetAttackAnimation(AttackButtonState _attackButtonState)
    {
        if (_attackButtonState == AttackButtonState.ButtonDown)
        {
            animator.SetBool("Attacking", true);
        }
        if (_attackButtonState == AttackButtonState.ButtonKeep)
        {
            animator.speed = 0.5f;
        }
        if (_attackButtonState == AttackButtonState.ButtonUp)
        {
            animator.speed = 1;
            animator.SetBool("Attacking", false);
        }
    }
    
    public void CreateTheBubbleSet()
    {
        rb.velocity = Vector3.zero;

        canSetBubbleStartPos = true;
        isPushed = false;
        spaceKeyStorage = 0;

        GameObject bubbleSetInstance = Instantiate(bubbleSet) as GameObject;

        GameObject bubbleInstance = bubbleSetInstance.transform.Find("Bubble").gameObject;

        bubbles.Add(bubbleInstance);
    }

    public void ChangeTheBubbleScale()
    {
        if (bubbles.Count == 0)
            return;

        if (bubbles[bubbles.Count - 1])
        {
            rb.velocity = Vector3.zero;

            if (spaceKeyStorage < bubbleProperty.MaxSize)
            {
                spaceKeyStorage += status.SpaceKeySpeed * Time.deltaTime;
                bubbles[bubbles.Count - 1].transform.localScale += new Vector3(spaceKeyStorage, spaceKeyStorage, spaceKeyStorage);
                if (canSetBubbleStartPos)
                {
                    bubbles[bubbles.Count - 1].transform.position = bubbleStartPos;
                    canSetBubbleStartPos = false;
                }
                //少しずつ前に移動させる
                bubbles[bubbles.Count - 1].transform.position += transform.forward * Time.deltaTime * 1.2f;
                //少しずつ上に移動させる
                bubbles[bubbles.Count - 1].transform.position += transform.up * Time.deltaTime * 0.8f;
            }
            else
            {
                if (!isPushed)
                {
                    PushTheBubbleOnceTime();
                    isPushed = true;
                }
            }
        }
    }
    public bool CheckIsBubbleOverMoveableSize()
    {
        if (spaceKeyStorage > 0)
            return true;
        else
            return false;
    }
    public void PushTheBubbleOnceTime()
    {
        if (bubbles[bubbles.Count - 1] == null || isPushed)
            return;

        if (!bubbles[bubbles.Count - 1].GetComponent<BubbleProperty>().IsForceFloating)
        {
            bubbles[bubbles.Count - 1].GetComponent<Rigidbody>().AddForce(transform.forward * status.BubbleFowardPower,
                ForceMode.VelocityChange);
            bubbles[bubbles.Count - 1].GetComponent<Rigidbody>().AddForce(transform.up * status.BubbleUpPower,
                ForceMode.VelocityChange);
            bubbles[bubbles.Count - 1].GetComponent<BubbleCollision>().SetDestroyEnable();
        }
    }
}


