using Fusion;
using TMPro;
using UnityEngine;
using System.Collections;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerHP _playerHP;

    [SerializeField] private float _playerSpeed = 2f;
    [SerializeField] private float _rotationSpeed = 2f;
    
    private const string _runningState = "IsRunning";
    [Networked] private bool IsRunning { get; set; }
    private GameController _gameController;

    private void Start()
    {
        _gameController = FindAnyObjectByType<GameController>();
    }

    public override void FixedUpdateNetwork()
    {
        if (_gameController.GameContinue) 
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 move = (transform.forward * vertical + transform.right * horizontal).normalized * _playerSpeed * Runner.DeltaTime;

            _controller.Move(move);

            bool isMoving = move != Vector3.zero;
            if (isMoving)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Runner.DeltaTime);
            }
            IsRunning = isMoving;
        }
    }

    public override void Render()
    {
        _animator.SetBool(_runningState, IsRunning);
    }

}