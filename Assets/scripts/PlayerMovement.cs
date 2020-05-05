using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour {

	// This is a reference to the Rigidbody component called "rb"
	public Rigidbody rb;

	public MeshRenderer PLS; //nooo!s
	public float forwardForce = 2000f; // Variable that determines the forward force
	public float sidewaysForce = 500f; // Variable that determines the sideways force

	// We marked this as "Fixed"Update because we
	// are using it to mess with physics.
	void FixedUpdate () {
		// Add a forward force

		if (Input.GetKey ("d")) // If the player is pressing the "d" key
		{
			// Add a force to the right
			rb.velocity = new Vector3 (sidewaysForce, 0, 0);

		}

		if (Input.GetKey ("a")) // If the player is pressing the "a" key
		{
			// Add a force to the left
			rb.velocity = new Vector3 (-sidewaysForce, 0, 0);
		}
		if (Input.GetKey ("w")) // If the player is pressing the "a" key
		{
			rb.velocity = new Vector3 (0, 0, forwardForce);

		}
		if (Input.GetKey ("s")) // If the player is pressing the "a" key
		{
			rb.velocity = new Vector3 (0, 0, -forwardForce);
		}
		//if (Input.GetKey ("e")) // If the player is pressing the "a" key
		//{
		//	PLS.enabled = true;
		//} else {
		//	PLS.enabled = false;
		//}

	}
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, 100))
			Debug.DrawLine (ray.origin, hit.point);
	}

}