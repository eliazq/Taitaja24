using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CO2Warning : MonoBehaviour
{
    [SerializeField] private GameObject[] emissionParticles;
    [SerializeField] private VolumeProfile volume;
    private enum WarningStates
    {
        none,
        small,
        normal,
        high
    }

    WarningStates state;

    private void Update()
    {
        UpdateState();
        switch (state)
        {
            case WarningStates.small:
                SmallWarning();
                break;
            case WarningStates.normal:
                NormalWarning();
                break;
            case WarningStates.high:
                HighWarning();
                break;
            case WarningStates.none:
                SetParticlesActive(0);
                if (volume.TryGet(out ChannelMixer mixer))
                {
                    mixer.redOutGreenIn.value = 0;
                }
                break;
        }
    }

    private void OnApplicationQuit()
    {
        if (volume.TryGet(out ChannelMixer mixer))
        {
            mixer.redOutGreenIn.value = 0;
        }
    }

    private void UpdateState()
    {
        float co2Emission = CO2EmissionManager.Instance.CO2Emission;

        if (co2Emission > 1.5f && co2Emission < 3)
            state = WarningStates.small;
        else if (co2Emission > 3 && co2Emission < 5)
            state = WarningStates.normal;
        else if (co2Emission > 5)
            state = WarningStates.high;
        else
        {
            state = WarningStates.none;
        }
    }

    private void SmallWarning()
    {
        SetParticlesActive(1);
        if(volume.TryGet(out ChannelMixer mixer))
        {
            mixer.redOutGreenIn.value = 15;
        }
        
    }

    private void NormalWarning()
    {
        SetParticlesActive(2);
        if (volume.TryGet(out ChannelMixer mixer))
        {
            mixer.redOutGreenIn.value = 30;
        }
    }

    private void HighWarning()
    {
        SetParticlesActive(4);
        if (volume.TryGet(out ChannelMixer mixer))
        {
            mixer.redOutGreenIn.value = 50;
        }
    }

    private void SetParticlesActive(int amount)
    {
        if (amount <= 0)
        {
            foreach (GameObject particle in emissionParticles)
            {
                particle.SetActive(false);
            }
            return;
        }
        for (int i = 0; i < amount; i++)
        {
            emissionParticles[i].SetActive(true);
        }
    }
}
