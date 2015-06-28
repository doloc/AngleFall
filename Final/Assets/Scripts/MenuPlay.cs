using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;

public class MenuPlay : MonoBehaviour {
	public Text highScore;
	private int m_highScore=0;

	// Use this for initialization
	void Start () {
		m_highScore = PlayerPrefs.GetInt("highscore");
		highScore.text = m_highScore.ToString () + " mt";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void clickPlay() {
		Application.LoadLevel ("Angle Fall");
	}
	
	public void clickOption() {
		Application.LoadLevel ("Option");
	}
}
