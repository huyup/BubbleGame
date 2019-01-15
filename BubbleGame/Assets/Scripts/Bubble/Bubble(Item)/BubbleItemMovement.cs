using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BubbleItemMovement : MonoBehaviour
{
    [SerializeField]
    private float pingPongSpeed = 0.5f;

    [SerializeField]
    private float pingPongLength = 2;

    [SerializeField]
    private float moveSpeed = 0.05f;

    [SerializeField]
    private float randomRange = 5;

    [SerializeField]
    private Transform initPos;

    private bool hasArrivalToInitPos;

    private bool canSetNextDestination;

    private Vector3 destination;

    public bool IsHitBoss { get; set; }
    // Update is called once per frame
    void Update()
    {
        if (IsHitBoss)
            return;

        if (!hasArrivalToInitPos)
        {
            if (Vector3.Distance(initPos.position, transform.position) < 5 || IsHitBoss)
            {
                canSetNextDestination = true;
                hasArrivalToInitPos = true;
            }
            else
            {
                MoveToInitPos();
            }
        }
        else
        {

            if (canSetNextDestination)
            {
                SetRandomDestination();
                if (!IsHitBoss)
                    canSetNextDestination = false;
            }
            else
            {
                if (Vector3.Distance(destination,
                        transform.position) < 5f)
                {
                    canSetNextDestination = true;
                }
                else
                {
                    MoveToRandomPos();
                }
            }

        }
        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * pingPongSpeed, pingPongLength), transform.position.z);
    }


    void MoveToInitPos()
    {
        Vector3 direction = (initPos.position - transform.position).normalized;

        transform.position += direction * Time.fixedDeltaTime * 60 * moveSpeed;
    }

    void SetRandomDestination()
    {
        Vector3 randomSphere = Random.insideUnitSphere * randomRange + Vector3.one;
        destination = new Vector3(initPos.position.x + randomSphere.x, initPos.position.y, initPos.position.z + randomSphere.z);
    }
    void MoveToRandomPos()
    {
        Vector3 direction = (destination - transform.position).normalized;

        transform.position += direction * Time.fixedDeltaTime * 60 * moveSpeed;
    }
}
