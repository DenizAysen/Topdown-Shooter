using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonVariables;

public class CollectableBase : MonoBehaviour,ICollectable
{
    #region Unity Fields
    [SerializeField] ParticleSystem collectedParticle;
    [SerializeField] CollectableType collectableItemType;
    #endregion
    #region Fields
    private ParticleSystem _createdParticle;
    #endregion
    #region Properties
    public CollectableType ItemType { get; set; }
    #endregion
    #region Unity Methods
    private void OnEnable()
    {
        _createdParticle = Instantiate(collectedParticle,null);
        _createdParticle.gameObject.SetActive(false);
        ItemType = collectableItemType;
    }
    #endregion
    public void Collected()
    {
        _createdParticle.gameObject.SetActive(true);
        _createdParticle.transform.position = transform.position;
        _createdParticle.Play();
        transform.DOScale(Vector3.zero, .5f).OnComplete(() =>
        {
            gameObject.transform.position.Scale(Vector3.one);
            gameObject.SetActive(false);
        } );
    }

}
