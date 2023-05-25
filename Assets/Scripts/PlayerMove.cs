using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour {

    public bool isPlayerMovement = false;
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float _sprintingMultiplier = 2f;
    [SerializeField] private float _horizontalSensitivity;

    private Camera _mainCamera;
    private Camera _spectatorCamera;
    private Rigidbody _rigidbody;
    private float _sprinting;
    private CoinController _coinController;
    private UIController _UI;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
        _mainCamera = GetComponentInChildren<Camera>();
        _spectatorCamera = FindObjectOfType<Camera>();
        _UI = FindObjectOfType<UIController>();
        _coinController = FindObjectOfType<CoinController>();
        _sprinting = _sprintingMultiplier;
    }

    private void ExitMenu() {
        isPlayerMovement = true;
        _spectatorCamera.enabled = false;
        _mainCamera.enabled = true;
        _coinController.coinNumber = 0;
        _UI.UpdateCoinCounter(_coinController.coinNumber);
        _UI.ToggleCoinText(true);
        _UI.ToggleStartText(false);
        _UI.ToggleControlsText(false);
    }


    private Vector2 GetInput() {
        Vector2 input = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));
        return input;
    }

    private void Update() {

        if(isPlayerMovement && Input.GetKey(KeyCode.LeftShift)) _sprinting = _sprintingMultiplier;
        else  _sprinting = 1f;

        //if (isPlayerMovement) {
        //    float input = Input.GetAxis("Mouse X");
        //    float Yrotation = input * _horizontalSensitivity * Time.deltaTime;
        //    _rigidbody.angularVelocity = new Vector3(0, Yrotation, 0);
        //}
        }

    private void FixedUpdate() {
        if (isPlayerMovement) {
            Vector2 wantedVelocity = GetInput() * _speed * _sprinting;
            Vector3 velocity = new Vector3(wantedVelocity.x, _rigidbody.velocity.y, wantedVelocity.y);
            _rigidbody.velocity = transform.TransformVector(velocity);
        }
    }

    public void StopPhysics() {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
