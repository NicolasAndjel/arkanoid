using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour {

    public float laserSpeed;
    public GameObject gameController; 
    

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        gameController = GameObject.Find("Game Controller"); //Busca el GameObject en runtime
        Main main = gameController.GetComponent<Main>(); // Dame su script así puedo referenciar a sus variables
        transform.position += new Vector3(0, 1) * laserSpeed * Time.deltaTime;

        for (int i = 0; i < main.bricks.Length; i++)
        {
            //Obtengo el collider del brick
            BoxCollider2D brickCollider = main.bricks[i].GetComponent<BoxCollider2D>();
            Brick brick = main.bricks[i].GetComponent<Brick>();
            
            //Chequeo la interseccin del brick y el laser
            if (brickCollider.bounds.Contains(transform.position))
            {
                if (brick.color == "Grey")
                {
                    brick.timesHit++;
                    if (brick.timesHit == 2)
                    {
                        Main.lastBrokenBrick = main.bricks[i].transform.position;
                        main.bricks[i].SetActive(false);
                        Main.score += 200;
                        Main.countPowerUp++;
                    }
       
                }
                else if (brick.color != "Gold") // busco la propiedad color dentro de ese script.
                {
                    Main.lastBrokenBrick = main.bricks[i].transform.position;
                    main.bricks[i].SetActive(false);
                    Main.score += 100;
                    Main.countPowerUp++;
                    Debug.Log("laser pegó a un brick");
                }
                //Destruyo el laser
                Destroy(gameObject);
            }
        }
    }
}
