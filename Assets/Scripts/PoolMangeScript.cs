using UnityEngine;
using System.Collections;

public class PoolMangeScript : MonoBehaviour {
	
	public poolScript obj;
	public bool ready = false;
	
	// Use this for initialization
	void Start () {
	}
	
	public void Init(int num)
	{
		for(int i = 0; i < num; ++i)
		{
			Transform tmp = Instantiate(obj.transform,Vector3.zero,Quaternion.identity) as Transform;
			tmp.parent = this.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		ready = true;
	}
	
	public Transform ObjAwake()
	{
		if(transform.GetChildCount() <= 0) return null;
		Transform tmp = transform.GetChild(0);
		tmp.parent = transform.parent;
		tmp.gameObject.SetActiveRecursively(true);
		return tmp;
	}
}
