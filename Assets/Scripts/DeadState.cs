using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : MonoBehaviour
{
    GameObject Pelvis, Leg_L, Knee_L, Leg_R, Knee_R, Spine1, Spine2, Head, LeftArm, LeftElbow, RightArm, RightElbow;
    GameObject[] part;
    Rigidbody[] RB;
    public Vector3 vel;
    public Quaternion[]rotation;
    // Use this for initialization
    void Start()
    {
        Pelvis = this.transform.gameObject;
        Leg_L = this.transform.Find("Leg.L").gameObject;
        Knee_L = this.transform.Find("Leg.L/Knee.L").gameObject;
        Leg_R = this.transform.Find("Leg.R").gameObject;
        Knee_R = this.transform.Find("Leg.R/Knee.R").gameObject;

        Spine1 = this.transform.Find("Spine1").gameObject;
        Spine2 = this.transform.Find("Spine1/Spine2").gameObject;

        Head = this.transform.Find("Spine1/Spine2/Head").gameObject;

        LeftArm = this.transform.Find("Spine1/Spine2/LeftArm").gameObject;
        LeftElbow = this.transform.Find("Spine1/Spine2/LeftArm/LeftElbow").gameObject;
        RightArm = this.transform.Find("Spine1/Spine2/RightArm").gameObject;
        RightElbow = this.transform.Find("Spine1/Spine2/RightArm/RightElbow").gameObject;

        part = new GameObject[] { Pelvis, Leg_L, Knee_L, Leg_R, Knee_R, Spine1, Spine2, Head, LeftArm, LeftElbow, RightArm, RightElbow };

        RB = new Rigidbody[] {Pelvis.GetComponent<Rigidbody>(),Leg_L.GetComponent<Rigidbody>(), Knee_L.GetComponent<Rigidbody>(), Leg_R.GetComponent<Rigidbody>(), Knee_R.GetComponent<Rigidbody>(),
                                   Spine1.GetComponent<Rigidbody>(),Spine2.GetComponent<Rigidbody>(),LeftArm.GetComponent<Rigidbody>(),LeftElbow.GetComponent<Rigidbody>(),RightArm.GetComponent<Rigidbody>(),
                                   RightElbow.GetComponent<Rigidbody>() };
        foreach(Rigidbody rb in RB)
        {
            rb.velocity = vel;
        }
        int n = 0;
        foreach(Quaternion rot in rotation)
        {
            part[n].transform.rotation = rot;
            n++;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
