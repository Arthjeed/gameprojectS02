using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PlayerProcedural : MonoBehaviour {

	Rigidbody2D rigidbody;
	Vector2 velocity;
	
	void Start () {
		rigidbody = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		velocity = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")).normalized * 10;
	}

	void FixedUpdate() {
		rigidbody.MovePosition (rigidbody.position + velocity * Time.fixedDeltaTime);
	}

	/*public Camera camera;
	public NavMeshAgent agent;

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit))
			{
				agent.SetDestination(hit.point);
			}
		}
	}*/
	/*Rigidbody rigidbody;
	Vector3 velocity;
	
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
	}

	void Update () {
		velocity = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * 10;
	}

	void FixedUpdate() {
		rigidbody.MovePosition (rigidbody.position + velocity * Time.fixedDeltaTime);
	}*/
}