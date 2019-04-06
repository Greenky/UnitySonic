using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public Animator _anim;
	public int levelNum;

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(GameObject.FindWithTag("GameManager").GetComponent<GameManager>().EndLevel(levelNum));
        _anim.enabled = true;
        GetComponent<BoxCollider2D>().enabled = false;
		Camera.main.transform.parent = null;
	}
}
