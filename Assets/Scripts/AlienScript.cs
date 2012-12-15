using UnityEngine;
using System.Collections;

public class AlienScript : MonoBehaviour {
	
	int level;

	public Material[] mates;
	
	// Use this for initialization
	void Start () {
		level = 0;
		SetColor();
	}
	
	void SetColor()
	{
		renderer.material = mates[level/2];
	}
	// Update is called once per frame
	void Update () {
	
	}
	
	public void LevelUp()
	{
		if(level < 8) level += 1;
		SetColor();
	}
	public int getLevel()
	{
		return level/2;
	}
	public void setLevel(int _level)
	{
		level = _level;
		SetColor();
	}
}
