using UnityEngine;
using System.Collections;

public class ObjectPickup : MonoBehaviour
{
	public Transform player;
	public float throwForce = 10;
	bool hasPlayer = false;
	bool beingCarried = false;

	void OnTriggerEnter(Collider other)
	{
		hasPlayer = true;
	}

	void OnTriggerExit(Collider other)
	{
		hasPlayer = false;
	}

	void Update()
	{
		if (beingCarried)
		{
			if (Input.GetMouseButtonDown(0))
			{
				GetComponent<Rigidbody>().isKinematic = false;
				transform.parent = null;
				beingCarried = false;
				GetComponent<Rigidbody>().AddForce(player.forward * throwForce, ForceMode.Impulse);
			}
		}
		else
		{
			if (Input.GetMouseButtonDown(0) && hasPlayer)
			{
				GetComponent<Rigidbody>().isKinematic = true;
				transform.parent = player;
				beingCarried = true;
			}
		}
	}
}