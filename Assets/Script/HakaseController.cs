using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HakaseController : MonoBehaviour {

    Animator animator;
	
	void Start () {
        animator = gameObject.GetComponent<Animator>();
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            animator.SetTrigger("Push");
        }

	}
}
