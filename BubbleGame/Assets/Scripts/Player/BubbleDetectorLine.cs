using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDetectorLine : MonoBehaviour
{
    [SerializeField]
    private float minAngle = 10;

    [SerializeField]
    private float minDistance = 5;

    [SerializeField]
    private float factorToCalArrowPos = 1.5f;
    
    private List<GameObject> bubbles = new List<GameObject>();

    [SerializeField]
    private GameObject arrow;

    [SerializeField]
    private GameObject player;

    private GameObject targetBubble;

    public bool CanUseShootSupportLine { get; set; }

    private Vector3 bubblePosition;
    private Vector3 bubbleForwardPosition;

    private bool canSetArrowVisible = false;
    private bool canSetArrowInvisible = false;

    private void Start()
    {
        CanUseShootSupportLine = false;
        var arrowRenders = arrow.GetComponentsInChildren<Renderer>();
        foreach (var arrowRender in arrowRenders)
        {
            arrowRender.enabled = false;
        }
    }
    private void Update()
    {

        if (!CanUseShootSupportLine)
        {
            SetArrowInvisible();
            return;
        }

        BubbleDetector();

        if (canSetArrowVisible)
        {
            SetArrowVisible();
            //canSetArrowVisible = false;
        }

        if (canSetArrowInvisible)
        {
            SetArrowInvisible();
            canSetArrowInvisible = false;
        }
    }

    public void BubbleDetector()
    {
        int bubbleWithoutArrowCount = 0;

        foreach (var bubble in bubbles)
        {
            if (!bubble)
            {
                bubbleWithoutArrowCount++;
                continue;
            }

            var targetPositionXZ = bubble.transform.position - new Vector3(0, bubble.transform.position.y, 0);
            var selfPositionXZ = player.transform.position - new Vector3(0, player.transform.position.y, 0);

            var diff = targetPositionXZ - selfPositionXZ;

            var angle = Vector3.Angle(diff, player.transform.forward);
            var distance = Vector3.Distance(player.transform.position, bubble.transform.position);
            if (angle <= minAngle && distance <= minDistance)
            {
                if (bubble.GetComponent<BubbleController>().GetBubbleState() != BubbleState.BePressed)
                {
                    canSetArrowVisible = true;
                    targetBubble = bubble;
                }
                else
                {
                    bubbleWithoutArrowCount++;
                }
            }
            else
            {
                bubbleWithoutArrowCount++;
            }
        }
        if (bubbleWithoutArrowCount == bubbles.Count)
            canSetArrowInvisible = true;
    }

    private void SetArrowVisible()
    {
        if (!targetBubble)
            return;
        var arrowRenders = arrow.GetComponentsInChildren<Renderer>();
        foreach (var arrowRender in arrowRenders)
        {
            arrowRender.enabled = true;
        }
        var distance = Vector3.Distance(player.transform.position, targetBubble.transform.position);
        Vector3 positionXZ = player.transform.position +
                             player.transform.forward * factorToCalArrowPos * distance;

        arrow.transform.position = new Vector3(positionXZ.x, targetBubble.transform.position.y, positionXZ.z);
        arrow.transform.rotation = Quaternion.LookRotation(player.transform.forward);
    }

    private void SetArrowInvisible()
    {
        var arrowRenders = arrow.GetComponentsInChildren<Renderer>();
        foreach (var arrowRender in arrowRenders)
        {
            arrowRender.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10 /*Bubble*/
            && other.transform.parent.name != "BubbleSetForItem 1(Clone)")
        {
            bubbles.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10 /*Bubble*/
            && other.transform.parent.name != "BubbleSetForItem 1(Clone)")
        {
            bubbles.Remove(other.gameObject);
        }
    }
}
