using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    public void BuyBuilding(Building building, Vector3 spawnPosition)
    {
        if (Player.Instance.Money >= building.Data.cost)
        {
            Instantiate(building.gameObject, spawnPosition, Quaternion.identity);
            Player.Instance.Pay(building.Data.cost);
        }
    }
}
