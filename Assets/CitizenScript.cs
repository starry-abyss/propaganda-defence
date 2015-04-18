using UnityEngine;
using System.Collections;

public class CitizenScript : MonoBehaviour {

	public enum CitizenTypes { None, Worker, Blonde };
	
	public CitizenTypes GetType()
	{
		return m_type;
	}
	
	public void SetType(CitizenTypes type)
	{
		if (type == CitizenTypes.Worker)
		{
			GetComponent<MeshRenderer>().material = (Material) Instantiate(Resources.Load("materials/citizen"));
		}
		else if (type == CitizenTypes.Blonde)
		{
			GetComponent<MeshRenderer>().material = (Material) Instantiate(Resources.Load("materials/citizen2"));
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
