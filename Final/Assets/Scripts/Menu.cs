using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void clickHome() {
		Application.LoadLevel ("PlayScene");
	}

	public void clickRestart() {
		Application.LoadLevel ("Angle Fall");
	}

	public void clickSetting() {
		Application.LoadLevel ("Setting");
	}
}
