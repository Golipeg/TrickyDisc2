using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _movementVelocity;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private SpriteRenderer _aimSprite;
    [SerializeField] private PlayerRotator _playerRotator;
    [SerializeField] private UserMoveTimeLimiter _timeLimiter;
    [SerializeField] private AudioSource _moveAudioClip;
    
    private bool _isMoving;

    private void Awake()
    {
        _startPosition = transform.position;
        _isMoving = true;
    }

    [UsedImplicitly]
    public void Move()
    {
        if (_isMoving)
        {
            _aimSprite.enabled = false;
            _isMoving = !_isMoving;
            _playerRotator.StopRotate();
            _timeLimiter.StopTimeLimiter();
            _rigidbody.velocity = transform.up * _movementVelocity;
            _moveAudioClip.Play();
        }
    }

    [UsedImplicitly] // вызываем через ивент при колизии игрока с врагом
    public void ChangeDirection()
    {
        _rigidbody.velocity *= -1;
    }

    [UsedImplicitly]
    public void ResetPosition() // вызывается через ивент при возвращении игрока в старт поинт триггер
    {
        if (!_isMoving)
        {
            _isMoving = !_isMoving;
            _aimSprite.enabled = true;
            _playerRotator.StartRotate();
            _timeLimiter.RestartTimeLimiter();
            _rigidbody.velocity = Vector2.zero;
            transform.position = _startPosition;
        }
    }
}