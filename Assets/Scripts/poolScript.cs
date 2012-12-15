using UnityEngine;
using System.Collections;

public class poolScript : MonoBehaviour {

	public Transform pool;
	
	// Use this for initialization
	void Start () {
		gameObject.SetActiveRecursively(false);
		pool = transform.parent;
	}
	
	public void moveToPool()
	{
		gameObject.SetActiveRecursively(false);
		transform.position = new Vector3(0,0,0);
		transform.rotation = Quaternion.identity;		
		transform.parent = pool;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
