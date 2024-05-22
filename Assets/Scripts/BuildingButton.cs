using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private BuildingDataSO buildingDataSO;
    [SerializeField] private GameObject infoContainer;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private TextMeshProUGUI cooldownText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        titleText.text = buildingDataSO.name;
        costText.text = "Cost " + buildingDataSO.cost.ToString() + "$";
        incomeText.text = "Income " + buildingDataSO.Income.ToString() + "$";
        cooldownText.text = "Production " + buildingDataSO.cooldown.ToString() + "s";

        infoContainer.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        infoContainer.SetActive(false);
    }



}
