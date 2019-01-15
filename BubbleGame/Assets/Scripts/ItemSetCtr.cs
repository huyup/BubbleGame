using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemSetCtr : MonoBehaviour
{
    [SerializeField] private GameObject itemBubbleExplosionEff;

    private GameObject bubble;

    private Transform playerRef;

    private bool canGetQuad;
    // Use this for initialization
    void Start()
    {
        bubble = transform.Find("Bubble").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (canGetQuad)
        {
            QuadFollow();
        }
    }
    private void QuadFollow()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.Find("Quad").DOMove(playerRef.position, 1));
        sequence.Join(transform.Find("Quad").DOScale(Vector3.zero, 1));

        Destroy(this.gameObject, 2);
    }
    public void GetItem(Transform _player)
    {
        playerRef = _player;
        Destroy(bubble);
        itemBubbleExplosionEff.GetComponent<ParticleSystem>().Play();

        canGetQuad = true;
        _player.GetComponent<PlayerController>().UseAirGun();
    }
}
