using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	Collider ground;

	// Use this for initialization
	void Start () {
		ground = GameObject.Find("Ground").GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float dx = Input.GetAxis("Horizontal");
		float dy = Input.GetAxis("Vertical");

		transform.Translate(new Vector3(dx, dy, 0) * 1.0f);
		
		Vector3 pos = transform.position;
		pos.x = Mathf.Clamp(pos.x, ground.bounds.min.x, ground.bounds.max.x);
		//pos.y = Mathf.Clamp(pos.y, ground.bounds.min.y, ground.bounds.max.y);
		pos.y = 30;
		pos.z = Mathf.Clamp(pos.z, ground.bounds.min.z, ground.bounds.max.z);
		transform.position = pos;
	}
}
