using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseCamera : MonoBehaviour {
    public GameObject target = null;
    GameObject lastobj = null;
    Quaternion rotation;
    Vector3 direction;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (target != null) {
            if (lastobj != target) {
                lastobj = target;
                transform.position = target.transform.position;
                Vector3 ad = new Vector3(0, 3, -5);
                ad = target.transform.Find("Armature/Parent/Pelvis").transform.TransformDirection(ad);
                transform.position += ad;
            }

            Debug.Log("aaaaaaaaaaaaaaaaaaaaa");
            direction = target.transform.Find("Armature/Parent/Pelvis").transform.position - transform.position;
            rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
	}
}
