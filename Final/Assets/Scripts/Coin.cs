using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Camera.mainCamera.transform.position.y >= (this.transform.position.y + 10)) {
			Vector3 np = this.transform.position;
			np.x = 0;
			np += new Vector3(Random.Range(-5f, 5f), Random.Range(16, 24), 0);
			this.transform.position = np;
		}
	}
}
