using System.Collections;
using UnityEngine;

public class CubeSpawn : MonoBehaviour {
	
	public Vector3 ObjectSpawnPosition;
	public GameObject obj;
	
	void Update(){
		if(Input.GetKeyDown(KeyCode.X)) {
			Instantiate(obj, ObjectSpawnPosition, Quaternion.identity);
		}
	}
}
