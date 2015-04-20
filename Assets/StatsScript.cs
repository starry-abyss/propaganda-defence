using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StatsScript : MonoBehaviour {
	
	public float health = 500.0f;
	static public float money;
	float healthDropTimer;
	public float fullHealth = 500.0f;
	
	bool gameEnded = false;
	
	public float healthDropPerWorker = 1.0f;
	public float healthDropPerBlonde = 2.0f;
	public float healthDropPerSailor = 3.0f;
	
	GameObject canvas;
	GameObject infoTop, infoBottom;
	GameObject waves;
	GameObject weaponShop;
	
	GameObject youWon, gameOver;
	
	public void EndGame(bool win)
	{
		gameEnded = true;
		
		if (win)
		{
			youWon.SetActive(true);
		}
		else
		{
			gameOver.SetActive(true);
			
			//WaveManagerScript.currentWave = -1;
			StartCoroutine(DeferredReload());
		}
	}
	
	IEnumerator DeferredReload()
	{
		yield return new WaitForSeconds(5.0f);
		Application.LoadLevel(Application.loadedLevel);
	}
	
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
			
			int workerCount = 0;
			int blondeCount = 0;
			int sailorCount = 0;
			for (int i = 0; i < hitColliders.Length; ++i)
			{
				CitizenScript citizen = hitColliders[i].GetComponent<CitizenScript>();
				AIAttackScript citizenAI = hitColliders[i].GetComponent<AIAttackScript>();
				if ((citizen != null) && !citizenAI.IsStopped())
				{
					if (citizen.GetType() == CitizenScript.CitizenTypes.Worker)
						++workerCount;
					else if (citizen.GetType() == CitizenScript.CitizenTypes.Blonde)
						++blondeCount;
					else if (citizen.GetType() == CitizenScript.CitizenTypes.Sailor)
						++sailorCount;
				}
			}
			
			float healthDrop = workerCount * healthDropPerWorker + blondeCount * healthDropPerBlonde
				+ sailorCount * healthDropPerSailor;
			health -= healthDrop;
			
			if (healthDrop > 0.0f)
			{
				//print(23);
				GameObject message = (GameObject) Instantiate(Resources.Load("prefabs/message"));
				Vector3 pos = GetComponent<Collider>().bounds.center;
				pos.y = 0;
				message.GetComponentInChildren<PopupMessageScript>().pos = pos;
				
				//transform.position/* + new Vector3(0,0,10.0f)*/;
				message.GetComponent<RectTransform>().SetParent(canvas.transform);
				message.transform.SetSiblingIndex(0);
				message.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
				message.GetComponentInChildren<Text>().text = (-healthDrop).ToString();
			}
			
			if (health <= 0)
			{
				EndGame(false);
			}
		}
	}

	// Use this for initialization
	void Start () {
		money = 0;
		
		healthDropTimer = Time.time;
		canvas = GameObject.Find("Canvas");
		infoTop = canvas.transform.Find("InfoTop").gameObject;
		infoBottom = canvas.transform.Find("InfoBottom").gameObject;
		
		waves = GameObject.Find("Waves");
		weaponShop = /*canvas.transform*/ GameObject.Find("WeaponShop");
		
		youWon = GameObject.Find("YouWon");
		gameOver = GameObject.Find("GameOver");
		//waveAnnounce = GameObject.Find("WaveAnnounce");
		youWon.SetActive(false);
		gameOver.SetActive(false);
		
		//waveAnnounce.SetActive(false);
		
		//EndGame(false);
		//Update ();
		
		//weaponShop.GetComponent<WeaponShopScript>().Select(AttachWeaponScript.WeaponTypes.DrinkSpot);
		
		/*Text[] shopPrices = weaponShop.GetComponentsInChildren<Text>();
		for (int i = 0; i != shopPrices.Length; ++i)
		{
			shopPrices[i].text = "$" + AttachWeaponScript.weaponPrices[i+1].ToString() + "K";
		}*/
	}
	
	// Update is called once per frame
	void Update () {
		HealthDrop();

		/*Image[] grayed = new Image[](4);
		for ()
			Image[] grayed = weaponShop.Find("Expensive").GetComponents<Image>();
		for (int i = 0; i != grayed.Length; ++i)
		{
			grayed[i].enabled = AttachWeaponScript.weaponPrices[i+1] >= money;
		}*/
		
		/*for (int i = 0; i < weaponShop.transform.childCount; ++i)
		{
			weaponShop.transform.GetChild(i).Find("Expensive").GetComponent<Image>().enabled
				= AttachWeaponScript.weaponPrices[i+1] > money;
		}*/
		
		WaveManagerScript wms = waves.GetComponent<WaveManagerScript>();
		infoTop.GetComponent<Text>().text = string.Format("Mental Health: {0}    Wave {1} of {2}", health, WaveManagerScript.currentWave + 1, wms.WaveCount());
		infoBottom.GetComponent<Text>().text = string.Format("Money: ${0}K", money);
		
		//GameObject.Find("EventSystem").GetComponent<UnityEventQueueSystem>
		if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !EventSystem.current.IsPointerOverGameObject())
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit rch; // = new RaycastHit();
			if (Physics.Raycast(ray, out rch))
			{
				AttachWeaponScript aws = rch.collider.gameObject.GetComponent<AttachWeaponScript>();
				if (aws != null)
				{
					if (Input.GetMouseButtonDown(0))
					{
						//Debug.Log("Mouse Down hit: "+ rch.collider.name);
						
						AttachWeaponScript.WeaponTypes weapon = weaponShop.GetComponent<WeaponShopScript>().SelectedWeapon();
						if ((weapon != AttachWeaponScript.WeaponTypes.None) && (weapon != aws.GetWeaponType()))
						{
							int cost = AttachWeaponScript.weaponPrices[(int) weapon];
							
							GameObject message = (GameObject) Instantiate(Resources.Load("prefabs/message"));
							Vector3 pos = rch.collider.bounds.center;
							pos.y = 0;
							message.GetComponentInChildren<PopupMessageScript>().pos = pos;
							//transform.position/* + new Vector3(0,0,10.0f)*/;
							message.GetComponent<RectTransform>().SetParent(canvas.transform);
							message.transform.SetSiblingIndex(0);
							message.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
							message.GetComponentInChildren<Text>().text = "-$" + (cost).ToString() + "K";
							
							money -= cost;
							aws.SetWeaponType(weapon);
						}
					}
					else
					{
						if (aws.GetWeaponType() == AttachWeaponScript.WeaponTypes.LieGenerator)
							aws.ActivateLies();
					}
				}
			}
		}
	}
		
}
