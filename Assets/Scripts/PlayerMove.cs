using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour {

    [SerializeField] private float _speed = 3;
    [SerializeField] private float _rotationSpeed = 400;
    [SerializeField] private float _sprintingMultiplier = 1; 
    [SerializeField] private Camera _spectatorCamera;

    private Rigidbody _rigidbody;
    private bool _isPlayerMovement = false;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private Vector2 GetInput() {
        Vector2 input = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));
        return input;
    }

    private void ExitMenu() {
        _isPlayerMovement = true;
        _spectatorCamera.enabled = false;
        MouseLook[] mouseLooks = GetComponentsInChildren<MouseLook>();
        mouseLooks[0].enabled = true;
        mouseLooks[1].enabled = true;
    }

    void Update() {

        if (!_isPlayerMovement && Input.GetKeyDown(KeyCode.Space)) ExitMenu();

        if (Input.GetKeyDown(KeyCode.LeftShift)) _sprintingMultiplier = 3f;
        else _sprintingMultiplier = 1f; 

        Vector3 wantedVelocity = GetInput() * _speed * Time.deltaTime * _sprintingMultiplier;

        _rigidbody.velocity = wantedVelocity;
    }
}
