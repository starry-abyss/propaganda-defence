using UnityEngine;
using System.Collections;

public class AIAttackScript : MonoBehaviour {

	//public Transform target;
	public Vector3 home;
	bool stopped = false;
	NavMeshAgent agent;
	
	void Stop()
	{
		stopped = true;
		agent.SetDestination(home);
	}

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		GameObject building = GameObject.Find("buildingCenter");
		agent.SetDestination(building.GetComponent<Collider>().bounds.ClosestPoint(transform.position));
		
		//GetComponent<NavMeshAgent>().SetDestination(target.position);
		agent.updateRotation = false;
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
