using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloatByDamage : MonoBehaviour
{
    [SerializeField]
    private EnemyFunctionRef enemyFunctionRef;

    [SerializeField]
    private Transform bubbleSetInstanceRef;

    [SerializeField]
    private Transform bubbleInstanceStartRef;

    [SerializeField]
    private float bubbleMaxSize;

    private Transform bubbleInstance;

    [SerializeField]
    private float increaseScaleVelocity;

    [SerializeField]
    private float factorToFloat;

    private Rigidbody rb;

    private bool canSetInitPosToBubble = true;

    private bool canFloat = false;

    [SerializeField]
    private ParticleSystem bubbleDamageParticleSystem;

    [SerializeField]
    private float factorToCalEmission = 1;

    /// <summary>
    ///　中心点に移動できるかどうか
    /// </summary>
    [SerializeField]
    private bool canChangeVelocityToCenter = false;

    [SerializeField]
    private bool isPushing = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (enemyFunctionRef.GetEnemyController().IsDied)
            return;

        if (canFloat)
        {
            if (bubbleInstance == null)
            {
                AddFallForce();
            }
            else
            {
                if (!isPushing)
                    MoveToCenterPos();
            }
        }
    }
    public void CreateBubbleByDamage()
    {
        if (canSetInitPosToBubble)
        {
            CreateBubbleByDamageOnInit();
            canSetInitPosToBubble = false;
        }

        CreateBubbleByDamageOnUpdate();
    }
    public void AddForceByPush(Vector3 _direction)
    {
        isPushing = true;
        GetComponent<Rigidbody>().velocity = _direction;
    }
    private void CreateBubbleByDamageOnInit()
    {
        Transform bubbleSetInstance = Instantiate(bubbleSetInstanceRef) as Transform;

        bubbleInstance = bubbleSetInstance.transform.Find("Bubble");

        bubbleInstance.GetComponent<BubbleProperty>().IsCreatedByDamage = true;
        bubbleInstance.position = bubbleInstanceStartRef.position;
    }

    private void CreateBubbleByDamageOnUpdate()
    {
        if (!bubbleInstance)
            return;
        if (!canFloat)
        {
            Vector3 scaleVelocity = new Vector3(increaseScaleVelocity, increaseScaleVelocity, increaseScaleVelocity) *
                                    Time.fixedDeltaTime * 60;

            Vector3 upVelocity = Vector3.up * Time.fixedDeltaTime * 60 * factorToFloat;

            if (bubbleInstance.localScale.x < bubbleMaxSize)
            {
                bubbleInstance.localScale += scaleVelocity;
                bubbleInstance.GetComponent<Rigidbody>().velocity = upVelocity;
            }
            else
            {
                bubbleInstance.GetComponent<BubbleController>().SetFloatVelocityToBubble();
                FloatByContainOnInit();
                canFloat = true;
            }
        }
    }

    private void MoveToCenterPos()
    {
        if (Vector3.Distance(transform.position, bubbleInstance.position) > 0.1f)
        {
            if (canChangeVelocityToCenter)
            {
                Vector3 direction = (bubbleInstance.position - transform.position).normalized;
                rb.velocity = (direction * Time.fixedDeltaTime * 200);
            }
        }
        else if (Vector3.Distance(transform.position, bubbleInstance.position) < 0.1f)
        {
            canChangeVelocityToCenter = false;
            rb.velocity = new Vector3(0, bubbleInstance.GetComponent<Rigidbody>().velocity.y, 0);
        }
    }
    private void FloatByContainOnInit()
    {

        canFloat = true;
        enemyFunctionRef.GetEnemyController().SetIsFloating(true);
        canChangeVelocityToCenter = true;
        rb.velocity = Vector3.zero;
        GetComponent<CharacterController>().enabled = false;
    }
    private void AddFallForce()
    {
        rb.velocity = Physics.gravity;
    }

    public void Reset()
    {
        //TODO:ここリセットする
        isPushing = false;
        canFloat = false;
        canSetInitPosToBubble = true;
        rb.velocity = Vector3.zero;

        enemyFunctionRef.GetEnemyController().SetIsFloating(false);

        if (!GetComponent<CharacterController>().enabled)
            GetComponent<CharacterController>().enabled = true;

        ResetEmitter();
    }

    public void ChangeEmitterOnUpdate(int _maxHp, int _nowHp)
    {
        ParticleSystem.EmissionModule emissionModule = bubbleDamageParticleSystem.emission;

        emissionModule.rateOverTime = (((_maxHp / 10) - (_nowHp / 10))) * factorToCalEmission;
    }

    public void StopEmitter()
    {
        if (bubbleDamageParticleSystem == null)
            return;
        bubbleDamageParticleSystem.Clear();
        bubbleDamageParticleSystem.Stop();
    }

    public void ResetEmitter()
    {

        if (!bubbleDamageParticleSystem.isPlaying)
        {
            ParticleSystem.EmissionModule emissionModule = bubbleDamageParticleSystem.emission;

            emissionModule.rateOverTime = 0;
            bubbleDamageParticleSystem.Clear();
            bubbleDamageParticleSystem.Play();
        }
    }
}
