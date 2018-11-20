using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSystem : MonoBehaviour {
    
    List<Explosion> List_Explosion;
    public List<GameObject> debris;
    Explosion[] exps;
    Blast[] blasts;

    // Use this for initialization
    void Start () {

        exps = ScriptableObject.FindObjectsOfType<Explosion>();
        List_Explosion = new List<Explosion>();
        debris = new List<GameObject>();

        foreach (Explosion exp in exps)
        {
            List_Explosion.Add(exp);
        }
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(debris.Count);
        for (; debris.Count > 100;)
        {
            Destroy(debris[0].gameObject);
            debris.RemoveAt(0);
        }

        blasts = ScriptableObject.FindObjectsOfType<Blast>();
        if (List_Explosion.Count > 0)
        {
            Debug.Log(List_Explosion.Count);
            for(int i = 0;i < List_Explosion.Count;i++)
            {
                if (List_Explosion[i].explosed)
                {
                    if (List_Explosion[i].LifeTime > 0)
                    {
                        foreach (Blast bl in blasts)
                        {
                            bl.gameObject.GetComponent<Rigidbody>().AddExplosionForce(200, List_Explosion[i].transform.position, 10);
                        }
                        List_Explosion[i].LifeTime--;
                    }
                    else
                    {
                        Destroy(List_Explosion[i].gameObject);
                        List_Explosion.RemoveAt(i);
                    }
                    
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bom = Instantiate((GameObject)Resources.Load("bom"), transform.position, Quaternion.identity);
            List_Explosion.Add(bom.GetComponent<Explosion>());
        }

	}


}
