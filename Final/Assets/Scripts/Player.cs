using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;

public class Player : MonoBehaviour {
	public GameObject player;
	public float MovingSpeed = 2f; 
	private Vector3 AccelerometerDirection;             // Trục cảm ứng nghiên
	public float AccelerometerSensitivity = 0.1f;       // Độ nhạy cảm ứng nghiên
	protected Animator animator;
	
	public Text countText;
	public Text currentScore;
	private int count;
	private int m_highScore=0;
	public Text highScore;

	public GameObject picture; 
	private int countimg;

	bool Pause = false;

	public GameObject restart;
	public GameObject home;
	public GameObject setting;
	public GameObject imggameover;
	public GameObject currentScoreGO;
	public GameObject highScoreGO;


	public GameObject resume;
	public GameObject homePause;
	public GameObject settingPause;
	public GameObject imgPauseGame;

	public float speedBuff = 20f;
	public int countBuff = 200;
	bool Buff = false;
	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();
		count = 0;
		SetCountText();
		countimg = 0;
	}

	void SetCountText ()
	{
		if (Pause == false) 
		{
			countText.text = count.ToString() + " mt";
			currentScore.text = count.ToString ();

			m_highScore = PlayerPrefs.GetInt("highscore");
			if (count > m_highScore)
			{
				m_highScore = count;
				PlayerPrefs.SetInt("highscore", m_highScore);
				PlayerPrefs.Save();
			}

			highScore.text = m_highScore.ToString ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		count = count + 1;
		SetCountText();
		// Detect xem đang chạy trên mobile hay trên các thiết bị khác
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			AccelerometerDirection = Input.acceleration;   
		}
		else
		{
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				AccelerometerDirection.y = AccelerometerSensitivity + 1;
			}
			if (Input.GetKey(KeyCode.RightArrow))
			{
				AccelerometerDirection.y = -AccelerometerSensitivity - 1;
			}
			if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
			{
				AccelerometerDirection.y = 0.0f;
			}
		}
		MovingSpeed += 0.0001f;
		// Di chuyển xe thẳng hướng phía trước
		transform.Translate(new Vector3(0,MovingSpeed * Time.deltaTime, 0));
		// Camera cũng phải chạy theo, giữ 1 khoảng cách nhất định với xe
		Camera.mainCamera.transform.position = new Vector3(0, transform.position.y + 1, transform.position.z - 10);
		Vector3 localpos = transform.localPosition;
		if (AccelerometerDirection.y > AccelerometerSensitivity)
		{
			if(localpos.x>-3)
			{
				transform.Translate(new Vector3(-MovingSpeed * Time.deltaTime, 0, 0));
			}
		}
		else if (AccelerometerDirection.y < -AccelerometerSensitivity)
		{
			if(localpos.x<3)
			{
				transform.Translate(new Vector3(MovingSpeed * Time.deltaTime, 0, 0));
			}
		}
		else
		{
			//transform.Translate(new Vector3(0,MovingSpeed * Time.deltaTime, 0));
		}

		if (countimg == 1)
		{
			picture.GetComponent<Image>().sprite = Resources.Load<Sprite>("ld20");
			MovingSpeed = speedBuff;
			Buff = true;
		}
		if (countimg == 2)
		{
			picture.GetComponent<Image>().sprite = Resources.Load<Sprite>("ld40");
		}
		if (countimg == 3)
		{
			picture.GetComponent<Image>().sprite = Resources.Load<Sprite>("ld60");
		}
		if (countimg == 4)
		{
			picture.GetComponent<Image>().sprite = Resources.Load<Sprite>("ld80");
		}
		if (countimg == 5)
		{
			picture.GetComponent<Image>().sprite = Resources.Load<Sprite>("ld100");

		}

		if (Buff == true) 
		{
			player.GetComponent<BoxCollider2D>().enabled = false;
			animator.SetBool("isBuff",true);
			animator.SetBool("isRunning",false);
			animator.SetBool("isDie",false);
			countBuff -=1;
			countimg = 0;
		}

		if (Buff == false) 
		{
			countBuff = 200;
			animator.SetBool("isBuff",false);
			animator.SetBool("isDie",false);
			animator.SetBool("isRunning",true);
		}

		if (countBuff == 150) 
		{
			MovingSpeed = 15;
		}

		if (countBuff == 100) 
		{
			MovingSpeed = 10;
		}

		if (countBuff == 50) 
		{
			MovingSpeed = 5;
		}

		if (countBuff == 30) 
		{
			MovingSpeed = 2;
		}

		if (countBuff <= 0) 
		{
			player.GetComponent<BoxCollider2D>().enabled = true;
			Buff = false;
		}

		if (Pause == false)
		{
			Time.timeScale = 1;
		}		
		else 
		{
			animator.SetBool("isBuff",false);
			animator.SetBool("isRunning",false);
			animator.SetBool("isDie",true);
			float seconds = 2;
			seconds -= 1 * Time.deltaTime;
			if(seconds <= 0)
			{
				Time.timeScale = 0;
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) 
	{
		//other.gameObject
		Debug.Log ("OnTriggerEnter2D with object has tag = " + other.gameObject.tag);
		if (other.gameObject.tag == "stone") 
		{
			Pause = true;/*
			restart.SetActive(true);
			home.SetActive(true);
			setting.SetActive(true);
			imggameover.SetActive(true);
			currentScoreGO.SetActive(true);
			highScoreGO.SetActive(true);*/
		}

		if (other.gameObject.tag == "feather") 
		{
			if (countimg <5)
			{
				countimg +=1;
			}
			Destroy(other.gameObject);
		}
	}

	public void clickPause() {
		Pause = true;
		resume.SetActive(true);
		homePause.SetActive(true);
		settingPause.SetActive(true);
		imgPauseGame.SetActive(true);
	}

	public void clickResume() {
		Pause = false;
		resume.SetActive(false);
		homePause.SetActive(false);
		settingPause.SetActive(false);
		imgPauseGame.SetActive(false);
	}
}
