using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.tag == "Player")
		{
			collision.collider.gameObject.GetComponent<Sonic>().getHit(collision.contacts[0].normal);
		}
	}
	
	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.collider.tag == "Player")
		{
			collision.collider.gameObject.GetComponent<Sonic>().getHit(collision.contacts[0].normal);
		}
	}
}
