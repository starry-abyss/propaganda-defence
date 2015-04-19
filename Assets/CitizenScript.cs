using UnityEngine;
using System.Collections;

public class CitizenScript : MonoBehaviour {

	public enum CitizenTypes { None, Worker, Blonde, Sailor };
	
	public CitizenTypes GetType()
	{
		return m_type;
	}
	
	public void SetType(CitizenTypes type)
	{
		if (type == CitizenTypes.Worker)
		{
			GetComponent<AIAttackScript>().hp = 3;
			GetComponent<MeshRenderer>().material = (Material) Instantiate(Resources.Load("materials/citizen"));
		}
		else if (type == CitizenTypes.Blonde)
		{
			GetComponent<AIAttackScript>().hp = 10;
			GetComponent<MeshRenderer>().material = (Material) Instantiate(Resources.Load("materials/citizen2"));
		}
		else if (type == CitizenTypes.Sailor)
		{
			GetComponent<AIAttackScript>().hp = 20;
			GetComponent<MeshRenderer>().material = (Material) Instantiate(Resources.Load("materials/citizen3"));
		}
		
		m_type = type;
	}
	
	CitizenTypes m_type;

	// Use this for initialization
	void Start () {
		//SetType(CitizenTypes.Blonde);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
