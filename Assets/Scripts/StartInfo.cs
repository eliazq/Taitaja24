using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInfo : MonoBehaviour
{
    [SerializeField] private GameObject startInfo;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("HasPlayed"))
        {
            PlayerPrefs.SetInt("HasPlayed", 1);
            startInfo.SetActive(true);
        }
    }
}
