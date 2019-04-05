using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().AddRing();
            Destroy(gameObject);
        }
    }
}
