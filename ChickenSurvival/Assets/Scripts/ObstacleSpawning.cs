using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObstacleSpawning : MonoBehaviour {
	public static ObstacleSpawning instance;

	public bool isSpawning;
	public bool stopSpawning;

	private List<GameObject> spawnLocations = new List<GameObject> ();

	[Range(.5f,5f)]
	public float waitTime = 5;
	[Range(1,13)]
	public int spawnAmount = 6;

	private float timer;

	void Awake() {
		GetSpawnLocations ();
		instance = this;
	}

	void GetSpawnLocations () {
		//Get all objects with tag Spawner, then order them by name
		spawnLocations = new List<GameObject> (GameObject.FindGameObjectsWithTag ("Spawner").OrderBy( go => go.name));	
	}

	// Use this for initialization
	void Start () {
		
		StartCoroutine (spawnObject());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator spawnObject () {
		while (!stopSpawning) {
			Debug.Log ("Trying to spawn object");
			while (isSpawning) {
				//Make temp list of available spawn locations this wave & get all spawnlocations
				List<GameObject> _spawnLocations = new List<GameObject> (spawnLocations);

				for (int i = 0; i < spawnAmount; i++) {			
					//Get a random spawn location, and then set spawnLoc to that position.
					int rsl = Random.Range (0, _spawnLocations.Count);
					Vector3 spawnLoc = _spawnLocations [rsl].transform.position;
					 
					//Random Obj from pool
					int ro = Random.Range (0,ObjectPooling.instance._object.Count);
					//Get object from pool
					GameObject obj = ObjectPooling.instance.GetPooledObject (ro);
					//Move obj to spawnLoc
					obj.transform.position = spawnLoc;
					//Random rotation
					//obj.transform.rotation = Quaternion.Euler(0,0,Random.Range(0f,360f));
					//Set obj active
					obj.SetActive (true);

					//Remove spawn location from list of available spawn locations
					_spawnLocations.RemoveAt (rsl);
				}

				yield return new WaitForSeconds (waitTime); 
			}
			yield return new WaitForSeconds (waitTime); 
		}
	}


}
