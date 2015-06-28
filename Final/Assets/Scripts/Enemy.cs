using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float speed = 2f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.mainCamera.transform.position.y >= (this.transform.position.y + 8)) {
			Vector3 np = this.transform.position;
			np.x = 0;
			np += new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(16, 24), 0);
			this.transform.position = np;
		}
	}
}
