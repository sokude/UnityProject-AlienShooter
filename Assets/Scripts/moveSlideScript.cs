using UnityEngine;
using System.Collections;



public class moveSlideScript : MonoBehaviour {
	
	public float posx;
	public float posy;
	public bool inact;
	public bool trig;
	public bool reltrig;
	
	public bool bkTrig;
	
	public int touchcnt;
	
	float x;
	float y;
	
	Rect textureRect;
	int touchedFinger;
	
	public int TouchId
	{
		get { return touchedFinger;}
	}
	
	
	// Use this for initialization
	void Start () {
		inact = false;
		posx = 0;
		posy = 0;
		reltrig = bkTrig = trig = false;
		
		touchedFinger = -1;
		
		calcSize();

	}
	
	public void calcSize()
	{
		textureRect = new Rect(
			Screen.width * transform.position.x + guiTexture.pixelInset.x,
			Screen.height * transform.position.y + guiTexture.pixelInset.y,
			guiTexture.pixelInset.width,
			guiTexture.pixelInset.height);

	}
	
	// Update is called once per frame
	void Update () 
	{
		reltrig = false;
		 
		if(Application.platform == RuntimePlatform.Android)
		{
			touchcnt = 0;
			touchedFinger = -1;
			foreach(Touch t in Input.touches)
			{
				if(t.phase == TouchPhase.Began)
				{

				}
				else if(t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)
				{
					if(textureRect.Contains(t.position)/* && t.fingerId == touchedFinger*/)
					{

						touchedFinger = t.fingerId;
						++touchcnt;
						if(!bkTrig && bkTrig != inact)
						{
							trig = true;
							bkTrig = true;
						}
						else
						{
							trig = false;
						}
						
						
						inact = true;
						posx = (t.position.x - textureRect.x) / textureRect.width;
						posy = (t.position.y - textureRect.y) / textureRect.height;

					}
					else
					{

						/*
						bkTrig = false;
						inact = false;
						trig = false;
						posx = posy =0;
						*/
					}
				}	
				else if(t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
				{

					if(textureRect.Contains(t.position) /* && t.fingerId == touchedFinger*/)
					{
						reltrig = true;
					}

					//touchedFinger = -1;

				}
			}
			if(touchcnt == 0)
			{
				bkTrig = false;
				inact = false;
				trig = false;
				posx = posy =0;
			}
		}
		else
		{
			Vector2 pos = Input.mousePosition;
			touchcnt = 0;
			if(Input.GetMouseButton(0))
			{
				if(Input.GetMouseButton(1))
				{
					++touchcnt;
				}
				++touchcnt;

				
				if(textureRect.Contains(pos))
				{		
					if(!bkTrig && bkTrig != inact)
					{
						trig = true;
						bkTrig = true;

					}
					else
					{
						trig = false;
					}

					inact = true;
					posx = (pos.x - textureRect.x) / textureRect.width;
					posy = (pos.y - textureRect.y) / textureRect.height;
				}
				else
				{
					bkTrig = false;
					inact = false;
					trig = false;
					posx = posy =0;
				}	
			}
			else if(Input.GetMouseButtonUp(0))
			{
				if(textureRect.Contains(pos))
				{
					reltrig = true;
					inact = false;
					posx = posy =0;
					touchedFinger = -1;
					trig = false;
					bkTrig = false;
				}
			}
		}

	}

	/*
	void OnMouseDrag()
	{
		if(Input.GetMouseButton(0))
		{
			inact = true;
			posx = (Input.mousePosition.x - (Screen.width * transform.position.x + guiTexture.pixelInset.x)) / guiTexture.pixelInset.width;
			posy = (Input.mousePosition.y - (Screen.height * transform.position.y  + guiTexture.pixelInset.y)) / guiTexture.pixelInset.height;
		}
		else{
			inact = false;
		}
	}
	void OnMouseExit()
	{
		inact = false;
		posx = posy =0;
	}
	*/
}
