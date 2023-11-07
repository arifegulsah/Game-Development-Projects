using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    private float _horizontalMove;
    private float _verticalMove;
    [SerializeField] float speed;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        _horizontalMove = movementVector.x;
        _verticalMove = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 playerMovement = new Vector3(_horizontalMove,0,_verticalMove); //0 ile zýpplanmýyor

        _rb.AddForce(playerMovement*speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
