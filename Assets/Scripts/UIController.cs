using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    const string coinString = "COINS X";
    const string startString = "PRESS SPACE TO START"; 
    const string winString = "LEVEL FINISHED PRESS R TO RESTART";
    const string controlsString = "WASD - movement, Mouse - look around      Collect all coins to finish level";

    [SerializeField] private Text coinText;
    [SerializeField] private Text messageText;
    [SerializeField] private Text controlsText;
    [SerializeField] private Text timerText;
    private CoinController coinController;

    private void Awake() {
        coinController = FindObjectOfType<CoinController>();
        ToggleCoinText(false);
        ToggleTimerText(false);
    }

    public void UpdateCoinCounter(int number) {
        coinText.text = coinString + number.ToString() + "/" + coinController.coinsOnLevel;
    }

    public void UpdateTimer(float time) {
        timerText.text = time.ToString();
    }

    public void ToggleText(Text text, bool toggle) {
        text.enabled = toggle;
    }

    public void ToggleCoinText(bool toggle) {
        ToggleText(coinText, toggle);
    }
    public void ToggleTimerText(bool toggle) {
        ToggleText(timerText, toggle);
    }
    public void ToggleControlsText(bool toggle) {
        ToggleText(controlsText, toggle);
    }
    public void ToggleStartText(bool toggle) {
        messageText.text = startString;
        ToggleText(messageText, toggle);
    }
    public void ToggleWinText(bool toggle) {
        messageText.text = winString;
        ToggleText(messageText, toggle);
    }


}
