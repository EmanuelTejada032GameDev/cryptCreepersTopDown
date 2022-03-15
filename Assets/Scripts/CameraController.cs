using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera _virtualCamera;
    CinemachineBasicMultiChannelPerlin _noise;

    private void Start()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float duration = 0.1f, float amplitude = 1.5f, float frequency = 20)
    {
        StopAllCoroutines();
        StartCoroutine(ApplyNoiseToCamera(duration, amplitude, frequency));
    }

    IEnumerator ApplyNoiseToCamera(float duration, float amplitude , float frequency)
    {

        _noise.m_AmplitudeGain = amplitude;
        _noise.m_FrequencyGain = frequency;
        yield return new WaitForSeconds(duration);
        _noise.m_AmplitudeGain = 0;
        _noise.m_FrequencyGain = 0;
    }
}
