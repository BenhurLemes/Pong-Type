using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Variáveis")]
    public GameObject EnemyPaddle;
    public GameObject PlayerPaddle;
    public GameObject Ball;
    public GameObject ScreenEndGame;

    public int Player_Pointers;
    public int Enemy_Pointers;
    public int WinPoints = 10;

    public TextMeshProUGUI textPlayer;
    public TextMeshProUGUI textEnemy;
    public TextMeshProUGUI textTimer;
    public TextMeshProUGUI TextWinner;

    public scriptBallController scriptBallController;

    public float Timer = 60f;
    public float TimerDuration;
    [SerializeField] bool isFirstTime = true;

    // getters e setters
    [SerializeField] bool isResetGame = false;
    [SerializeField] private float resetShakeDuration = 0.3f;

    #region getters e setters
    private void setResetGame(bool resetGame)
    {
        this.isResetGame = resetGame;
    }

    public bool getResetGame()
    {
        return this.isResetGame;
    }
    #endregion


    #region Funções void 
    public void ScorePlayer()
    {
        Player_Pointers++;
        textPlayer.text = Player_Pointers.ToString();
        CheckWin();
    }

    public void ScoreEnemy()
    {
        Enemy_Pointers++;
        textEnemy.text = Enemy_Pointers.ToString();
        CheckWin();
    }

    public void ResetGame()
    {
        if (!isFirstTime)
        {
            setResetGame(true);
        }

        PlayerPaddle.SetActive(true);
        EnemyPaddle.SetActive(true);
        Ball.SetActive(true);

        EnemyPaddle.transform.position = new Vector3(-7f, 0f, 0f);
        PlayerPaddle.transform.position = new Vector3(7f, 0f, 0f);
        scriptBallController.ResetBall();

        StartCoroutine(ResetShakeFlag());

        if (Enemy_Pointers >= WinPoints || Player_Pointers >= WinPoints)
        {
            Enemy_Pointers = 0;
            Player_Pointers = 0;
        }

        TimerDuration = Timer;

        textPlayer.text = Player_Pointers.ToString();
        textEnemy.text = Enemy_Pointers.ToString();
        textTimer.text = TimerDuration.ToString();

        ScreenEndGame.SetActive(false);
    }
    
    IEnumerator ResetShakeFlag()
    {
        yield return new WaitForSeconds(resetShakeDuration);
        isFirstTime = false;
        setResetGame(false);
    }

    private void CheckWin()
    {
        if(Enemy_Pointers >= WinPoints || Player_Pointers >= WinPoints)
        {
            EndGame();
            isFirstTime = true;
        }
        else
        {
            ResetGame();
        }
    }

    public void EndGame()
    {
        string winner= SaveController.Instance.GetName(Player_Pointers > Enemy_Pointers);
        TextWinner.text = "Vitória de " + winner;
        SaveController.Instance.SaveWinner(winner);
        PlayerPaddle.SetActive(false);
        EnemyPaddle.SetActive(false);
        Ball.SetActive(false);

        ScreenEndGame.SetActive(true);
        //Invoke("LoadMenu", 2f);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Timing()
    {
        if (ScreenEndGame.activeSelf)
        {
            return;
        }
        else
        {
            if (TimerDuration > 0)
            {
                TimerDuration -= Time.deltaTime;
                textTimer.text = Mathf.FloorToInt(TimerDuration).ToString();
            }
            else
            {
                ResetGame();
            }
        }
    }
    #endregion

    void Start()
    {
        ResetGame();
    }

    private void Update()
    {
        Timing();
    }
}
