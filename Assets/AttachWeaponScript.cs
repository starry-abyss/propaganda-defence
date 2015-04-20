using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class AttachWeaponScript : MonoBehaviour {

	AIAttackScript target;

	public enum WeaponTypes { None = 0, DrinkSpot, ShoppingCenter, LieGenerator, Brothel };
	static public int[] weaponPrices = new int[]{ 0, 50, 100, 200, 200 };
	
	GameObject icon;
	float attackTimer;
	float liesTimer;
	ParticleSystem targetPointer;
	LineRenderer targetPointerLine;
	GameObject canvas;
	
	bool lieHint;
	
	public void ResetCooldown()
	{
		liesTimer = -100;
	}
	
	public WeaponTypes GetWeaponType()
	{
		return m_type;
	}
	
	public void SetWeaponType(WeaponTypes type)
	{
		MeshRenderer iconRenderer = icon.GetComponent<MeshRenderer>();
		iconRenderer.enabled = true;
		if (type == WeaponTypes.LieGenerator)
		{
			iconRenderer.material = (Material) Instantiate(Resources.Load("materials/liegeneratorhint"));
			lieHint = true;
		}
		else if (type == WeaponTypes.DrinkSpot)
		{
			iconRenderer.material = (Material) Instantiate(Resources.Load("materials/drinks"));
		}
		else if (type == WeaponTypes.ShoppingCenter)
		{
			iconRenderer.material = (Material) Instantiate(Resources.Load("materials/shopping"));
		}
		else if (type == WeaponTypes.Brothel)
		{
			iconRenderer.material = (Material) Instantiate(Resources.Load("materials/brothel"));
		}
		else
		{
			iconRenderer.enabled = false;
		}
		
		m_type = type;
	}
	
	WeaponTypes m_type;

	// Use this for initialization
	void Start () {
		canvas = GameObject.Find("Canvas");
		icon = transform.Find("WeaponIcon").gameObject;
		SetWeaponType(WeaponTypes.None);
		//SetWeaponType(WeaponTypes.DrinkSpot);
		//SetWeaponType(WeaponTypes.ShoppingCenter);
		target = null;
		
		liesTimer = -100;
		attackTimer = -100;
		targetPointer = transform.Find("TargetPointer").GetComponent<ParticleSystem>();
		targetPointer.Stop();
		
		targetPointerLine = transform.Find("TargetPointer").GetComponent<LineRenderer>();
	}
	
	public void ActivateLies()
	{
		if (liesTimer + 10.0f < Time.time)
		{
			liesTimer = Time.time;
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5.0f);
	
			for (int i = 0; i < hitColliders.Length; ++i)
			{
				ProcessCitizen(hitColliders[i]);
			}
		}
	}
	
	bool ProcessCitizen(Collider citizenCollider)
	{
		CitizenScript citizen = citizenCollider.GetComponent<CitizenScript>();
		AIAttackScript citizenAI = citizenCollider.GetComponent<AIAttackScript>();
		if ((citizen != null) && !citizenAI.IsStopped())
		{
			if ((target == null) || (citizenAI == target))
			{
				bool hit = false;
				int moneyEarned = 0;
				if (m_type == WeaponTypes.LieGenerator)
				{
					hit = true;
				}
				else if (m_type == WeaponTypes.DrinkSpot)
				{
					targetPointerLine.SetColors(new Color(0.811f, 0.713f, 0.368f), new Color(0.811f, 0.713f, 0.368f));
					if (citizen.GetType() == CitizenScript.CitizenTypes.Worker)
					{
						hit = true;
						moneyEarned = 10;
					}
					else if (citizen.GetType() == CitizenScript.CitizenTypes.Sailor)
					{
						hit = true;
						moneyEarned = 40;
					}
				}
				else if (m_type == WeaponTypes.ShoppingCenter)
				{
					targetPointerLine.SetColors(new Color(0.972f, 0.388f, 0.388f), new Color(0.972f, 0.388f, 0.388f));
					if (citizen.GetType() == CitizenScript.CitizenTypes.Blonde)
					{
						hit = true;
						moneyEarned = 20;
					}
				}
				else if (m_type == WeaponTypes.Brothel)
				{
					targetPointerLine.SetColors(new Color(0.145f, 0.054f, 0.533f), new Color(0.145f, 0.054f, 0.533f));
					if (citizen.GetType() == CitizenScript.CitizenTypes.Sailor)
					{
						hit = true;
						moneyEarned = 40;
					}
					else if (citizen.GetType() == CitizenScript.CitizenTypes.Worker)
					{
						hit = true;
						moneyEarned = 10;
					}
				}
				
				/*if (target == null)
							{*/
				//targetPointer.Play();
				if (hit && (target == null))
				{
					target = citizenAI;
				}
				
				if ((citizenAI != target) && (m_type != WeaponTypes.LieGenerator))
					return false;
				//}
				
				if (hit)
				{
					targetPointerLine.enabled = true;
					attackTimer = Time.time;
					
					if (m_type == WeaponTypes.LieGenerator)
					{
						//citizenAI.Stop();
						citizenAI.Hit(10);
					}
					else if (m_type == WeaponTypes.DrinkSpot)
					{
						citizenAI.Hit(1);
						citizenAI.home = transform.position;
					}
					else if (m_type == WeaponTypes.ShoppingCenter)
					{
						citizenAI.Hit(5);
						citizenAI.home = transform.position;
					}
					else if (m_type == WeaponTypes.Brothel)
					{
						citizenAI.Hit(7);
						citizenAI.home = transform.position;
					}
				}
				
				if (citizenAI.IsStopped())
				{
					target = null;
					//targetPointer.Stop();
					targetPointerLine.enabled = false;
					
					
					StatsScript.money += moneyEarned;
					
					//print(23);
					GameObject message = (GameObject) Instantiate(Resources.Load("prefabs/message"));
					Vector3 pos = GetComponent<Collider>().bounds.center;
					pos.y = 0;
					message.GetComponentInChildren<PopupMessageScript>().pos = pos;
					
					//transform.position/* + new Vector3(0,0,10.0f)*/;
					message.GetComponent<RectTransform>().SetParent(canvas.transform);
					message.transform.SetSiblingIndex(0);
					message.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
					message.GetComponentInChildren<Text>().text = "$" + moneyEarned.ToString() + "K";
					message.GetComponentInChildren<Text>().color = new Color(0.0f, 0.7f, 0.0f);
				}
				
				return true;
			}
		}
		
		return false;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_type == WeaponTypes.LieGenerator)
		{	
			MeshRenderer iconRenderer = icon.GetComponent<MeshRenderer>();
			if (liesTimer + 10.0f < Time.time)
			{
				if (!lieHint)
				{
					iconRenderer.material = (Material) Instantiate(Resources.Load("materials/liegeneratorhint"));
					lieHint = true;
				}
			}
			else if (lieHint)
			{
				iconRenderer.material = (Material) Instantiate(Resources.Load("materials/liegenerator"));
				lieHint = false;
			}
				//print(iconRenderer.material.name);
				
				//iconRenderer.material = (Material) Instantiate(Resources.Load("materials/liegenerator"));
				//;
		}
	
		if (attackTimer + 1.0f < Time.time)
		{
			//float hitTime = Time.time;
		
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5.0f);
			
			if (target != null)
			{
				int pos = Array.IndexOf(hitColliders, target.GetComponent<Collider>());
				if (pos == -1)
				{
					target = null;
					//targetPointer.Stop();
					targetPointerLine.enabled = false;
				}
			}
			
			for (int i = 0; i < hitColliders.Length; ++i)
			{
				if (m_type != WeaponTypes.LieGenerator)
				{
					if (ProcessCitizen(hitColliders[i]))
						break;
				}
			}
		}
		
		if (target != null)
		{
			/*Vector3 dp = transform.position - target.transform.position;
		float distance = Vector3.Distance(target.transform.position, transform.position);
		//Mathf.Sqrt(dx * dx + dy * dy);
		float angle = Mathf.Atan2(-dp.y, -dp.x) * Mathf.Rad2Deg;
		
		Quaternion rotation = new Quaternion();
		//targetPointer.transform.rotation;
		//rotation.SetLookRotation(target.transform.position);
		rotation.eulerAngles = new Vector3(0, angle, 0);
		//rotation.eulerAngles = new Vector3(0, angle, 0);
		targetPointer.transform.localRotation = rotation;*/
			
			//targetPointerLine.transform.localRotation = rotation;
			//targetPointerLine.SetPosition(1, new Vector3(0, 0, distance));
			
			
			//targetPointer.startLifetime = distance / targetPointer.startSpeed;
			//targetPointer.Simulate(targetPointer.startLifetime);
			
			targetPointerLine.SetPosition(0, transform.position);
			targetPointerLine.SetPosition(1, target.transform.position);
			
			//targetPointerLine.enabled = true;
			//targetPointer.enabled = true;
		}
		//else
		//{
			//targetPointerLine.enabled = false;
		//}
	}
}
