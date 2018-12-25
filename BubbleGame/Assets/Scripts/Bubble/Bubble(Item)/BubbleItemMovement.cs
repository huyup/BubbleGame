using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(initPos.position, transform.position) < 5)
        {

            canSetNextDestination = true;
            hasArrivalToInitPos = true;
        }

        if (!hasArrivalToInitPos)
            MoveToInitPos();
        else
        {
            Debug.Log("Distance" + Vector3.Distance(initPos.position, transform.position));
            if (Vector3.Distance(destination-new Vector3(0, destination.y, 0), transform.position - new Vector3(0, transform.position.y, 0)) < 2f)
                canSetNextDestination = true;

            if (canSetNextDestination)
            {
                SetRandomDestination();
                canSetNextDestination = false;
            }

            MoveToRandomPos();
        }

        transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * pingPongSpeed, pingPongLength), transform.position.z);
    }

    void MoveToInitPos()
    {
        Debug.Log("MoveToInitPos");
        Vector3 direction = (initPos.position - transform.position).normalized;

        transform.position += direction * Time.fixedDeltaTime * 60 * moveSpeed;
    }

    void SetRandomDestination()
    {
        destination = initPos.transform.position + Random.insideUnitSphere * randomRange;
    }
    void MoveToRandomPos()
    {
        Vector3 direction = (destination - transform.position).normalized;

        transform.position += direction * Time.fixedDeltaTime * 60 * moveSpeed;
    }
}
