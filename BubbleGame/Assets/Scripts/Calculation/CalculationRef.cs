using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationRef : MonoBehaviour
{
    [SerializeField]
    private CalculateBoxContainBox calculateContain;

    [SerializeField]
    private CalculateBiggerBoxCollider calculateBigger;


    public CalculateBoxContainBox GetContainFunction()
    {
        return calculateContain;
    }

    public CalculateBiggerBoxCollider GetBiggerFunction()
    {
        return calculateBigger;
    }
}
