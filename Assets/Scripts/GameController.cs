using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private Renderer cueRenderer;
    private bool playerTurn;
    private UnityEngine.UI.Text turn;
    private UnityEngine.UI.Text balls;

    private int countStripedBalls;
    private int countColoredBalls;

    private enum BallType {Striped, Colored, Black, White, None};
    private BallType player1BallsSelected;
    private BallType player2BallsSelected;

    // Use this for initialization
    void Start () {
        cueRenderer = GameObject.FindGameObjectWithTag("Cue").GetComponent<Renderer>();
        playerTurn = true;
        setColor(playerTurn);

        countStripedBalls = countColoredBalls = 0;

        turn = GameObject.FindGameObjectWithTag("Turn").GetComponent<UnityEngine.UI.Text>();
        balls = GameObject.FindGameObjectWithTag("Balls").GetComponent<UnityEngine.UI.Text>();

        player1BallsSelected = player2BallsSelected = BallType.None;
    }
	
    void ChangePlayer()
    {
        playerTurn = !playerTurn;
        setColor(playerTurn);
        turn.text = (playerTurn) ? "1" : "2";

        string ballSelectedText = (!playerTurn) ? ballTypeToText(player1BallsSelected) : ballTypeToText(player2BallsSelected);
        balls.text = ballSelectedText;
    }
    void BallPotted(Collider ball)
    {
        Debug.Log("Ball potted:" + ball.tag);
        BallType ballType;
        switch (ball.tag)
        {
            case "WhiteBall": ballType = BallType.White;
                ball.attachedRigidbody.position = new Vector3(-10, 2, 2);
                break;
            case "BlackBall":
                ballType = BallType.Black;
                break;
            case "ColoredBall":
                ballType = BallType.Colored;
                break;
            case "StripedBall":
                ballType = BallType.Striped;
                break;
            default:
                ballType = BallType.None;
                break;
        }
        if (ballType == BallType.Colored)
            countColoredBalls++;
        if (ballType == BallType.Striped)
            countStripedBalls++;

        if(ballType == BallType.Black)
        {
            if (playerTurn)
            {
                if(player1BallsSelected == BallType.Colored)
                {
                    if (countColoredBalls < 7)
                        SceneManager.LoadScene("Demo");

                }
                else if (player2BallsSelected == BallType.Striped)
                {
                    if (countStripedBalls < 7)
                        SceneManager.LoadScene("Demo");
                }
            } else
            {
                if (player2BallsSelected == BallType.Colored)
                {
                    if (countColoredBalls < 7)
                        SceneManager.LoadScene("Demo");

                }
                else if (player2BallsSelected == BallType.Striped)
                {
                    if (countStripedBalls < 7)
                        SceneManager.LoadScene("Demo");
                }
            }
        }
        if (playerTurn)
        {
            if(player1BallsSelected == BallType.None)
            {
                player1BallsSelected = ballType;
                player2BallsSelected = (ballType == BallType.Striped)? BallType.Colored : BallType.Striped;
            } else if (player1BallsSelected != ballType) {
                Debug.Log("Cheater!");
            }
        } else
        {
            if (player2BallsSelected == BallType.None)
            {
                player2BallsSelected = ballType;

                player1BallsSelected = (ballType == BallType.Striped) ? BallType.Colored : BallType.Striped;
            }
            else if (player2BallsSelected != ballType)
            {
                Debug.Log("Cheater!");
            }
        }

        string ballSelectedText = (!playerTurn) ? ballTypeToText(player1BallsSelected) : ballTypeToText(player2BallsSelected);
        balls.text = ballSelectedText;



    }

    private string ballTypeToText(BallType ballType)
    {
        switch (ballType)
        {
            case BallType.Colored:
                return "Colored";
            case BallType.Striped:
                return "Striped";
            case BallType.White:
                return "White";
            case BallType.Black:
                return "Black";
            default:
                return "None yet";
        }
    }
    private void setColor(bool player)
    {
        if (player) cueRenderer.material.SetColor("_Color", Color.red); else cueRenderer.material.SetColor("_Color", Color.blue);
    }


}
