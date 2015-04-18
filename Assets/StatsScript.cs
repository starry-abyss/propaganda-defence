using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsScript : MonoBehaviour {

	public float health = 5000.0f;
	public float money = 100.0f;
	float healthDropTimer;
	public float fullHealth = 5000.0f;
	public float healthDropPerCitizen = 10.0f;
	
	public void Repair()
	{
		health = fullHealth;
	}

	void HealthDrop()
	{
		if (healthDropTimer + 1.0f < Time.time)
		{
			healthDropTimer = Time.time;

			Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5.0f);
			
			int citizenCount = 0;
			for (int i = 0; i < hitColliders.Length; ++i)
			{
				if (hitColliders[i].GetComponent<CitizenScript>() != null) ++citizenCount;
			}
			
			float healthDrop = citizenCount * healthDropPerCitizen;
			health -= healthDrop;
			
			if (healthDrop > 0.0f)
			{
				//print(23);
				GameObject message = (GameObject) Instantiate(Resources.Load("prefabs/message"));
				message.GetComponentInChildren<PopupMessageScript>().pos = transform.position/* + new Vector3(0,0,10.0f)*/;
				message.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").transform);
				message.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
				message.GetComponentInChildren<Text>().text = (-healthDrop).ToString();
			}
		}
	}

	// Use this for initialization
	void Start () {
		healthDropTimer = Time.time;
		
		GameObject.Find("Waves").GetComponent<WaveScript>().StartWave();
	}
	
	// Update is called once per frame
	void Update () {
		HealthDrop();
	}
		
}
