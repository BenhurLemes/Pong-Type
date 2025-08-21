using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    [Header("Variaveis")]
    [SerializeField] GameManager gameManager;
    private bool isShaking = false;
    private Vector3 originalPosition;
    [SerializeField] float duration = 0.3f;
    [SerializeField] float intensity = 0.1f;
    // Start is called before the first frame update

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = FindAnyObjectByType<GameManager>();
        }
        originalPosition = transform.position;

    }


    // Update is called once per frame
    void Update()
    {
        if(gameManager.getResetGame() && !isShaking)
        {
            StartCoroutine(Shaking());
        }
    }

    IEnumerator Shaking()
    {
        isShaking = true;
        Vector3 startposition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Vector2 randomOffset = Random.insideUnitCircle * intensity;

            transform.position = new Vector3(
                startposition.x + randomOffset.x,
                startposition.y + randomOffset.y,
                startposition.z);

            yield return null;
        }

        transform.position = startposition;
        isShaking = false;
    }
}
