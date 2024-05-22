using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] private BuildingDataSO buildingDataSO;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image progressBar;

    public BuildingDataSO Data { get {  return buildingDataSO; } }

    float processTimer;
    private void Update()
    {
        RotateTowardsPlayer(canvas);

        processTimer += Time.deltaTime;
        if (processTimer >= buildingDataSO.cooldown)
        {
            processTimer = 0;
            Player.Instance.GetPaid(buildingDataSO.Income);
            Player.Instance.CO2TotalEmission += buildingDataSO.Co2Emission;
        }
        progressBar.fillAmount = processTimer / buildingDataSO.cooldown;
    }

    private void RotateTowardsPlayer(GameObject gameObject)
    {
        Vector3 direction = Camera.main.transform.position - gameObject.transform.position;

        gameObject.transform.rotation = Quaternion.LookRotation(direction);
    }


}
