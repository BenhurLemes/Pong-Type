using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PaddleController : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 5f;
    private float Seconds = 0.2f;
    private Rigidbody2D rb;
    private bool isDashing = false;
    public string movementAxisName = "Vertical";
    public bool isPlayer = true;
    public SpriteRenderer spriteRenderer;

    public Vector2 limits = new Vector2(-4.5f, 4.5f);

    private GameObject Ball;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (isPlayer)
        {
            spriteRenderer.color = SaveController.Instance.colorPlayer;
        }
        else
        {
            spriteRenderer.color = SaveController.Instance.colorEnemy;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float movePlayer = Input.GetAxis(movementAxisName);

        if (!isDashing)
        {
            // Calcula a nova posição da raqueta baseada na entrada e na velocidade
            Vector3 newPosition = transform.position + Vector3.up * movePlayer * speed * Time.deltaTime;

            // Limita a posição vertical da raquete para que ela não saia do mapa
            newPosition.y = Math.Clamp(newPosition.y, -4.5f, 4.5f);

            // atualiza a posição da raquete
            transform.position = newPosition;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            StartCoroutine(Dash(movePlayer));

        }
    }

    IEnumerator Dash(float y)
    {
        isDashing = true;
        rb.velocity = new Vector2(0, y * speed * 2);

        yield return new WaitForSeconds(Seconds);

        rb.velocity = Vector2.zero;
        isDashing = false;
    }

}
