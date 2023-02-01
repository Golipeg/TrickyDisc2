using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent _enemyDestroyed;
    [SerializeField] private UnityEvent _playerGameStartPoint;
    [SerializeField] private UnityEvent _playerDied;
    [SerializeField] private ParticleSystem _playerDeathParticlesPrefab;
    [SerializeField] private ParticleSystem _enemyDeathParticlesPrefab;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag(GlobalConstants.BORDER_TAG))
        {
            _playerDied?.Invoke();
        }

        if (collider.TryGetComponent(out EnemyController enemyController))
        {
            enemyController.Destroy();
            Instantiate(_enemyDeathParticlesPrefab, transform.position, Quaternion.identity);
            _enemyDestroyed?.Invoke();
        }

        if (collider.CompareTag(GlobalConstants.PLAYER_START_POINT_TAG))
        {
            _playerGameStartPoint?.Invoke();
        }
    }

    [UsedImplicitly]
    public void OnPlayerDied()
    {
        Instantiate(_playerDeathParticlesPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}