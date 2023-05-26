using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

    [SerializeField] private PlayerMove PlayerPrefab;
    [SerializeField] private GameObject SpawnPosition;

    public bool isFinished = false;
    private float Timer;
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
        if(Input.GetKeyUp(KeyCode.Escape))  QuitGame(); 
        if (isFinished) {
            if ((!_playerMove.isPlayerMovement) && Input.GetKeyDown(KeyCode.R)) Restart();
            _UI.ToggleWinText(true);
            _playerMove.isPlayerMovement = false;
            _playerMove.StopPhysics();
        } else if (!_playerMove.isPlayerMovement && Input.GetKeyDown(KeyCode.Space)) ExitMenu();
        else {
            Timer += Time.deltaTime;
            _UI.UpdateTimer(Timer);
        }

    }

    private void Restart() {
        SceneManager.LoadScene("Scene");
    }

    public void SpawnPlayer() {
        PlayerMove player = Instantiate(PlayerPrefab, SpawnPosition.transform.position, SpawnPosition.transform.rotation);
    }

    private void ExitMenu() {
        Timer = 0;
        isFinished = false;
        _playerMove.isPlayerMovement = true;
        _spectatorCamera.enabled = false;
        _playerMove.GetComponentInChildren<Camera>().enabled = true;
        _coinController.coinNumber = 0;
        _UI.UpdateCoinCounter(_coinController.coinNumber);
        _UI.ToggleCoinText(true);
        _UI.ToggleStartText(false);
        _UI.ToggleControlsText(false);
        _UI.ToggleTimerText(true);
    }
    public void QuitGame() {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

}
