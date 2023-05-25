using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    [SerializeField] private PlayerMove PlayerPrefab;
    [SerializeField] private GameObject SpawnPosition;

    public bool isFinished = false;
    private PlayerMove _playerMove;
    private Camera _spectatorCamera;
    private CoinController _coinController;
    private UIController _UI;


    private void Awake() {
        _spectatorCamera = FindObjectOfType<Camera>();
        _UI = FindObjectOfType<UIController>();
        _coinController = FindObjectOfType<CoinController>();

        SpawnPlayer();
        _playerMove = FindObjectOfType<PlayerMove>();
    }

    private void Update() {
        if (!_playerMove.isPlayerMovement) {
            if (!isFinished && Input.GetKeyDown(KeyCode.Space))  ExitMenu(); 
            if (isFinished && Input.GetKeyDown(KeyCode.R))  Restart();
        } else if (isFinished) {

            _UI.ToggleWinText(true);
            _playerMove.isPlayerMovement = false;
            _playerMove.StopPhysics();

        }
    }

    private void Restart() {
        SceneManager.LoadScene("Scene");
    }

    public void SpawnPlayer() {
        PlayerMove player = Instantiate(PlayerPrefab, SpawnPosition.transform.position, SpawnPosition.transform.rotation);
    }

    private void ExitMenu() {
        isFinished = false;
        _playerMove.isPlayerMovement = true;
        _spectatorCamera.enabled = false;
        _playerMove.GetComponentInChildren<Camera>().enabled = true;
        _coinController.coinNumber = 0;
        _UI.UpdateCoinCounter(_coinController.coinNumber);
        _UI.ToggleCoinText(true);
        _UI.ToggleStartText(false);
        _UI.ToggleControlsText(false);
    }

}
