using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRotator : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    private void Start()
    {
        transform.DORotate(new Vector3(0,360,0),rotateSpeed,RotateMode.FastBeyond360)
            .SetLoops(-1).SetEase(Ease.Linear);
    }
}
