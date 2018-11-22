using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SetBoxColliderVertice : MonoBehaviour
{
    BoxCollider boxCollider;

    /// <summary>
    /// FIXME:各頂点を格納する型をfloatにできないか？
    /// </summary>
    [HideInInspector]
    public Vector3[] lengthVertices = new Vector3[2];
    [HideInInspector]
    public Vector3[] heightVertices = new Vector3[2];
    [HideInInspector]
    public Vector3[] depthVertices = new Vector3[2];
    // Use this for initialization
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        SetVertices(ref lengthVertices, ref heightVertices, ref depthVertices);
    }

    // Update is called once per frame
    void Update()
    {
        SetVertices(ref lengthVertices, ref heightVertices, ref depthVertices);
        
        float lengthMagnitude = 0;
        float depthMagnitude = 0;

        UpdateLength(lengthMagnitude, depthMagnitude);
        //長いほうをlengthとします
        if (depthMagnitude > lengthMagnitude)
            ChangeLengthVerticesToDepthVertices();
    }

    private void UpdateLength(float _lengthMagnitude, float _depthMagnitude)
    {
        _lengthMagnitude = Vector3.Distance(new Vector3(lengthVertices[0].x, 0, 0)
            , new Vector3(lengthVertices[1].x, 0, 0));
        _depthMagnitude = Vector3.Distance(new Vector3(depthVertices[0].x, 0, 0)
            , new Vector3(depthVertices[1].x, 0, 0));

    }
    private void ChangeLengthVerticesToDepthVertices()
    {
        Vector3[] tmpPos = new Vector3[2];
        tmpPos[0] = lengthVertices[0];
        tmpPos[1] = lengthVertices[1];

        lengthVertices[0] = depthVertices[0];
        lengthVertices[1] = depthVertices[1];

        depthVertices[0] = tmpPos[0];
        depthVertices[1] = tmpPos[1];

    }
    private void SetVertices(ref Vector3[] lengthVertices, ref Vector3[] heightVertices, ref Vector3[] depthVertices)
    {
        SetVerticesInLength(ref lengthVertices);
        SetVerticesInHeight(ref heightVertices);
        SetVerticesInDepth(ref depthVertices);

    }
    private void SetVerticesInLength(ref Vector3[] lengthVertices)
    {
        Vector3 lengthVertice1 = transform.TransformPoint(boxCollider.center +
new Vector3(-boxCollider.size.x, 0, 0) * 0.5f);

        Vector3 lengthVertice2 = transform.TransformPoint(boxCollider.center +
new Vector3(boxCollider.size.x, 0, 0) * 0.5f);

        lengthVertices[0] = lengthVertice1;
        lengthVertices[1] = lengthVertice2;
    }

    private void SetVerticesInHeight(ref Vector3[] heightVertices)
    {
        Vector3 heightVertice1 = transform.TransformPoint(boxCollider.center +
new Vector3(0, -boxCollider.size.y, 0) * 0.5f);

        Vector3 heightVertice2 = transform.TransformPoint(boxCollider.center +
new Vector3(0, boxCollider.size.y, 0) * 0.5f);

        heightVertices[0] = heightVertice1;
        heightVertices[1] = heightVertice2;
    }

    private void SetVerticesInDepth(ref Vector3[] depthVertices)
    {
        Vector3 depthVertice1 = transform.TransformPoint(boxCollider.center +
            new Vector3(0, 0, -boxCollider.size.z) * 0.5f);

        Vector3 depthVertice2 = transform.TransformPoint(boxCollider.center +
    new Vector3(0, 0, boxCollider.size.z) * 0.5f);

        depthVertices[0] = depthVertice1;
        depthVertices[1] = depthVertice2;
    }
}
