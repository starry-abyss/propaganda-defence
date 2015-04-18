using UnityEngine;
using System.Collections;

public class AIAttackScript : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
		GetComponent<NavMeshAgent>().SetDestination(target.position);
	}
	
	// Update is called once per frame
	void Update () {
		//GetComponent<NavMeshAgent>().velocity = ;
	}
}
