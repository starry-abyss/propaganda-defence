using UnityEngine;
using System.Collections;

public class PopupMessageScript : MonoBehaviour {

	public Vector3 pos;

	public void FadesAway()
	{
		Destroy(transform.parent.gameObject);
	}

	// Use this for initialization
	void Start () {
		Update();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);
		//pos.y = 10;
		//viewportPoint += new Vector2(0,10.0f,0);
		RectTransform parentTransform = (RectTransform) GetComponent<RectTransform>().parent;
		parentTransform.anchorMin = viewportPoint;  
		parentTransform.anchorMax = viewportPoint; 
	}
}
