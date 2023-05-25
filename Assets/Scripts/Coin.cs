using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private Transform _playerTransform;

    private void Awake() {
        _playerTransform = FindObjectOfType<PlayerMove>().transform;
    }

    void Update() {

        float Distance = Vector3.Distance(transform.position, _playerTransform.position);
        if (Distance < 0.9f) {
            //FindObjectOfType<CoinCounter>().CollectedCoins++;
            Destroy(gameObject);
        }

    }
}
