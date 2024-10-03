using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolDictionary : MonoBehaviour
{
	#region Fields
	private List<Pool> _pool;
	#endregion
	#region Properties
	public Dictionary<string, Queue<GameObject>> poolDictionary { get; set; }
	#endregion
	#region Public Methods
	public void ResetPool()
	{
		_pool = new List<Pool>();
	}
	public void SetPool(List<Pool> pool , Transform parent = null)
	{
		_pool = pool;
		poolDictionary = new Dictionary<string, Queue<GameObject>>();
		foreach (Pool item in _pool)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();
			for (int i = 0; i < item.Size; i++)
			{
				GameObject gmo = Instantiate(item.Prefab);
				gmo.name = item.Prefab.name;
				gmo.SetActive(false);
				if (parent != null) 
				{
					gmo.transform.parent = parent;
				}
				objectPool.Enqueue(gmo);
			}
			poolDictionary.Add(item.Name, objectPool);
		}
	}
	public GameObject SpawnFromPool(string name , Vector3 position, Transform parent = null)
	{
		if (!poolDictionary.ContainsKey(name))
		{
			Debug.LogError($"Gameobject could not be spawned Key: {name}");
			return null;
		}

		GameObject spawnedObj = poolDictionary[name].Dequeue();
		if (spawnedObj == null) 
		{
			return null;
		}

		spawnedObj.SetActive(true);
        if (parent != null)
        {
			spawnedObj.transform.SetParent(parent);
        }

		spawnedObj.transform.position = position;
		spawnedObj.transform.rotation = Quaternion.identity;	
		poolDictionary[name].Enqueue(spawnedObj);
		return spawnedObj;
    }
	#endregion
}
