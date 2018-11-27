using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleExplosionEffController : MonoBehaviour
{
    private ParticleSystem bubbleExplosionEff;

    private float minStartSize;
    private float maxStartSize;

    private float sphereRadius;

    [SerializeField]
    private float factorToCalTheMinSize = 0.5f;

    [SerializeField]
    private float factorToCalTheMaxSize = 1.3f;

    [SerializeField]
    private float factorToCalTheRadius = 2f;

    [SerializeField]
    private float explosionEffLastTime = 1.8f;

    private bool canSetEffParameter = false;

    // Use this for initialization
    void Start()
    {
        bubbleExplosionEff = GetComponent<ParticleSystem>();
    }

    public void SaveEffStartProperty(Vector3 _bubbleSize)
    {
        float averageScale = (_bubbleSize.x + _bubbleSize.y + _bubbleSize.z) / 3;
        minStartSize = averageScale * factorToCalTheMinSize;
        maxStartSize = averageScale * factorToCalTheMaxSize;

        sphereRadius = averageScale * factorToCalTheRadius;
        canSetEffParameter = true;
    }
    private void ChangeEffStartProperty(Vector3 _initPlayPos)
    {
        transform.position = _initPlayPos;

        ParticleSystem.MainModule mainModule = bubbleExplosionEff.main;

        mainModule.startSize = new ParticleSystem.MinMaxCurve(minStartSize, maxStartSize);

        ParticleSystem.ShapeModule shapeModule = bubbleExplosionEff.shape;

        shapeModule.radius = sphereRadius;
    }

    public void PlayExplosionEff(Vector3 _initPlayPos)
    {
        if (canSetEffParameter)
        {
            ChangeEffStartProperty(_initPlayPos);
            canSetEffParameter = false;
        }

        if (!bubbleExplosionEff.isPlaying)
        {
            bubbleExplosionEff.Play();
            Destroy(this.gameObject, explosionEffLastTime);
        }
    }
}
