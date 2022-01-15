using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float speed = 3f;
    
    [SerializeField] [Tooltip("This will tell if the movement should be relative to player's rotation")] 
    private bool relativeMove = false;

    private Rigidbody2D _rigidbody;
    private Vector2 _movementVector;
    private Vector2 _lookDirection;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetRotation();
        GetMovement();
    }

    private void FixedUpdate()
    {
        ApplyRotation();
        if(relativeMove)
            ApplyRelativeMovement(_lookDirection);
        else
            ApplySimpleMovement();
    }

    private void GetRotation()
    {
        Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _lookDirection = worldMousePosition - _rigidbody.position;
    }

    private void GetMovement()
    {
        _movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void ApplyRotation()
    {
        float angle = Mathf.Atan2(_lookDirection.y, _lookDirection.x) * Mathf.Rad2Deg - 90f;
        _rigidbody.rotation = angle;
    }

    private void ApplyRelativeMovement(Vector2 lookDirection)
    {
        if (_movementVector.magnitude > 0)
        {
            Vector2 sideDirection = (Quaternion.AngleAxis(90, Vector3.forward) * lookDirection) * _movementVector.x;
            Vector2 frontDirection = (lookDirection * _movementVector.y);
            Vector2 relativeMovDirection = frontDirection + sideDirection;
                                           
            Vector2 newPosition = _rigidbody.position + relativeMovDirection.normalized * (speed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPosition);
        }
    }

    private void ApplySimpleMovement()
    {
        if (_movementVector.magnitude > 0)
        {
            Vector2 newPosition = _rigidbody.position + _movementVector * (speed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(newPosition);
        }
    }
}
