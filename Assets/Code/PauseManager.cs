using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour {

	public bool isPaused;

	public void SetPaused(bool pause){
		isPaused = pause;
		if(pause){
			Time.timeScale = 0.0f;
		}else if(pause==false){
			Time.timeScale = 1.0f;
		}
	}

	public void SetSoftPaused(bool pause){
		isPaused = pause;
	}

	public bool GetPaused(){
		return isPaused;
	}
}
