using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public event EventHandler OnMoneyChanged;
    [SerializeField] private LayerMask gridPlatformLayer;
    [SerializeField] private Material gridPlatformSelectedMaterial;
    [SerializeField] private GameObject selectedBuildingPrefab;
    [SerializeField] private GameObject[] buildingPrefabs;
    [SerializeField] private float moneyBackPercent = 0.25f;
    private float money;
    public float Money 
    {
        get { return money; }
        private set 
        {
            money = value;
            OnMoneyChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public float CO2Emission { get; set; }
    public float CO2TotalEmission { get; set; }
    private Material gridPlatformMaterial;
    GameObject lastSelectedGridPlatform;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        Money = 500; // 9M For debuging
    }

    private void Update()
    {
        HandleGrid();
    }

    [NaughtyAttributes.Button]
    private void Get100()
    {
        GetPaid(100);
    }

    public void SelectBuilding(string buildingName)
    {
        bool foundMatch = false;
        foreach(GameObject building in buildingPrefabs)
        {
            if (buildingName == building.GetComponent<Building>().Data.name)
            {
                selectedBuildingPrefab = building;
                foundMatch = true;
                break;
            }
        }
        if (!foundMatch) selectedBuildingPrefab = null;
        
    }

    private void HandleGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, gridPlatformLayer))
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Mouse Clicked While Hovering Platform
                if (hit.collider.TryGetComponent(out BuildingPlatform buildingPlatform) && !buildingPlatform.hasBuilding && selectedBuildingPrefab != null)
                {
                    if (BuildingManager.Instance.BuyBuilding(selectedBuildingPrefab.GetComponent<Building>(), hit.transform))
                        buildingPlatform.hasBuilding = true;
                }
                else if (buildingPlatform.hasBuilding && selectedBuildingPrefab == null)
                {
                    // Remove Building
                    GameObject buildingFromPlatform = hit.transform.GetChild(0).gameObject;
                    float moneyReturn = buildingFromPlatform.GetComponent<Building>().Data.cost * moneyBackPercent;
                    Destroy(buildingFromPlatform);
                    hit.transform.GetComponent<BuildingPlatform>().hasBuilding = false;
                    GetPaid((int)moneyReturn);
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (hit.collider.TryGetComponent(out BuildingPlatform buildingPlatform) && buildingPlatform.hasBuilding)
                {
                    // Remove Building
                    GameObject buildingFromPlatform = hit.transform.GetChild(0).gameObject;
                    float moneyReturn = buildingFromPlatform.GetComponent<Building>().Data.cost * moneyBackPercent;
                    Destroy(buildingFromPlatform);
                    hit.transform.GetComponent<BuildingPlatform>().hasBuilding = false;
                    GetPaid((int)moneyReturn);
                }
            }
            if (hit.collider.TryGetComponent(out MeshRenderer meshRenderer))
            {
                // Selected grid Platform is changed, change the old selected material to its normal mat
                if (lastSelectedGridPlatform != null)
                {
                    if (hit.collider.gameObject != lastSelectedGridPlatform) lastSelectedGridPlatform.GetComponent<MeshRenderer>().material = gridPlatformMaterial;
                }
                else gridPlatformMaterial = meshRenderer.material;

                // selected grid platform material to new selected
                meshRenderer.material = gridPlatformSelectedMaterial;
                lastSelectedGridPlatform = hit.collider.gameObject;
            }
        }
        else if (lastSelectedGridPlatform != null && lastSelectedGridPlatform.GetComponent<MeshRenderer>().material != gridPlatformMaterial)
            lastSelectedGridPlatform.GetComponent<MeshRenderer>().material = gridPlatformMaterial;
    }

    public void GetPaid(int amount)
    {
        Money += amount;
    }

    public void Pay(int amount)
    {
        Money -= amount;
    }

    public void PayTax()
    {
        // Calculate tax amount based on the tax rate obtained from CO2EmissionManager
        float taxRate = CO2EmissionManager.Instance.GetTaxRate();
        int taxAmount = (int)(Money * taxRate);

        // Deduct tax from the player's money
        Money -= taxAmount;
    }
}
