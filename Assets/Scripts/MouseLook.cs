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
    private Vector2 lastInput;
    private float inputLagTimer;


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

        if(lockHorizontalRotation) wantedVelocity.x = 0;
        else wantedVelocity.y = 0;

        velocity = new Vector2(
            Mathf.MoveTowards(velocity.x, wantedVelocity.x, _acceleration.x * Time.deltaTime),
            Mathf.MoveTowards(velocity.y, wantedVelocity.y, _acceleration.y * Time.deltaTime));
        rotation += velocity * Time.deltaTime;
        rotation.y = ClampVerticalAngle(rotation.y);

        transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);
    }

}
