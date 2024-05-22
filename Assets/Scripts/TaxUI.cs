using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaxUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI taxText;
    [SerializeField] private float taxShowTime = 1.5f;

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        // Store the initial position of the object
        startPosition = transform.position;

        // Calculate the target position (move down by some amount, adjust as needed)
        targetPosition = startPosition - new Vector3(0f, 38f, 0f); // Move down by 1 unit

        Player.Instance.OnTaxPay += Instance_OnTaxPay;
    }

    private void Instance_OnTaxPay(object sender, Player.OnTaxEventArgs e)
    {
        taxText.text = "tax " +  "-" + e.tax.ToString();
        StartCoroutine(MoveObject());
        StartCoroutine(ShowAndHideTaxText());
    }

    IEnumerator MoveObject()
    {
        float elapsedTime = 0f;

        // Move the object downwards
        while (elapsedTime < taxShowTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / taxShowTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches the target position exactly
        transform.position = targetPosition;

        // Wait for a short duration (e.g., 1 second) before moving back
        yield return new WaitForSeconds(1f);

        // Move the object back to its original position
        elapsedTime = 0f;
        while (elapsedTime < taxShowTime)
        {
            transform.position = Vector3.Lerp(targetPosition, startPosition, elapsedTime / taxShowTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object reaches the original position exactly
        transform.position = startPosition;
    }

    IEnumerator ShowAndHideTaxText()
    {
        taxText.gameObject.SetActive(true);
        yield return new WaitForSeconds(taxShowTime);
        taxText.gameObject.SetActive(false);
    }
}
