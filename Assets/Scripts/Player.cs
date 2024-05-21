using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private LayerMask gridPlatformLayer;
    [SerializeField] private Material gridPlatformSelectedMaterial;
    [SerializeField] private GameObject selectedBuildingPrefab;

    public int Money { get; private set; } = 9000000; // 9M
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

    private void Update()
    {
        HandleGrid();
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
                if (hit.collider.TryGetComponent(out BuildingPlatform buildingPlatform) && !buildingPlatform.hasBuilding)
                {
                    BuildingManager.Instance.BuyBuilding(selectedBuildingPrefab.GetComponent<Building>(), hit.transform.position);
                    buildingPlatform.hasBuilding = true;
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
}
