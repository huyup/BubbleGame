using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CalculateBoxColliderVertices : MonoBehaviour
{
    private BoxCollider boxCollider;

    /// <summary>
    /// FIXME:各頂点を格納する型をfloatにできないか？
    /// </summary>
    [HideInInspector]
    public Vector3[] LengthVertices = new Vector3[2];
    [HideInInspector]
    public Vector3[] HeightVertices = new Vector3[2];
    [HideInInspector]
    public Vector3[] DepthVertices = new Vector3[2];

    // Use this for initialization
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        SetVertices(ref LengthVertices, ref HeightVertices, ref DepthVertices);
    }

    // Update is called once per frame
    void Update()
    {
        SetVertices(ref LengthVertices, ref HeightVertices, ref DepthVertices);
        
        float lengthMagnitude = 0;
        float depthMagnitude = 0;

        UpdateLength(ref lengthMagnitude,ref depthMagnitude);

        //長いほうをlengthとします
        if (depthMagnitude > lengthMagnitude)
            ChangeLengthVerticesToDepthVertices();
    }

    private void UpdateLength(ref float lengthMagnitude,ref float depthMagnitude)
    {
        lengthMagnitude = Vector3.Distance(new Vector3(LengthVertices[0].x, 0, 0)
            , new Vector3(LengthVertices[1].x, 0, 0));
        depthMagnitude = Vector3.Distance(new Vector3(DepthVertices[0].x, 0, 0)
            , new Vector3(DepthVertices[1].x, 0, 0));
    }
    private void ChangeLengthVerticesToDepthVertices()
    {
        Vector3[] tmpPos = new Vector3[2];
        tmpPos[0] = LengthVertices[0];
        tmpPos[1] = LengthVertices[1];

        LengthVertices[0] = DepthVertices[0];
        LengthVertices[1] = DepthVertices[1];

        DepthVertices[0] = tmpPos[0];
        DepthVertices[1] = tmpPos[1];

    }
    private void SetVertices(ref Vector3[] lengthVertices, ref Vector3[] heightVertices, ref Vector3[] depthVertices)
    {
        SetVerticesInLength(ref lengthVertices);
        SetVerticesInHeight(ref heightVertices);
        SetVerticesInDepth(ref depthVertices);

    }
    private void SetVerticesInLength(ref Vector3[] lengthVertices)
    {
        Vector3 lengthVertex1 = transform.TransformPoint(boxCollider.center +
new Vector3(-boxCollider.size.x, 0, 0) * 0.5f);

        Vector3 lengthVertex2 = transform.TransformPoint(boxCollider.center +
new Vector3(boxCollider.size.x, 0, 0) * 0.5f);

        lengthVertices[0] = lengthVertex1;
        lengthVertices[1] = lengthVertex2;
    }

    private void SetVerticesInHeight(ref Vector3[] heightVertices)
    {
        Vector3 heightVertex1 = transform.TransformPoint(boxCollider.center +
new Vector3(0, -boxCollider.size.y, 0) * 0.5f);

        Vector3 heightVertex2 = transform.TransformPoint(boxCollider.center +
new Vector3(0, boxCollider.size.y, 0) * 0.5f);

        heightVertices[0] = heightVertex1;
        heightVertices[1] = heightVertex2;
    }

    private void SetVerticesInDepth(ref Vector3[] depthVertices)
    {
        Vector3 depthVertex1 = transform.TransformPoint(boxCollider.center +
            new Vector3(0, 0, -boxCollider.size.z) * 0.5f);

        Vector3 depthVertex2 = transform.TransformPoint(boxCollider.center +
    new Vector3(0, 0, boxCollider.size.z) * 0.5f);

        depthVertices[0] = depthVertex1;
        depthVertices[1] = depthVertex2;
    }
}
