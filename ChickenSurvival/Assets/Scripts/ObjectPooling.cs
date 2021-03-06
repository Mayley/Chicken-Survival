using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
public class ObjectPooling : MonoBehaviour {

	public static ObjectPooling instance;

	public List<GameObject> _object;
	public List<bool> willGrow;
	public List<int> poolSize;

	private List<List<GameObject>> obstaclePool = new List<List<GameObject>> ();

	// Use this for initialization
	void Awake () {
		instance = this;
		CreatePool();
		SetupPool ();
	}

	void Start () {
		
	}

	void CreatePool () {
		for (int i = 0; i < _object.Count; i++) {
			obstaclePool.Add(new List<GameObject>(poolSize[i]));
		}
	}

	public GameObject GetPooledObject (int index) {

		List<GameObject> pool = obstaclePool [index];

		if (pool.Count > 0) {
			for (int i = 0; i < pool.Count; i++) {
				if (!pool [i].activeInHierarchy) {
					return pool [i];
				} 
			}

			//If no object is found, grow the pool, and restart loop to check for an available object
			if (willGrow [index]) {
				GameObject obj = Instantiate (_object [index]);
				obj.SetActive (false);
				string parent = _object [index].name + " pool";
				obj.transform.parent = GameObject.Find (parent).transform;
				obj.SetActive (false);
				pool.Add (obj);
				return pool [pool.Count - 1];
			}
		}
		//If pool size is 0 then return no object
		return null;		
	}
		
	void SetupPool (){
		int i = 0;

		//Create a holding gameObject within the scene for our pools
		for (int r = 0; r < obstaclePool.Count; r++){
			GameObject poolTitle = new GameObject();
			poolTitle.name = _object [r].name + " pool";
			poolTitle.transform.parent = GameObject.Find ("Spawner").gameObject.transform;
		}

		//Create the X (poolSize) objects within each pool for that object
		foreach (List<GameObject> pool in obstaclePool) {
			for (int x = 0; x < poolSize[i]; x++) {
				GameObject obj = Instantiate (_object [i]);
				string parent = _object [i].name + " pool";
				obj.transform.parent = GameObject.Find (parent).transform;
				obj.SetActive (false);
				pool.Add (obj);
			}
			i++;
		}
	}
}
