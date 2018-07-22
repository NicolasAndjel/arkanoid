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
    float paddleHeightBottom;// borde inferior del paddle
    float paddleHeightTop;// borde superior del paddle

    //Ball Controls
    public SpriteRenderer ballRenderer;
    Vector3 ballDirection = new Vector3(1, 1, 0);
    public float ballSpeed;
    bool kickOff = false;

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

    //Colliders

    BoxCollider2D leftWallCollider;
    BoxCollider2D rightWallCollider;
    BoxCollider2D topWallCollider;
    BoxCollider2D bottomWallCollider;
    BoxCollider2D paddleCollider;

    //Power Ups
    public bool powerLarge = false;
    public bool slowBall = false;
    public bool tripleBall = false;
    public GameObject extraball1;
    public GameObject extraball2;
    Vector3 extraball1Direction;
    Vector3 extraball2Direction;
    bool extraBall1isActive = false;
    bool extraBall2isActive = false;

    // Use this for initialization
    void Start () {
        leftWallCollider = wall_left_Renderer.GetComponent<BoxCollider2D>();
        rightWallCollider = wall_right_Renderer.GetComponent<BoxCollider2D>();
        topWallCollider = wall_top_Renderer.GetComponent<BoxCollider2D>();
        bottomWallCollider = wall_bottom_Renderer.GetComponent<BoxCollider2D>();
        paddleCollider = paddleRenderer.GetComponent<BoxCollider2D>();

        paddleHeightBottom = -(paddleCollider.size.y / 2);// borde inferior del paddle
        paddleHeightTop = (paddleCollider.size.y / 2);// borde superior del paddle

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
            Vector3 extraball1NextPosition;
            Vector3 extraball2NextPosition;

            


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
                
                float ballPaddleDiff = paddleCollider.transform.position.y - ballRenderer.transform.position.y;
                if (paddleCollider.bounds.Contains(ballNextPosition) && (ballPaddleDiff < paddleHeightTop && ballPaddleDiff > paddleHeightBottom))
                {
                    ballDirection.x *= -1;
                }
            }

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
                extraball1.transform.position = ballNextPosition;
                extraball2.transform.position = ballNextPosition;
                extraball1Direction.x = -ballDirection.x;
                extraball1Direction.y = ballDirection.y;
                extraball2Direction.x = ballDirection.x;
                extraball2Direction.y = -ballDirection.y;
                extraBall1isActive = true;
                extraBall2isActive = true;
                tripleBall = false;
            }
            if (extraBall1isActive == true)
            {
                extraball1.transform.position += extraball1Direction * ballSpeed * Time.deltaTime;
                extraball1NextPosition = extraball1.transform.position + extraball1Direction * ballSpeed * Time.deltaTime;

                if (
                rightWallCollider.bounds.Contains(extraball1NextPosition) ||
                leftWallCollider.bounds.Contains(extraball1NextPosition)
                )
                {
                    extraball1Direction.x *= -1;
                }

                else if (
                    topWallCollider.bounds.Contains(extraball1NextPosition) ||
                    paddleCollider.bounds.Contains(extraball1NextPosition)
                    )
                {
                    extraball1Direction.y *= -1;
                    float ballPaddleDiff = paddleCollider.transform.position.y - extraball1.transform.position.y;
                    if (paddleCollider.bounds.Contains(extraball1NextPosition) && (ballPaddleDiff < paddleHeightTop && ballPaddleDiff > paddleHeightBottom))
                    {
                        extraball1Direction.x *= -1;
                    }
                }

            }
            else
            {
                extraball1NextPosition = new Vector3(50, 50, 0);
            }

            if (extraBall2isActive == true)
            {
                extraball2.transform.position += extraball2Direction * ballSpeed * Time.deltaTime;
                extraball2NextPosition = extraball2.transform.position + extraball2Direction * ballSpeed * Time.deltaTime;

                if (
                rightWallCollider.bounds.Contains(extraball2NextPosition) ||
                leftWallCollider.bounds.Contains(extraball2NextPosition)
                )
                {
                    extraball2Direction.x *= -1;
                }

                else if (
                    topWallCollider.bounds.Contains(extraball2NextPosition) ||
                    paddleCollider.bounds.Contains(extraball2NextPosition)
                    )
                {
                    extraball2Direction.y *= -1;
                    float ballPaddleDiff = paddleCollider.transform.position.y - extraball2.transform.position.y;
                    if (paddleCollider.bounds.Contains(extraball2NextPosition) && (ballPaddleDiff < paddleHeightTop && ballPaddleDiff > paddleHeightBottom))
                    {
                        extraball2Direction.x *= -1;
                    }
                }
            }
            else
            {
                extraball2NextPosition = new Vector3(50, 50, 0);
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
                float brickBallDiff = brickCollider.transform.position.y - ballRenderer.transform.position.y;//diferencia entre valor Y de la pelota y del ladrillo
                float brickextraBall1Diff = brickCollider.transform.position.y - extraball1.transform.position.y;//diferencia entre valor Y de la pelota extra 1 y del ladrillo
                float brickextraBall2Diff = brickCollider.transform.position.y - extraball2.transform.position.y;//diferencia entre valor Y de la pelota extra 2 y del ladrillo
                
                //Si la pelota original toca un brick
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
                
                //Si la pelota extra1 toca un brick
                if (brickCollider.bounds.Contains(extraball1NextPosition) && (brickextraBall1Diff < brickHeightTop && brickextraBall1Diff > brickHeightBottom))
                {
                    extraball1Direction.x *= -1;
                    if (brick.color != "Grey") // busco la propiedad color dentro de ese script.
                    {
                        bricks[i].SetActive(false);
                        score += 100;
                    }
                }
                else if (brickCollider.bounds.Contains(extraball1NextPosition))
                {
                    extraball1Direction.y *= -1;
                    if (brick.color != "Grey") // busco la propiedad color dentro de ese script.
                    {
                        bricks[i].SetActive(false);
                        score += 100;
                    }
                }

                //Si la pelota extra2 toca un brick
                if (brickCollider.bounds.Contains(extraball2NextPosition) && (brickextraBall2Diff < brickHeightTop && brickextraBall2Diff > brickHeightBottom))
                {
                    extraball2Direction.x *= -1;
                    if (brick.color != "Grey") // busco la propiedad color dentro de ese script.
                    {
                        bricks[i].SetActive(false);
                        score += 100;
                    }
                }
                else if (brickCollider.bounds.Contains(extraball2NextPosition))
                {
                    extraball2Direction.y *= -1;
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

            #region loose life and game
            if (!extraBall1isActive && !extraBall2isActive)
            {
                if (bottomWallCollider.bounds.Contains(ballNextPosition))
                {
                    lifeQuantity -= 1;
                    Debug.Log("Quedan " + lifeQuantity + " vidas");
                    kickOff = false;
                    if (lifeQuantity < 0)
                    {
                        ballRenderer.transform.position = new Vector3(9999, 9999);
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
            }
            else
            {
                if (
                    (ballNextPosition.y < bottomWallCollider.transform.position.y) && 
                    (extraball1NextPosition.y < bottomWallCollider.transform.position.y) && 
                    (extraball2NextPosition.y < bottomWallCollider.transform.position.y)
                   )
                {
                    lifeQuantity -= 1;
                    Debug.Log("Quedan " + lifeQuantity + " vidas");
                    kickOff = false;
                    if (lifeQuantity < 0)
                    {
                        ballRenderer.transform.position = new Vector3(9999, 9999);
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
                 
            }
            
            #endregion

            #region UI
            scoreDisplay.text = "Score: " + score;

            #endregion




        }



    }

    }

