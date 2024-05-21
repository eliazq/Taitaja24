using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingDataSO buildingDataSO;

    public BuildingDataSO Data { get {  return buildingDataSO; } }

    float processTimer;
    private void Update()
    {
        processTimer += Time.deltaTime;

        if (processTimer >= buildingDataSO.cooldown)
        {
            processTimer = 0;
            Player.Instance.GetPaid(buildingDataSO.Income);
            Player.Instance.CO2TotalEmission += buildingDataSO.Co2Emission;
        }
    }


}
