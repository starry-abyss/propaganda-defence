using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float dx = Input.GetAxis("Horizontal");
		float dy = Input.GetAxis("Vertical");
		
		transform.Translate(new Vector3(dx, dy, 0) * 1.0f);
	}
}
