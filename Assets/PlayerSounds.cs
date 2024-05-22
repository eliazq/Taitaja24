using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buildingPlaceClip;
    private void Start()
    {
        Player.Instance.OnBuildingPlaced += Instance_OnBuildingPlaced;
    }

    private void Instance_OnBuildingPlaced(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(buildingPlaceClip, Camera.main.transform.position);
    }
}
