using UnityEngine;

public class BubbleProperty : MonoBehaviour
{
    /// <summary>
    /// 存続時間
    /// </summary>
    [SerializeField]
    private float lastTime = 1f;
    public float LastTime
    {
        get { return lastTime; }
    }

    /// <summary>
    /// 最大サイズ
    /// </summary>
    [SerializeField]
    private float maxSize = 0.05f;
    public float MaxSize
    {
        get { return maxSize; }
    }

    /// <summary>
    /// ダメージで作られたときの最大サイズ
    /// </summary>
    [SerializeField]
    private float maxSizeInDamageCreated = 4;
    public float MaxSizeInDamageCreated
    {
        get { return maxSizeInDamageCreated; }
    }
}
