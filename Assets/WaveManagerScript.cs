using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveManagerScript : MonoBehaviour {

	static public int currentWave;
	GameObject waveAnnounce;
	
	/*public string CurrentWaveName()
	{
	
	}*/
	
	bool waveStarted = false;
	
	WaveScript[] waveScripts;
	
	public int WaveCount()
	{
		return GetComponents<WaveScript>().Length;
	}

	IEnumerator DeferredStart() 
	{
		yield return new WaitForSeconds(3.0f);
		
		//StatsScript.money += 100;
		StatsScript.money += (currentWave + 1) * 200;
		waveAnnounce.SetActive(false);
		waveScripts[currentWave].StartWave();
		waveStarted = true;
	}

	// Use this for initialization
	void Start () {
		waveScripts = GetComponents<WaveScript>();
		waveAnnounce = GameObject.Find("WaveAnnounce");
		currentWave = -1;
		Announce();
	}
	
	public void Announce()
	{
		++currentWave;
		
		//StatsScript.money += (currentWave + 1) * 100;
		
		waveAnnounce.transform.Find("WaveNumber").GetComponent<Text>().text = "Wave " + (currentWave + 1).ToString();
		waveAnnounce.transform.Find("WaveName").GetComponent<Text>().text = waveScripts[currentWave].waveName;
		
		waveAnnounce.SetActive(true);
		StartCoroutine(DeferredStart());
	}
	
	// Update is called once per frame
	void Update () {
		if (waveStarted && waveScripts[currentWave].WaveComplete())
		{
			if (currentWave + 1 < waveScripts.Length)
			{
				waveStarted = false;
				Announce();
			}
			else
				GameObject.Find("buildingCenter").GetComponent<StatsScript>().EndGame(true);
		}
	}
}
