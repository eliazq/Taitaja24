using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CO2EmissionManager : MonoBehaviour
{
    public static CO2EmissionManager Instance;

    public float CO2Emission { get; private set; }
    private float taxRate; // Tax rate as a percentage

    [SerializeField] private TextMeshProUGUI emissionText;

    [SerializeField] private float baseTaxRate = 0.1f; // Base tax rate as a percentage (10% in this example)
    [SerializeField] private float taxInterval = 10f; // Interval in seconds for tax collection

    private float taxTimer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    private void Update()
    {
        float emissions = 0;

        // Get all buildings emission data
        for (int i = 0; i < transform.childCount; i++)
        {
            float co2Emission = 0;

            Transform buildingPlatform = transform.GetChild(i);
            if (buildingPlatform.childCount > 0 && buildingPlatform.GetChild(0).TryGetComponent(out Building building))
            {
                co2Emission = building.Data.Co2Emission;
            }

            emissions += co2Emission;
        }

        CO2Emission = emissions;
        emissionText.text = "CO2: " + CO2Emission.ToString("F2");

        // Calculate the tax rate based on CO2 emission and base tax rate
        taxRate = CalculateTaxRate();

        // Manage tax payment interval
        taxTimer += Time.deltaTime;
        if (taxTimer >= taxInterval)
        {
            taxTimer = 0;
            Player.Instance.PayTax();
        }
    }

    private float CalculateTaxRate()
    {
        // Calculate tax rate based on some logic, for example, using CO2 emission as a factor
        // For simplicity, let's assume the tax rate increases linearly with CO2 emissions
        // You can replace this with your custom logic
        return baseTaxRate * (CO2Emission / 100.0f); // Assuming CO2Emission is a percentage of some maximum value
    }

    public float GetTaxRate()
    {
        return taxRate;
    }

}
