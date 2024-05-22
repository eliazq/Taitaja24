using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BuildingDataSO : ScriptableObject
{
    public new string name = "building";
    public int cost = 10000;
    public float Co2Emission = 0.049f; // 0.016 in second small oil rig
    public float cooldown = 3;
    public int Income = 80;
}
