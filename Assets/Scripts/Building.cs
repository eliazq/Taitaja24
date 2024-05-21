using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingDataSO buildingDataSO;

    public BuildingDataSO Data { get {  return buildingDataSO; } }


}
