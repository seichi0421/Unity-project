using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class 往復運動 : MonoBehaviour {
    float speed;
    public bool 加算 = true;
    Vector3 V;
    public float x,y,z,ω = 1;
    Rigidbody rb;
    // Use this for initialization
    void Start () {
        
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (加算)
        {
            speed = 10 * Mathf.Sin(ω * Mathf.PI * Time.time);
            V = new Vector3(speed * x, speed * y, speed * z);
            transform.TransformDirection(V);
            rb.velocity = V;
        }
	}
}
