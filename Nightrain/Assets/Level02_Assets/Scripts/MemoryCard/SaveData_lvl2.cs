﻿using UnityEngine;
using System.Collections;

public class SaveData_lvl2 : MonoBehaviour {

	public void saveCutScene(int cutscene){
		PlayerPrefs.SetInt("Cutscene", cutscene);
	}
	
	public void saveFrames(int frames){
		PlayerPrefs.SetInt("Frames", frames);
	}
	
	public void savePath(string path){
		PlayerPrefs.SetString("Path", path);
	}
	
	public void saveLevel(int level){
		PlayerPrefs.SetInt("Level", level);
	}
	
	public void savePlayer(string player){
		PlayerPrefs.SetString ("Player", player);
	}

	public void saveLoading(string loading){
		PlayerPrefs.SetString ("Loading", loading);
	}
	
	public void saveDifficult(string difficult){
		PlayerPrefs.SetString("Difficult", difficult);
	}

	public void WarriorAttributes(){
		PlayerPrefs.SetInt("LVL", 1);
		PlayerPrefs.SetInt("VIT", 150);
		PlayerPrefs.SetInt("PM", 40);
		PlayerPrefs.SetInt("STR", 10);
		PlayerPrefs.SetInt("DEF", 8);
		PlayerPrefs.SetInt("SPD", 5);
		PlayerPrefs.SetInt("EXP", 0);
	}

	public void SageAttributes(){
		PlayerPrefs.SetInt("LVL", 1);
		PlayerPrefs.SetInt("VIT", 75);
		PlayerPrefs.SetInt("PM", 125);
		PlayerPrefs.SetInt("STR", 6);
		PlayerPrefs.SetInt("DEF", 5);
		PlayerPrefs.SetInt("SPD", 7);
		PlayerPrefs.SetInt("EXP", 0);
	}

	public void ThiefAttributes(){
		PlayerPrefs.SetInt("LVL", 1);
		PlayerPrefs.SetInt("VIT", 100);
		PlayerPrefs.SetInt("PM", 75);
		PlayerPrefs.SetInt("STR", 7);
		PlayerPrefs.SetInt("DEF", 6);
		PlayerPrefs.SetInt("SPD", 9);
		PlayerPrefs.SetInt("EXP", 0);
	}

	public void saveStatus(string attribute, int value){

		if(attribute == "LVL" && value < 100){
			PlayerPrefs.SetInt (attribute, value);
			return;
		}

		if(attribute == "EXP"){
			PlayerPrefs.SetInt (attribute, value);
			return;
		}

		if(value <= 510 && (attribute == "VIT" || attribute == "PM")){
			PlayerPrefs.SetInt (attribute, value);
			return;
		}else if(value > 500 && (attribute == "VIT" || attribute == "PM")){
			print ("Attribute: " + attribute);
			PlayerPrefs.SetInt (attribute, 510);
			return;
		}

		if(value <= 255  && (attribute != "VIT" || attribute != "PM"))
			PlayerPrefs.SetInt (attribute, value);
		else if(value > 255  && (attribute != "VIT" || attribute != "PM"))
			PlayerPrefs.SetInt (attribute, 255);
	}

}
