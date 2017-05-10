using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallMovement : MonoBehaviour {

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

	void Update () {
        if (rb.velocity.magnitude < 0.3)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

}
