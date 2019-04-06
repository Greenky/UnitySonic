using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour
{
	[SerializeField] private Rigidbody2D _rb;
	private void Awake()
	{
		_rb.gravityScale = 0;
		Invoke("ColliderOn", 1);
	}

	private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().AddRing();
            Destroy(gameObject);
        }
    }

	private void ColliderOn()
	{
		GetComponent<CircleCollider2D>().enabled = true;
	}

	public void Explode(Vector2 where)
	{
		StartCoroutine(Blinking());
		where.x = Random.Range(-1f, 1f);
		where.y = 1;
		_rb.gravityScale = 1;
		_rb.AddForce(where * 8, ForceMode2D.Impulse);
		Invoke("OffGarvity", Random.Range(0.5f, 0.8f));
	}

	private void OffGarvity()
	{
		_rb.velocity = Vector2.zero;
		_rb.gravityScale = 0;
	}

	private IEnumerator Blinking()
	{
		for (int i = 0; i < 20; i++)
		{
			GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
			yield return new WaitForSeconds(0.25f);
		}
		Destroy(gameObject);
	}
}
