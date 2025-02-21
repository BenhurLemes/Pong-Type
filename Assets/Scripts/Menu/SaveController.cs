using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    public Color colorPlayer = Color.white;
    public Color colorEnemy = Color.white;
    public string PlayerName;
    public string EnemyName;

    private static SaveController _instance;
    private string SavedWinnerKey = "SavedWinner";

    public static SaveController Instance
    {
        get
        {
            if (_instance == null)
            {
                // procura a instancia na cena
                _instance = FindObjectOfType<SaveController>();

                // se não achar ele vai criar
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(SaveController).Name);
                    _instance = singletonObject.AddComponent<SaveController>();
                }
            }
            return _instance;
        }
    }

    public string GetName(bool isPlayer)
    {
        return isPlayer ? PlayerName : EnemyName;
    }

    public void Reset()
    {
        PlayerName = "";
        EnemyName = "";
        colorPlayer = Color.white;
        colorEnemy = Color.white;
    }

    public void SaveWinner(string winner)
    {
        PlayerPrefs.SetString(SavedWinnerKey, winner);
    }

    public string GetWinner()
    {
        return PlayerPrefs.GetString(SavedWinnerKey);
    }

    public void ClearSave()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // Mantenha o Singleton vivo entre as cenas
        DontDestroyOnLoad(this.gameObject);
    }
}
