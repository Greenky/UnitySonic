using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public Animator _anim;

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(GameObject.FindWithTag("GameManager").GetComponent<GameManager>().EndLevel());
        _anim.enabled = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
