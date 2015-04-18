﻿using UnityEngine;
using System.Collections;

public class WaveScript : MonoBehaviour {

	public string waveName;
	public int workerCount;
	public int blondeCount;
	float spawnTimer;
	
	int workerLeft;
	int blondeLeft;
	//int ;
	
	public void StartWave()
	{
		print(waveName);
		StatsScript building = GameObject.Find("buildingCenter").GetComponent<StatsScript>();
		building.Repair();
	
		workerLeft = workerCount;
		blondeLeft = blondeCount;
	}

	// Use this for initialization
	void Start () {
		spawnTimer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimer + 1.0f < Time.time)
		{
			spawnTimer = Time.time;
			
			CitizenScript.CitizenTypes type = CitizenScript.CitizenTypes.None;
			if (workerLeft > 0)
			{
				type = CitizenScript.CitizenTypes.Worker;
				--workerLeft;
			}
			else if (blondeLeft > 0)
			{
				type = CitizenScript.CitizenTypes.Blonde;
				--blondeLeft;
			}
			
			//print(workerLeft);
			
			if (type != CitizenScript.CitizenTypes.None)
			{
				Random.seed = (int) System.DateTime.Now.Ticks;
				Vector3 pos = new Vector3(0, 1, 0);
				// = new Vector3(Mathf.Round(Mathf.Cos(a)), 1, Mathf.Round(Mathf.Sin(a)));
				Collider ground = GameObject.Find("Ground").GetComponent<Collider>();
				
				//print("min " + ground.bounds.min.ToString());
				//print("max " + ground.bounds.max.ToString());
				
				if (Random.Range(0, 2) == 0)
				{
					pos.z = Random.Range(ground.bounds.min.z, ground.bounds.max.z);
				
					if (Random.Range(0, 2) == 0)
					{
						pos.x = ground.bounds.min.x;
					}
					else
					{
						pos.x = ground.bounds.max.x;
					}
				}
				else
				{
					pos.x = Random.Range(ground.bounds.min.x, ground.bounds.max.x);
					
					if (Random.Range(0, 2) == 0)
					{
						pos.z = ground.bounds.min.z;
					}
					else
					{
						pos.z = ground.bounds.max.z;
					}
				}
				
				print(pos);
				
			
				GameObject citizen = (GameObject) Instantiate(Resources.Load("prefabs/citizen"), pos, Quaternion.Euler(new Vector3(90, 0, 0)));
				//citizen.GetComponent<Transform>().position = pos;
				citizen.GetComponent<CitizenScript>().SetType(type);
				citizen.GetComponent<AIAttackScript>().home = pos;
				/*message.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").transform);
				message.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
				message.GetComponentInChildren<Text>().text = (-healthDrop).ToString();*/
			}
		}
	}
}