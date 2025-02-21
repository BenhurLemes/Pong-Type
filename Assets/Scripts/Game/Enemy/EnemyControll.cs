using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 6f;
    public SpriteRenderer spriteRenderer;

    public bool isAI = true; 
    public string movementAxisName = "Vertical";
    private bool isDashing = false;
    private float dashDuration = 0.2f;

    public Vector2 limits = new Vector2(-4.5f, 4.5f);

    private GameObject Ball;

    public void SetAsPlayer2()
    {
        isAI = false;
    }

    public void SetAsAI()
    {
        isAI = true;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Ball = GameObject.Find("Ball");
        spriteRenderer.color = SaveController.Instance.colorEnemy;
    }

    void Update()
    {
        if (isAI)
        {
            ControlByAI();
        }
        else
        {
            ControlByPlayer();
        }
    }

    void ControlByAI()
    {
        if (Ball != null)
        {
            float targetY = Mathf.Clamp(Ball.transform.position.y, limits.x, limits.y);
            Vector2 targetPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        }
    }

    void ControlByPlayer()
    {
        float movePlayer = Input.GetAxis(movementAxisName);

        if (!isDashing)
        {
            Vector3 newPosition = transform.position + Vector3.up * movePlayer * speed * Time.deltaTime;
            newPosition.y = Mathf.Clamp(newPosition.y, limits.x, limits.y);
            transform.position = newPosition;
        }

        if (Input.GetKeyDown(KeyCode.B) && !isDashing)
        {
            StartCoroutine(Dash(movePlayer));
        }
    }

    IEnumerator Dash(float y)
    {
        isDashing = true;
        rb.velocity = new Vector2(0, y * speed * 2);

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector2.zero;
        isDashing = false;
    }
}
