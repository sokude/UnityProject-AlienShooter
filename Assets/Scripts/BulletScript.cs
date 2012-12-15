using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	
	poolScript pool;
	gameMainScript main;
	
	public bool flg;
	
	// Use this for initialization
	void Start () {
		pool = GetComponent("poolScript") as poolScript;
		main = transform.Find("/gameMain").GetComponent("gameMainScript") as gameMainScript;
		flg = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		transform.Translate(new Vector3(0,Time.deltaTime * 13,0));
		if(transform.position.y > 20)
		{
			pool.moveToPool();
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(!gameObject.active) return;
		AlienScript a = other.GetComponent("AlienScript") as AlienScript;
		Vector3 rnd =  new Vector3( Random.Range(-1.0f,1.0f) ,0,0);
		int lev = a.getLevel();

		switch(lev)
		{
		case 4:
			main.shotEnmBlt(transform.position+rnd,(main.ShipInstance.position - (transform.position+rnd)));
			
			main.shotEnmBlt(transform.position + rnd,new Vector3(1,-8,0));
			main.shotEnmBlt(transform.position + rnd,new Vector3(-1,-8,0));
			main.shotEnmBlt(transform.position + rnd,new Vector3(2,-8,0));
			main.shotEnmBlt(transform.position + rnd,new Vector3(-2,-8,0));
			main.shotEnmBlt(transform.position + rnd,new Vector3(0,-8,0));
			break;
		case 3:
			main.shotEnmBlt(transform.position + rnd,new Vector3(1,-5,0));
			main.shotEnmBlt(transform.position + rnd,new Vector3(-1,-5,0));
			main.shotEnmBlt(transform.position + rnd,new Vector3(2,-5,0));
			main.shotEnmBlt(transform.position + rnd,new Vector3(-2,-5,0));
			main.shotEnmBlt(transform.position + rnd,new Vector3(0,-5,0));
			break;
		case 2:
			main.shotEnmBlt(transform.position + rnd,new Vector3(1.5f,-5,0));
			main.shotEnmBlt(transform.position + rnd,new Vector3(-1.5f,-5,0));
			main.shotEnmBlt(transform.position + rnd,new Vector3(0,-5,0));
			break;
		case 1:
			main.shotEnmBlt(transform.position + rnd,new Vector3(0,-5,0));
			break;
		case 0:
			break;
		}
		
		
		pool.moveToPool();
		a.transform.position = new Vector3( Random.Range(-10,10) ,50,0);
		a.LevelUp();
		main.addScore(10);
	}
}
