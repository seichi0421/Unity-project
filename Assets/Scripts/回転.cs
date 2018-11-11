using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class 回転 : MonoBehaviour {
    Rigidbody rb;
    public bool 加算 = true;
    public float x,y,z;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        if(rb == null)
        {
            transform.gameObject.AddComponent<Rigidbody>();
        }
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (加算)
        {
            rb.AddTorque(new Vector3(x ,y, z));
            //transform.localEulerAngles += new Vector3(x, y, z);
        }
	}
}
