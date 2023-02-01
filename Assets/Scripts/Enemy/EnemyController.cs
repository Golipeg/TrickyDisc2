using System;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _minMovingDuration;
    [SerializeField] private float _maxMovingDuration;
    [SerializeField] private AudioSource _enemyDestroyedSound;
    private float _minPointX;
    private float _maxPointX;
    private SpriteRenderer _enemySprite;
    private float _delayBetweenMovements;
    private Sequence _moveSequence;


    private float GetRandomMovementDuration()
    {
        return Random.Range(_minMovingDuration, _maxMovingDuration);
    }

    private float GetNextRandomPosition()
    {
        return Random.Range(_minPointX, _maxPointX);
    }

    private void Move()
    {
        var randomMovementDuration = GetRandomMovementDuration();
        var nextPosition = GetNextRandomPosition(); 
        _moveSequence = DOTween.Sequence();

        _moveSequence.Append(transform.DOMoveX(nextPosition, randomMovementDuration));
        _moveSequence.AppendInterval(_delayBetweenMovements);
        _moveSequence.OnComplete(Move);
    }

    public void Initialize(float minPointX, float maxPointX, float delayBetweenMovements)
    {
        _enemySprite = GetComponent<SpriteRenderer>();
        var offset = _enemySprite.bounds.size.x / 2;
        _minPointX = minPointX + offset;
        _maxPointX = maxPointX - offset;
        _delayBetweenMovements = delayBetweenMovements;
        Move();
    }

    [UsedImplicitly]
    public void Destroy()
    {
        _enemyDestroyedSound.Play();
        Destroy(gameObject);
        
        
    }

    private void OnDestroy()
    {
        _moveSequence.Kill();
    }
}