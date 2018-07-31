using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pills : MonoBehaviour {

    public string pillType;
    Vector3 pillDirection = new Vector3(0, -1, 0);
    public float pillsSpeed = 1.5f;
    public GameObject gameController;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        gameController = GameObject.Find("Game Controller"); //Busca el GameObject en runtime
        Main main = gameController.GetComponent<Main>(); // Dame su script así puedo referenciar a sus variables
        Vector3 pillPosition = this.transform.position;
        this.transform.position += pillDirection * pillsSpeed * Time.deltaTime;

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

                case "laser":
                    Main.laser = true;
                    break;

                case "tripleball":
                    Main.tripleBall = true;
                    break;
            }
            Destroy(gameObject);
        }
        else if (main.losePanel.activeInHierarchy || main.winPanel.activeInHierarchy)
        {
            Destroy(gameObject);
        }
        
    }
}
