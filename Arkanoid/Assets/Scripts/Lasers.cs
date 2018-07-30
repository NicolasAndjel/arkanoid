using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour {

    public float laserSpeed;
    public GameObject[] bricks;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 1) * laserSpeed * Time.deltaTime;

        for (int i = 0; i < bricks.Length; i++)
        {
            //Obtengo el collider del enemigo
            BoxCollider2D brickCollider = bricks[i].GetComponent<BoxCollider2D>();
            //Chequeo la interseccin del enemigo y la bala
            if (brickCollider.bounds.Contains(transform.position))
            {
                //if (bricks.color != "Grey") // busco la propiedad color dentro de ese script.
                //{
                //    Main.lastBrokenBrick = bricks[i].transform.position;
                //    bricks[i].SetActive(false);
                //    Main.score += 100;
                //    Main.countPowerUp++;
                //    Debug.Log("laser pegó a un brick");
                //}
                //Destruyo el laser
                Destroy(gameObject);
            }
        }
    }
}
