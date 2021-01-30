using UnityEngine;
using System.Collections;

public class ObjectPickup : MonoBehaviour
{
	public Transform player;
	public float throwForce = 10;
	bool hasPlayer = false;
	bool beingCarried = false;

	public static bool dontAllowPickups = false;

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
			hasPlayer = true;
	}

	void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
			hasPlayer = false;
	}

	void Update()
	{
		if (!dontAllowPickups)
		{
			if (player == null)
			{
				player = GameObject.FindGameObjectWithTag("MainCamera").transform;
			}
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

}