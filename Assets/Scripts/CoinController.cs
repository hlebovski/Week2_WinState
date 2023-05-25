using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    [SerializeField] public int coinNumber;
    [SerializeField] public int coinsOnLevel;
    private UIController _UI;
    private LevelController _levelController;

    private void Awake() {
        _UI = FindObjectOfType<UIController>();
        _levelController = FindObjectOfType<LevelController>();

        Coin[] coinsFound = FindObjectsOfType<Coin>();
        coinsOnLevel = coinsFound.Length;
    }

    public void CollectCoin() {
        coinNumber++;
        _UI.UpdateCoinCounter(coinNumber);
        if(coinNumber == coinsOnLevel) {
            _levelController.isFinished = true;
        }
    }

}
