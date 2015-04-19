using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AIAttackScript : MonoBehaviour {

	//public Transform target;
	public Vector3 home;
	bool stopped = false;
	NavMeshAgent agent;
	
	public int hp = 1;
	GameObject canvas;
	
	public void Hit(int points)
	{
		hp -= points;
		
		if (points > 0)
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
			message.GetComponentInChildren<Text>().text = (-points).ToString();
		}
		
		if (hp <= 0)
			Stop();
	}
	
	public bool IsStopped()
	{
		return stopped;
	}
	
	public void Stop()
	{
		agent.SetDestination(home);
		stopped = true;
		Destroy(gameObject);
	}

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		GameObject building = GameObject.Find("buildingCenter");
		agent.SetDestination(building.GetComponent<Collider>().bounds.ClosestPoint(transform.position));
		
		//GetComponent<NavMeshAgent>().SetDestination(target.position);
		agent.updateRotation = false;
		
		canvas = GameObject.Find("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
		if (stopped && (agent.remainingDistance <= agent.stoppingDistance) && (!agent.hasPath))
		{
			Destroy(gameObject);
		}

		//GetComponent<NavMeshAgent>().velocity = ;
	}
}
