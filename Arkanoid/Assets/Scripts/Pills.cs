using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pills : MonoBehaviour {

    public Main main;
    public bool pillDestroy = false;
    Vector3 pillDirection = new Vector3(0, -1, 0);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float pillSpeed = main.pillsSpeed;
        this.transform.position += pillDirection * pillSpeed * Time.deltaTime;

        //desaparecer la pill cuando toque la paddle
        Vector3 pillPosition = this.transform.position;
        //BoxCollider2D pillCollider = this.GetComponent<BoxCollider2D>();
        if (main.paddleCollider.bounds.Contains(pillPosition))
        {
            switch (main.pillNumber)
            {
                case 0:
                    main.powerLarge = true;
                    break;

                case 1:
                    main.slowBall = true;
                    break;

                case 2:
                    main.tripleBall = true;
                    break;
            }
            this.transform.position = new Vector3(-27,-2);
        }
        //main.powerLarge = false;
        //main.slowBall = false;
        //main.tripleBall = false;
    }
}
