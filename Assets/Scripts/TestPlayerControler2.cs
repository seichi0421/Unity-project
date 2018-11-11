﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]

public class TestPlayerControler2 : MonoBehaviour {

    
    GameObject Pelvis,Leg_L,Knee_L,Leg_R,Knee_R,Spine1,Spine2,Head,LeftArm,LeftElbow,RightArm,RightElbow;
    GameObject[] Children;
    Rigidbody rb;
    Rigidbody[] childrenrb;
    float[] childrenmass;
    Quaternion Head_rotation;//Headの計算用の四元数
    Quaternion Pelvis_rotation;//Pelvisの計算用の四元数
    Quaternion headY;         //HeadのY軸の回転角
    Quaternion pelvisY;       //PelvisのY軸の回転角

    Quaternion[] corpserot;

    bool a = false;

    List<GameObject> List_Corpse = new List<GameObject>();

    Vector3 rotationY,MousePosition,CameraAngle;

    float RotationLimit = 60;
	void Start () {
        //体の各パーツのboneの取得
        Pelvis = this.transform.Find("Armature/Parent/Pelvis").gameObject;
            Leg_L = this.transform.Find("Armature/Parent/Pelvis/Leg.L").gameObject;
                Knee_L = this.transform.Find("Armature/Parent/Pelvis/Leg.L/Knee.L").gameObject;
            Leg_R = this.transform.Find("Armature/Parent/Pelvis/Leg.R").gameObject;
                Knee_R = this.transform.Find("Armature/Parent/Pelvis/Leg.R/Knee.R").gameObject;

            Spine1 = this.transform.Find("Armature/Parent/Pelvis/Spine1").gameObject;
                Spine2 = this.transform.Find("Armature/Parent/Pelvis/Spine1/Spine2").gameObject;

                    Head = this.transform.Find("Armature/Parent/Pelvis/Spine1/Spine2/Head").gameObject;

                    LeftArm = this.transform.Find("Armature/Parent/Pelvis/Spine1/Spine2/LeftArm").gameObject;
                        LeftElbow = this.transform.Find("Armature/Parent/Pelvis/Spine1/Spine2/LeftArm/LeftElbow").gameObject;
                    RightArm = this.transform.Find("Armature/Parent/Pelvis/Spine1/Spine2/RightArm").gameObject;
                        RightElbow = this.transform.Find("Armature/Parent/Pelvis/Spine1/Spine2/RightArm/RightElbow").gameObject;

        rb = GetComponent<Rigidbody>();

        //初期化
        Pelvis.transform.localEulerAngles = new Vector3(0, 0, 0);
        Head_rotation = Head.transform.rotation;
        Pelvis_rotation = Pelvis.transform.rotation;

        //各パーツのGameObject,rigidbodyを取得
        Children = new GameObject[] { Pelvis, Leg_L, Knee_L, Leg_R, Knee_R, Spine1, Spine2, LeftArm, LeftElbow, RightArm, RightElbow };

        


        /*childrenrb = new Rigidbody[] {Pelvis.GetComponent<Rigidbody>(),Leg_L.GetComponent<Rigidbody>(), Knee_L.GetComponent<Rigidbody>(), Leg_R.GetComponent<Rigidbody>(), Knee_R.GetComponent<Rigidbody>(),
                                   Spine1.GetComponent<Rigidbody>(),Spine2.GetComponent<Rigidbody>(),LeftArm.GetComponent<Rigidbody>(),LeftElbow.GetComponent<Rigidbody>(),RightArm.GetComponent<Rigidbody>(),
                                   RightElbow.GetComponent<Rigidbody>() };*/

    }
	
	// Update is called once per frame
	void Update () {
        


        //移動
        Vector3 move;
        move = new Vector3(Input.GetAxis("Horizontal")*100, 0, Input.GetAxis("Vertical")*100);
        var direction = Head.transform;
        direction.localEulerAngles = new Vector3(0, direction.transform.localEulerAngles.y, 0);
        move = direction.TransformDirection(move);
        rb.AddForce(move);

        //視点
        CameraAngle.x -= Input.GetAxis("Mouse Y");
        CameraAngle.y += Input.GetAxis("Mouse X");

        var spine2eulerX = Spine2.transform.localEulerAngles.x;//Spine2のx軸の回転角

        if (spine2eulerX > 90)//0度以下の時の補正
        {
            spine2eulerX -= 360;
        }
        if (CameraAngle.x - spine2eulerX > 85)//下向き限界
        {
            CameraAngle.x = 85;
        }
        if(CameraAngle.x - spine2eulerX < -85)//上むき限界
        {
            CameraAngle.x = -85;
        }

        Head_rotation.eulerAngles = CameraAngle;//Headの回転をVector3からQuaternionへ
        //体の回転
        //Debug.Log(Head.transform.localEulerAngles.y + "  ,  " + Pelvis_rotation.eulerAngles.y);
        var PelvisTarget = Quaternion.Euler(0, Head.transform.eulerAngles.y, 0);
        var HeadTarget = Quaternion.Euler(Head_rotation.eulerAngles.x, Pelvis_rotation.eulerAngles.y, Head_rotation.eulerAngles.z);
        //Quaternion.RotateTowards(始点クォータニオン,終点クォータニオン,速度);・・・始点クォータニオンから終点クォータニオンまで滑らかに遷移する。

        if (Head.transform.localEulerAngles.y > RotationLimit && Head.transform.localEulerAngles.y < 180)//時計回り
        {
            Pelvis_rotation = Quaternion.RotateTowards(Pelvis_rotation ,PelvisTarget ,15 );
            Head_rotation = Quaternion.RotateTowards(Head_rotation, HeadTarget, 15);
        }

        if (Head.transform.localEulerAngles.y < 360 -RotationLimit && Head.transform.localEulerAngles.y > 180)//反時計回り
        {
            Pelvis_rotation = Quaternion.RotateTowards(Pelvis_rotation, PelvisTarget, 15);
            Head_rotation = Quaternion.RotateTowards(Head_rotation, HeadTarget, 15);
        }
        Pelvis.transform.localPosition = new Vector3(0, 0.01f, 0);
    }

    private void LateUpdate()
    {
        
        Head.transform.rotation = Head_rotation;
        Pelvis.transform.rotation = Pelvis_rotation;

        Head_rotation.eulerAngles = CameraAngle;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "TrueDeath"&& !a)
        {
            a = true;
            GameObject DeadAvator = (GameObject)Resources.Load("DeadAvator");//Resourcesフォルダからプレハブを読み込む

            GameObject corpse = Instantiate(DeadAvator, Pelvis.transform.position, transform.rotation);//死体生成
            corpse.GetComponent<DeadState>().vel = rb.velocity;

            //死体用のクォータニオン
            corpserot = new Quaternion[] {Pelvis.transform.rotation,Leg_L.transform.rotation, Knee_L.transform.rotation, Leg_R.transform.rotation, Knee_R.transform.rotation,
                                   Spine1.transform.rotation,Spine2.transform.rotation,Head.transform.rotation,LeftArm.transform.rotation,LeftElbow.transform.rotation,
                                   RightArm.transform.rotation,RightElbow.transform.rotation };

            corpse.GetComponent<DeadState>().rotation = corpserot;

            List_Corpse.Add(corpse);//リストに追加
            Debug.Log(List_Corpse.Count);
            if (List_Corpse.Count > 5)//死体が5体より多いなら古いものから消す
            {
                Destroy(List_Corpse[0]);
                List_Corpse.RemoveAt(0);
                
            }

            rb.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(0, 10, 5);//リスポーン
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "TrueDeath")
        {
            a = false;
        }
    }

}
