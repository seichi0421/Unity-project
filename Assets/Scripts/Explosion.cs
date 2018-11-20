using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Explosion : MonoBehaviour {
    Rigidbody rb;
    float debX, debY,debZ;
    Blast[] bl;
    Collider[] ovlp;
    public bool explosed = false;
    public int LifeTime = 5;

    List<GameObject> debris;

    // Use this for initialization
    void Start () {

        rb = gameObject.GetComponent<Rigidbody>();
        debris = new List<GameObject>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionStay(Collision collision)
    {
        if (!explosed)
        {
            explosion();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
            if (other.GetComponentInParent<TestPlayerControler2>()&&explosed)
            {

                other.GetComponentInParent<TestPlayerControler2>().death = true;
            }
        
    }

    void Dest()
    {
        Destroy(gameObject);
    }

    void explosion()
    {
        explosed = true;
        for (int i = 0; i < 15; i++)
        {
            GameObject deb = Instantiate((GameObject)Resources.Load("debris"), transform.position + (Random.insideUnitSphere * 0.5f), Quaternion.identity);
            ScriptableObject.FindObjectOfType<ExplosionSystem>().debris.Add(deb);
            deb.GetComponent<Rigidbody>().AddExplosionForce(50, transform.position, 0);
        }
        Destroy(gameObject.GetComponent<MeshRenderer>());
        Invoke("Dest", 1);
    }
}
