using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI uiWinner;

    private void Start()
    {
        SaveController.Instance.Reset();
        string lastWinner = SaveController.Instance.GetWinner();

        if(lastWinner != "")
        {
            uiWinner.text = "Ultimo vencedor: " + lastWinner;
        }
        else
        {
            uiWinner.text = "";
        }
    }
}
