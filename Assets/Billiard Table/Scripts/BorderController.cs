using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour {
    private GameObject cue;
	// Use this for initialization
	void Start () {
        cue = GameObject.FindGameObjectWithTag("Cue");
        Physics.IgnoreCollision(transform.GetComponent<Collider>(), cue.GetComponent<Transform>().GetComponent<Collider>());
    }
	
}
