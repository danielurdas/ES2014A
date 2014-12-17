﻿using UnityEngine;
using System.Collections;

public class ItemInfo : MonoBehaviour {
	
		
	// ====== TEXT STYLES ======
	public string text_item;
	private string text;
	private GUIStyle text_style;
	private GUIStyle guiStyleBack;


	// ====== CURSOR ======
	private Texture2D[] cursorTexture;
	private CursorMode mode = CursorMode.Auto;
	private Vector2 hotSpot = Vector2.zero;

	// ====== SHADERS ======
	//public Shader shader1;
	//public Shader shader2;


	private float timer = 1;
	private bool isHover = false;
	
	// Use this for initialization
	void Start () {

		// ADD CURSOR
		this.cursorTexture = new Texture2D[9];

		this.cursorTexture[0] = Resources.Load<Texture2D>("Misc/cursor");

		for(int i = 1; i < this.cursorTexture.Length; i++)
			this.cursorTexture[i] = Resources.Load<Texture2D>("Misc/hand_" + (i-1));


		Cursor.SetCursor(cursorTexture[0], hotSpot, mode);


		this.text = "";
		this.text_style = new GUIStyle ();
		this.text_style.normal.textColor = Color.white;
		this.text_style.fontSize = 15;
		this.text_style.alignment = TextAnchor.UpperCenter ; 
		this.text_style.wordWrap = true; 
	}

	void Update(){
		if (this.isHover) {
			timer += 8*Time.deltaTime;
			if(timer > 9f)
				timer = 1f;
			Cursor.SetCursor(cursorTexture[(int)timer], hotSpot, mode);
		}
	}

	void OnMouseEnter () {	
		this.text = this.text_item;
		CursorScript.isHover = true;
		this.isHover = true;

		/*for (int i = 0; i < this.renderer.materials.Length; i++) {
			this.renderer.materials[i].SetColor ("_OutlineColor", new Color(1f, 0f ,0f));
			this.renderer.materials[i].shader = shader1;
		}*/

	}

	void OnMouseExit () { 
		this.text = "";
		CursorScript.isHover = false;
		if(cursorTexture[0] != null)
			Cursor.SetCursor(cursorTexture[0], hotSpot, mode);
		this.timer = 1f;
		this.isHover = false;

		/*for (int i = 0; i < this.renderer.materials.Length; i++) {
			this.renderer.materials[i].SetColor ("_OutlineColor", new Color(0f, 0f ,0f));
			this.renderer.materials[i].shader = shader2;
		}*/
	}

	void OnDestroy() {
		this.isHover = false;
		CursorScript.isHover = false;
		//Cursor.SetCursor(cursorTexture[0], hotSpot, mode);
	}

	void OnGUI() { 
		if (this.text != "") { 
			var x = Event.current.mousePosition.x; 
			var y = Event.current.mousePosition.y; 
			GUI.Label (new Rect (x-150,y+20,300,60), this.text, this.text_style); 
		} 
	}

}