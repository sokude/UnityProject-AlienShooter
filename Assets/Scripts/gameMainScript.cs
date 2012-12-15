using UnityEngine;
using System.Collections;

public class gameMainScript : MonoBehaviour {
	
	public Transform ShipInstance;
	
	public moveSlideScript move;
	
	public Transform alienPrefab;
	Transform[] aliens;
	float scale = 100;

	float interval;
	
	public Transform pools;
	public PoolMangeScript bulletPool;
	public PoolMangeScript enmbltPool;
	
	float guiScale;
	float slideScale;
	
	enum GameMode
	{
		GAMEOVER,
		INGAME
	};
	
	GameMode gmode;
	
	public GUIText gameoverText;
	public GUIText touchtostartText;
	public GUIText scoreText;
	
	int score;
	
	Vector3 adjustPos(Vector3  _vec)
	{
		return new Vector3(_vec.x * slideScale + (0.5f - slideScale/2.0f),_vec.y,_vec.z);
	}
	
	// Use this for initialization
	void Start () {
		aliens = new Transform[40];
		for(int i = 0; i < 40; ++i)
		{
			aliens[i] = Instantiate(alienPrefab,new Vector3(0,15,0),Quaternion.identity) as Transform;
		}
		
		bulletPool.Init(15);
		enmbltPool.Init(50);
		scale = 0;
		setAlienPos();
		interval = 0;
		
		gmode = GameMode.GAMEOVER;		
		score = 0;
		
		
		GUITexture[] textures;
        textures = gameObject.GetComponentsInChildren<GUITexture>();
		guiScale = Screen.height / 480.0f;

        foreach (GUITexture tex in textures) {
			Rect tmp = tex.pixelInset;
			tmp.width *= guiScale;
			tmp.height *= guiScale;
			tmp.x *= guiScale;
			tmp.y *= guiScale;
			tex.pixelInset = tmp;

        }
		slideScale = (320*guiScale / Screen.width);
		
		move.calcSize();
		move.transform.position = adjustPos(move.transform.position);
	}
	
	void setAlienPos()
	{
		for(int i = 0; i < 40; ++i)
		{
			float a = (((i / 4) % 2) - 0.5f) * 2;
			float x = a * scale * (i / 8) + (a * scale*0.5f);
			//Vector3 tgtpos = new Vector3(x,(i % 4) * scale + 8,0);
			aliens[i].localPosition = Vector3.Lerp(aliens[i].localPosition,new Vector3(x,(i % 4) * scale + 13,0),0.1f);
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu))
		{
			Application.Quit();
			return;
		}
		if(!bulletPool.ready || !enmbltPool.ready)
		{
			return;
		}
		
		scale = Mathf.PingPong(Time.time/2.0f,0.5f) + 1;

		setAlienPos();
		
		
		if(gmode == GameMode.GAMEOVER)
		{
			scene_gameover();
		}
		else
		{
			scene_ingame();
		}
	}
	
	void scene_gameover()
	{
		if((Time.time % 1) < 0.5f)
		{
			touchtostartText.enabled = false;
		}
		else
		{
			touchtostartText.enabled = true;
		}
		if(move.trig)
		{
			gmode = GameMode.INGAME;
			gameoverText.gameObject.active = false;
			touchtostartText.gameObject.active = false;
			ShipInstance.gameObject.active = true;
			
			for(int i = 0; i < 40; ++i)
			{
				aliens[i].SendMessage("setLevel",0.0f);
			}
			poolScript[] p = pools.GetComponentsInChildren<poolScript>();
			foreach(poolScript obj in p)
			{

				obj.moveToPool();
			}
			score = 0;
			dispScore();
		}
	}
	
	void scene_ingame()
	{
		if(move.inact)
		{
			float hax = (move.posx - 0.5f) * 2.0f * 7.0f;
			
			if(hax < ShipInstance.position.x - 0.2f)
			{
				ShipInstance.Translate(new Vector3(-0.2f,0,0));
			}
			else if(hax > ShipInstance.position.x + 0.2f)
			{
				ShipInstance.Translate(new Vector3(0.2f,0,0));
			}
			else
			{
				ShipInstance.position = new Vector3(hax,0,0);
			}
		}
		ShipInstance.Translate(Vector3.zero);		
		if(interval <= 0)
		{
			Transform tmp = bulletPool.ObjAwake();
			if(tmp != null)
			{
				tmp.position = ShipInstance.position;
			}
			interval = 0.1f;
		}
		else
		{
			interval -= Time.deltaTime;
		}
	}
	
	public void changeModeToGameover()
	{
		gmode = GameMode.GAMEOVER;
		gameoverText.gameObject.active = true;
		touchtostartText.gameObject.active = true;
		ShipInstance.gameObject.active = false;
	}

	
	public void shotEnmBlt(Vector3 pos,Vector3 dir)
	{
		Transform tmp = enmbltPool.ObjAwake();
		if(tmp != null)
		{
			tmp.position = pos;
			tmp.SendMessage("setVec",dir);
		}
	}
	
	void dispScore()
	{
		scoreText.text = score.ToString("D6");
	}	
	
	public void addScore(int _add)
	{
		score += _add;
		dispScore();		
	}
	
	/*
	public void hitEffectAwake(Vector3 pos)
	{
		Transform tmp = hitEffectPool.ObjAwake();
		if(tmp != null)
		{
			tmp.position = pos;
		}
	}
	*/
}
