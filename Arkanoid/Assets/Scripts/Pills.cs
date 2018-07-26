using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pills : MonoBehaviour {

    public string pillType;
    Vector3 pillDirection = new Vector3(0, -1, 0);
    float pillSpeed = Main.pillsSpeed;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pillPosition = this.transform.position;
        this.transform.position += pillDirection * pillSpeed * Time.deltaTime;

        //desaparecer la pill cuando toque la paddle
        if (Main.paddleCollider.bounds.Contains(pillPosition))
        {
            switch (pillType)
            {
                case "enlarge":
                    Main.powerLarge = true;
                    break;

                case "slowball":
                    Main.slowBall = true;
                    break;

                case "tripleball":
                    Main.tripleBall = true;
                    break;
            }
            Destroy(gameObject);
        }
        
    }
}
