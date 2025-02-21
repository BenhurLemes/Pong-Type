using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    public bool isPlayer;
    public TMP_InputField inputField;

    private void Start()
    {
        inputField.onValueChanged.AddListener(UpdateName);
    }

    private void UpdateName(string name)
    {
        if (isPlayer)
        {
            SaveController.Instance.PlayerName = name;
        }
        else
        {
            SaveController.Instance.EnemyName = name;
        }
    }
}
