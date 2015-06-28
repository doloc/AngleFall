using UnityEngine;
using System.Collections;

public class BackGround : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.mainCamera.transform.position.y >= (this.transform.position.y + 30)) {
			this.transform.position += new Vector3(0, 69, 0);
		}
	}
}
