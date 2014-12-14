﻿using UnityEngine;
using System.Collections;

public class GameEngineLevel02_new : MonoBehaviour {
	
	private PauseMenuGUI gui;
	
	private RaycastHit getObjectScene;
	private bool pause = false;
	
	// --- CHARACTER ---
	private GameObject prefab;
	//private GameObject prefab2;
	//private int pref = 1;
	private GameObject character;
	public GameObject respawn;
	
	// --- LIGHT
	public GameObject ambientLight;
	private CharacterScript cs;
	private ClickToMove_lvl2 cm;
	private Color c;
	
	// --- NPCs ---
	private string[] nameNPC = {"Enemy"};
	private GameObject npc;
	private Movement ms;
	private bool allIsDead = false;
	
	// --- Delays ---
	public float delay = 2;

	// Final de juego
	private bool end_game = false;
	private float end_time = 0.0f;

	// MEMORY CARD 
	private MemoryCard mc;
	private SaveData save;
	private LoadData load;

	// Time Played
	private static float time_play = 0;
	private float time_dead = 0.0f;

	
	// Use this for initialization
	void Awake () {
		//print (PlayerPrefs.GetString("Difficult"));
		// --- LOAD RESOURCES TO CHARACTER ---
		//this.prefab = Resources.Load<GameObject>("Prefabs/MainCharacters/Level02/hombre_lvl2");
		this.prefab = Resources.Load<GameObject>("Prefabs/MainCharacters/Level02/"+PlayerPrefs.GetString("Player")+"_lvl2");
		this.character = Instantiate (prefab, respawn.transform.position, prefab.transform.rotation) as GameObject;
		this.cs = this.character.GetComponent<CharacterScript> ();
		this.cm = this.character.GetComponent<ClickToMove_lvl2> ();

		// Memory Card Save/Load data
		this.mc = GameObject.FindGameObjectWithTag ("MemoryCard").GetComponent<MemoryCard> ();
		this.save = this.mc.saveData();
		this.load = this.mc.loadData();
		time_play = this.load.loadTimePlayed (); 
		print ("Time begin: " + time_play);

		// --- LOAD RESOURCES TO MENU ---
		gui = new PauseMenuGUI();
		gui.initResources ();
		
		
		this.c = this.ambientLight.light.color;

	}
	
	// Update is called once per frame
	void Update () {
		this.PauseScreen ();
		this.StateMachine ();
		time_play += Time.deltaTime;
	}
	
	
	void StateMachine(){
		
		if (Input.GetKeyDown (KeyCode.Escape) && !this.pause) {
			this.pause = true;
			Time.timeScale = 0;
		} else if (Input.GetKeyDown (KeyCode.Escape) && this.pause) {
			this.pause = false;
			Time.timeScale = 1;
		} 
		/*else if (Input.GetKeyDown (KeyCode.C)) {
			Vector3 pos = character.transform.position;
			Quaternion rot = character.transform.rotation;
			Destroy (this.character);
			if(pref == 1) {
				character = Instantiate (prefab2, pos, rot) as GameObject;
				pref = 2;
			} else {
				character = Instantiate (prefab, pos, rot) as GameObject;
				pref = 1;
			}
			cs = this.character.GetComponent<CharacterScript> ();
		}*/
		
		this.isAlive ();
	}
	
	//Comprueba si el personaje sigue vivo
	void isAlive(){
		int num = this.character.GetComponent<CharacterScript> ().getHealth();
		//If the character is dead we show "game over" scene
		if(num <= 0) {
			if(time_dead == 0.0f) time_dead = Time.time;
			if(Time.time-time_dead > 3.0f) {
				Application.LoadLevel(7);
			} else {
				cm.dieAnim ();
			}
		}
	}

	void PauseScreen(){	
		if (this.pause && !this.cs.isCritical ())
			this.ambientLight.light.color = new Color (.2f, .2f, .2f);
		else if (!this.pause && !this.cs.isCritical ())
			this.ambientLight.light.color = new Color (1.0f, 1.0f, 1.0f);
		else if (this.pause && this.cs.isCritical ())
			this.ambientLight.light.color = new Color (.5f, .25f, .5f);
		else if (!this.pause && this.cs.isCritical ())
			this.CautionScreen ();
	}
	
	
	// Efecto critico con luz roja
	void CautionScreen(){	
		if (this.cs.isCritical()) {
			this.c.r = 1.0f;
			if(this.c.g >= 0.5f)
				this.c.g -= 0.02f;
			if(this.c.b >= 0.5f)
				this.c.b -= 0.02f;
			this.ambientLight.light.color = this.c;
		} else {
			this.c.r = 1.0f;
			if(this.c.g <= 1.0f)
				this.c.g += 0.02f;
			if(this.c.b <= 1.0f)
				this.c.b += 0.02f;
			this.ambientLight.light.color = this.c;
		}	
	}
	
	void OnGUI(){	
		if (this.pause) 
			this.pause = this.gui.pauseMenu (this.pause);		
		this.gui.confirmMenu(this.pause);
	}

	public void finish_game() {
		if (!end_game) {
			end_game = true;
			end_time = Time.time;
			print ("Time finish: " + time_play);
			this.save.saveTimePlayed(time_play);
		}
	}

	public static float getTimePlay(){
		return time_play;
	}
}
