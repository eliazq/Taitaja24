using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask gridPlatformLayer;
    [SerializeField] private Material gridPlatformSelectedMaterial;

    private Material gridPlatformMaterial;
    GameObject lastSelectedGridPlatform;

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
}
