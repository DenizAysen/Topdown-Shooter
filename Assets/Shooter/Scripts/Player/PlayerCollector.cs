using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private readonly string _finish = "Finish";
    #region Actions
    public static Action onFinished; 
    #endregion
    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.TryGetComponent<ICollectible>(out var colletible))
        //{
        //    colletible.Collected();
        //}
        //if(collision.gameObject.TryGetComponent<IObstacle>(out var obstacle))
        //{
        //    obstacle.Hit();
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(_finish))
            onFinished?.Invoke();
    }
}
