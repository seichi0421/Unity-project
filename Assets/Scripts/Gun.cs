using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Gun : MonoBehaviour {

    Rigidbody rb,bullrb;
    float px,py,pz,
          ScaleX, ScaleY, ScaleZ;

    List<GameObject> List_Bullet = new List<GameObject>();

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>();

        px = transform.position.x;
        py = transform.position.y;
        pz = transform.position.z;

        
        
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {

            ScaleX = transform.localScale.x;
            ScaleY = transform.localScale.y;
            ScaleZ = transform.localScale.z;

            GameObject Bullet = (GameObject)Resources.Load("Bullet");
            Vector3 bulletposition = new Vector3(0,0,(ScaleZ/2)+ScaleZ);
            bulletposition = transform.TransformDirection(bulletposition);
            GameObject bull = Instantiate(Bullet,transform.position + bulletposition,Quaternion.identity);
            List_Bullet.Add(bull);

            if(List_Bullet.Count > 10)
            {
                Destroy(List_Bullet[0]);
                List_Bullet.RemoveAt(0);
            }

            bull.transform.localScale = transform.localScale / 2;
            bullrb = bull.gameObject.GetComponent<Rigidbody>();
            Debug.Log(transform.position + bulletposition);
            rb.velocity = new Vector3(0, 0, 0);

            bulletposition = new Vector3(0,0,ScaleZ*2);
            bulletposition = transform.TransformDirection(bulletposition);
            rb.AddExplosionForce(bullrb.mass * 4000, transform.position + bulletposition, 0);

            bulletposition = new Vector3(0, 0,ScaleZ/2);
            bulletposition = transform.TransformDirection(bulletposition);
            bullrb.AddExplosionForce(bullrb.mass*4000,transform.position + bulletposition, 0);
            
        }
		
	}



}
