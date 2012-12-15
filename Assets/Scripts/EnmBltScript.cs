using UnityEngine;
using System.Collections;

public class EnmBltScript : MonoBehaviour {
	
	Vector3 dir;
	poolScript pool;
	gameMainScript main;
	
	// Use this for initialization
	void Start () {
		pool = GetComponent("poolScript") as poolScript;
		main = transform.Find("/gameMain").GetComponent("gameMainScript") as gameMainScript;
	}
	
	// Update is called once per frame
	void Update () {

		//transform.Translate(new Vector3(0,Time.deltaTime * -13,0));
		transform.Translate(dir * Time.deltaTime);
		if(transform.position.y < -5)
		{
			pool.moveToPool();
		}		
	}
	
	void setVec(Vector3 _vec)
	{
		dir = _vec;
	}
	

	void OnTriggerEnter(Collider other)
	{

		if(!gameObject.active) return;
		pool.moveToPool();
		main.changeModeToGameover();
	}
	
}
