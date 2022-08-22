using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InitialData", menuName = "InitialData")]
public class InitialData: ScriptableObject
{
    [SerializeField]
    private int CubesCount;
    [SerializeField]
    private float SemiMajorAxis;
    [SerializeField]
    private float SemiMinorAxis;

    public int GetCubesCount()
    {
        return CubesCount;
    }

    public float GetSemiMajorAxis()
    {
        return SemiMajorAxis;
    }

    public float GetSemiMinorAxis()
    {
        return SemiMinorAxis;
    }
}
