using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    [SerializeField] private Vector2 _sensitivity;
    [SerializeField] private Vector2 _acceleration;
    [SerializeField] private float _maxVerticalAngle;
    [SerializeField] private float _inputLagPeriod;
    [SerializeField] private bool lockHorizontalRotation;

    private Vector2 velocity;
    private Vector2 rotation;
    private Vector2 rotationPlayer;
    private Vector2 lastInput;
    private float inputLagTimer;
    private PlayerMove _playerMove;

    private void Awake() {
        _playerMove = FindObjectOfType<PlayerMove>();
    }

    private Vector2 GetInput() {
        inputLagTimer += Time.deltaTime;

        Vector2 input = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y"));

        bool zeroInput = (Mathf.Approximately(0, input.x) && Mathf.Approximately(0, input.y));
        if (!zeroInput || inputLagTimer >= _inputLagPeriod) {
            lastInput = input;
            inputLagTimer = 0;
        }

        return lastInput;
    }

    private float ClampVerticalAngle(float value) {
        return Mathf.Clamp(value, -_maxVerticalAngle, _maxVerticalAngle);
    }

    private void Update() {
        Vector2 wantedVelocity = GetInput() * _sensitivity;

        velocity = new Vector2(
            Mathf.MoveTowards(velocity.x, wantedVelocity.x, _acceleration.x ),
            Mathf.MoveTowards(velocity.y, wantedVelocity.y, _acceleration.y ));
        rotation += velocity ;
        rotation.y = ClampVerticalAngle(rotation.y);

        if (_playerMove.isPlayerMovement) {
            transform.localEulerAngles = new Vector3(rotation.y, 0, 0);
            _playerMove.transform.localEulerAngles = new Vector3(0, rotation.x, 0);
        }

    }
    
}
