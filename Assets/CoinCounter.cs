using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Awake()
    {
        EventsManager.StartListening("PlayerCatchCoin", UpdateCoinCounter);
        textMeshProUGUI.text = Score.Coins.ToString();

    }

    void UpdateCoinCounter(Args args)
    {
        textMeshProUGUI.text = Score.Coins.ToString();
    }

}
