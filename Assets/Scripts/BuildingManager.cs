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

    public bool BuyBuilding(Building building, Transform buildingPlatform)
    {
        if (Player.Instance.Money >= building.Data.cost)
        {
            GameObject newBuilding = Instantiate(building.gameObject, buildingPlatform);
            newBuilding.transform.position = buildingPlatform.position;
            newBuilding.transform.localPosition = Vector3.zero;
            Player.Instance.Pay(building.Data.cost);
            return true;
        }
        return false;
    }
}
