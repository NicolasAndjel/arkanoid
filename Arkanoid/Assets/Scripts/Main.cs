using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour {
    //Paddle Controls
    public SpriteRenderer paddleRenderer;
    public float paddleSpeed;
    Vector3 paddleDirection;
    public BoxCollider2D gameArea;
    Vector3 paddlePosition;
    //Ball Controls
    public SpriteRenderer ballRenderer;
    Vector3 ballDirection = new Vector3(1, 1, 0);
    public float ballSpeed;
    bool kickOff = false;
    //bool followPaddle = true;

    //Walls
    public SpriteRenderer wall_left_Renderer;
    public SpriteRenderer wall_right_Renderer;
    public SpriteRenderer wall_top_Renderer;
    public SpriteRenderer wall_bottom_Renderer;
    //tiles
    public GameObject redTilePrefab;
    public GameObject yellowTilePrefab;
    public GameObject blueTilePrefab;
    public GameObject purpleTilePrefab;
    public GameObject greenTilePrefab;
    public GameObject greyTilePrefab;
    //lives and score
    public GameObject[] lives;
    public int lifeQuantity;
    public Text scoreDisplay;
    public int score;

    //menus
    public GameObject winPanel;
    public GameObject losePanel;

    //Bricks
    public GameObject[] bricks;

    //Power Ups
    public bool powerLarge = false;
    public bool slowBall = false;
    public bool tripleBall = false;



    // Use this for initialization
    void Start () {
        


    }

    // Update is called once per frame
    void Update () {
        if(!losePanel.activeInHierarchy && !winPanel.activeInHierarchy)
        {
            //Paddle Behavior
            paddleDirection.x = Input.GetAxis("Horizontal");
            Vector3 paddleNextPosition = paddleRenderer.transform.position + paddleDirection * paddleSpeed * Time.deltaTime;
            if (gameArea.bounds.Contains(paddleNextPosition))
            {
                paddleRenderer.transform.position += paddleDirection * paddleSpeed * Time.deltaTime;
            }


            #region Colliders
            BoxCollider2D leftWallCollider = wall_left_Renderer.GetComponent<BoxCollider2D>();
            BoxCollider2D rightWallCollider = wall_right_Renderer.GetComponent<BoxCollider2D>();
            BoxCollider2D topWallCollider = wall_top_Renderer.GetComponent<BoxCollider2D>();
            BoxCollider2D bottomWallCollider = wall_bottom_Renderer.GetComponent<BoxCollider2D>();
            BoxCollider2D paddleCollider = paddleRenderer.GetComponent<BoxCollider2D>();
            //CircleCollider2D ballCollider = ballRenderer.GetComponent<CircleCollider2D>();

            #endregion

            #region Ball Behavior
            if (Input.GetKeyUp(KeyCode.Space))
            {
                kickOff = true;
            }

            if (kickOff == true)
            {
                ballRenderer.transform.position += ballDirection * ballSpeed * Time.deltaTime;
            }
            else
            {
                ballRenderer.transform.position = paddleRenderer.transform.position + new Vector3(0, 0.4f);
            }

            Vector3 ballNextPosition = ballRenderer.transform.position + ballDirection * ballSpeed * Time.deltaTime;

            if (
                rightWallCollider.bounds.Contains(ballNextPosition) ||
                leftWallCollider.bounds.Contains(ballNextPosition)
                )
            {
                ballDirection.x *= -1;
            }

            else if (
                topWallCollider.bounds.Contains(ballNextPosition) ||
                paddleCollider.bounds.Contains(ballNextPosition)
                )
            {
                ballDirection.y *= -1;
                float paddleHeightBottom = -(paddleCollider.size.y / 2);// borde inferior del paddle
                float paddleHeightTop = (paddleCollider.size.y / 2);// borde superior del paddle
                float ballPaddleDiff = paddleCollider.transform.position.y - ballRenderer.transform.position.y;
                if (paddleCollider.bounds.Contains(ballNextPosition) && (ballPaddleDiff < paddleHeightTop && ballPaddleDiff > paddleHeightBottom))
                {
                    ballDirection.x *= -1;
                }
            }

            #endregion

            #region loose life and game
            else if (bottomWallCollider.bounds.Contains(ballNextPosition))
            {
                lifeQuantity -= 1;
                Debug.Log("Quedan " + lifeQuantity + " vidas");
                kickOff = false;
                if (lifeQuantity < 0)
                {
                    ballRenderer.transform.position = new Vector3(9999,9999);
                    losePanel.SetActive(true);

                }
                else
                {
                    for (int i = 0; i < lives.Length; i++)
                    {
                        lives[lifeQuantity].SetActive(false);
                    }
                }
            }
            #endregion

            #region Brick destruction 
            bool endGame = true;
            for (int i = 0; i < bricks.Length; i++)
            {
                BoxCollider2D brickCollider = bricks[i].GetComponent<BoxCollider2D>();
                Brick brick = bricks[i].GetComponent<Brick>();// dame el script que tiene asignado este objeto (bricks[i]) de unity.
                float brickHeightBottom = -(brickCollider.size.y / 2);// borde inferior del ladrillo
                float brickHeightTop = (brickCollider.size.y / 2);// borde superior del ladrillo
                float brickBallDiff = brickCollider.transform.position.y - ballRenderer.transform.position.y;//doferencia entre valor Y de la pelota y del ladrillo
                if (brickCollider.bounds.Contains(ballNextPosition) && (brickBallDiff < brickHeightTop && brickBallDiff > brickHeightBottom))
                {
                    ballDirection.x *= -1;
                    if (brick.color != "Grey") // busco la propiedad color dentro de ese script.
                    {
                        bricks[i].SetActive(false);
                        score += 100;
                    }
                }
                else if (brickCollider.bounds.Contains(ballNextPosition))
                {
                    ballDirection.y *= -1;
                    if (brick.color != "Grey") // busco la propiedad color dentro de ese script.
                    {
                        bricks[i].SetActive(false);
                        score += 100;
                    }
                }
            }
            #endregion

            #region win condition
            for (int i = 0; i < bricks.Length; i++)
            {
                Brick brick = bricks[i].GetComponent<Brick>(); // dame el script que tiene asignado este objeto (bricks[i]) de unity.
                if (brick.color == "" && brick.isActiveAndEnabled)
                {
                    endGame = false;
                    break;
                }
            }
            if (endGame) 
            {
                winPanel.SetActive(true);
            }
            #endregion
           
            #region UI
            scoreDisplay.text = "Score: " + score;

            #endregion


            #region Power Ups
            //LargePaddle
            if (powerLarge == true)
            {
                paddleRenderer.transform.localScale += new Vector3(0.5f, 0, 0);
                gameArea.transform.localScale -= new Vector3(0.1f, 0, 0);
                powerLarge = false;
            }

            //SlowBall
            if (slowBall == true)
            {
                ballSpeed -= 1;
                slowBall = false;
            }

            //tripleBall
            if (tripleBall == true)
            {
                //Instantiate(ballPrefab, ballNextPosition, Quaternion.identity);
                tripleBall = false;
            }

            #endregion

        }



    }

    }

