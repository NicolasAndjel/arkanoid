using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pills : MonoBehaviour {

    public Main main;
    public bool pillActive = false;
    Vector3 pillDirection = new Vector3(0, -1, 0);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float pillSpeed = main.pillsSpeed;
        this.transform.position += pillDirection * pillSpeed * Time.deltaTime;
        
    }
}
