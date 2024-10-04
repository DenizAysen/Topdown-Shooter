using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
	#region Unity Fields
	[SerializeField] CinemachineVirtualCamera cmVirtualCamera;
	[SerializeField] float shakeTimeOut = .3f;
	#endregion
	#region Fields
	CinemachineBasicMultiChannelPerlin _cmBasicMultiChannelPerlin;
    #endregion
    #region Unity Methods
    private void Awake()
    {
        _cmBasicMultiChannelPerlin = cmVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (_cmBasicMultiChannelPerlin == null)
        {
            Debug.LogError("No multichannelperlin");
        }
        _cmBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    #endregion
    #region Private Methods
    private void OnHit(float damage)
    {
        StartCoroutine(ShakeCameraroutine());
    }
    private IEnumerator ShakeCameraroutine()
    {
        _cmBasicMultiChannelPerlin.m_AmplitudeGain = 1f;
        yield return new WaitForSeconds(shakeTimeOut);
        _cmBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
    }
    #endregion
}
