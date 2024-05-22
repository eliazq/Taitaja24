using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buildingPlaceClip;
    [SerializeField] private AudioClip buildingRemovedClip;
    private void Start()
    {
        Player.Instance.OnBuildingPlaced += Instance_OnBuildingPlaced;
        Player.Instance.OnBuildingRemoved += Instance_OnBuildingRemoved;
    }

    private void Instance_OnBuildingRemoved(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(buildingRemovedClip, Camera.main.transform.position, 0.3f);
    }

    private void Instance_OnBuildingPlaced(object sender, System.EventArgs e)
    {
        AudioSource.PlayClipAtPoint(buildingPlaceClip, Camera.main.transform.position);
    }
}
