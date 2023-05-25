using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private Transform _playerTransform;
    private CoinController _coinController;

    private void Start() {
        _playerTransform = FindObjectOfType<PlayerMove>().transform;
        _coinController = FindObjectOfType<CoinController>();
    }

    void Update() {
        float Distance = Vector3.Distance(transform.position, _playerTransform.position);

        if (Distance < 0.9f) {
            _coinController.CollectCoin();
            Destroy(gameObject);
        }
    }
}
