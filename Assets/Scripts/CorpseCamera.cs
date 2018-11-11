using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseCamera : MonoBehaviour {
    public GameObject target;
    GameObject lastobj = null;
    Quaternion rotation;
    Vector3 direction;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() { 
        if(lastobj != target){
            lastobj = target;
            transform.position = target.transform.position;
            Vector3 ad = new Vector3(0, 3, -5);
            ad = target.transform.TransformDirection(ad);
            transform.position += ad;
        }
        

        direction = target.transform.position - transform.position;
        rotation = Quaternion.LookRotation(direction);
        transform.rotation = rotation;
	}
}
