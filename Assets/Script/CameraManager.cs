using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineComponent;

    public void SetZoom(float value, float time = 2, Action actionOnZoom = null)
    {

        if (time == 0)
        {
            //Instant Zoom
            cinemachineComponent.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = value;
        }
        else
            StartCoroutine(SetZoomCoroutine(value, time));

    }

    public IEnumerator SetZoomCoroutine(float value, float timeToZoom, Action actionOnZoom = null)
    {
        float elapsed = 0;
        float time = timeToZoom;

        if (timeToZoom == 0)
        {
            //Instant zoom
            cinemachineComponent.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = value;
            yield return null;
        }

        float startingValue = cinemachineComponent.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance;

        while (elapsed < time)
        {
            float zoom = Mathf.Lerp(startingValue, value, (elapsed / time));

            cinemachineComponent.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = zoom;

            elapsed += Time.deltaTime;

            yield return null;
        }
        if (actionOnZoom != null)
            actionOnZoom();

    }
}
