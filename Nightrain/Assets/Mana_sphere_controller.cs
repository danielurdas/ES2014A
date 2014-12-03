﻿using UnityEngine;
using System.Collections;

public class Mana_sphere_controller : MonoBehaviour {
	private CharacterScript_lvl2 mana;
	//private int max_health;
	
	// Use this for initialization
	void Start () {
		mana = GameObject.FindGameObjectWithTag ("Player").GetComponent <CharacterScript_lvl2>();
		//max_health = (int) health.getMaxHealth();
		Destroy (this.gameObject, 15.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			mana.setManaRestore (10);
			
			Destroy (this.gameObject);
		}
	}
}
