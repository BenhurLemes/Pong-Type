using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class scriptBallController : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;
    public Vector2 startingVelocity = new Vector2(6f, 6f);
    public GameManager gameManager;
    private bool isSpeedBall = false;

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        if (Rigidbody2D == null)
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }
        Rigidbody2D.velocity = startingVelocity;
        isSpeedBall = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            Vector2 newVelocity = Rigidbody2D.velocity;

            newVelocity.y = -newVelocity.y;
            Rigidbody2D.velocity = newVelocity;
        }

        if (collision.gameObject.tag == "Wall_Point_Player")
        {
            gameManager.ScoreEnemy();
            ResetBall();
        }

        if (collision.gameObject.tag == "Wall_Point_Enemy")
        {
            gameManager.ScorePlayer();
            ResetBall();

        }

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            Rigidbody2D.velocity = new Vector2(-Rigidbody2D.velocity.x, Rigidbody2D.velocity.y);
        }
    }

    private void Update()
    {
        if(gameManager.TimerDuration <= gameManager.Timer / 2 && !isSpeedBall)
        {
            isSpeedBall = true;
            Rigidbody2D.velocity *= 2;
        }
    }
}
