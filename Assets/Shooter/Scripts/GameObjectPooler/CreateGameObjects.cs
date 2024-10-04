using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PoolDictionary))]
public class CreateGameObjects : SingletonCreator<CreateGameObjects>
{
    #region Unity Fields
    [SerializeField] private List<Pool> pooledGameObjects;
    #endregion
    #region Fields
    private PoolDictionary _poolDictionary;
    private Transform _destinationParentTransform;
    #endregion
    #region Unity Methods
    void Awake()
    {
        StartPooling();
    }
    #endregion
    #region Public Methods
    public void StartPooling()
    {
        _destinationParentTransform = transform;
        _poolDictionary = GetComponent<PoolDictionary>();
        _poolDictionary.ResetPool();
        _poolDictionary.SetPool(pooledGameObjects,_destinationParentTransform);
    }
    public GameObject CreateGameObject(string prefabName, Vector3 pos, Transform parent = null)
    {
        return _poolDictionary.SpawnFromPool(prefabName, pos, parent);
    }
    #endregion
}
