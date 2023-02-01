using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyController _enemyPrefab;
    [SerializeField] private Transform _leftSpawnPoint;
    [SerializeField] private Transform _rightSpawnPoint;
    [SerializeField] float _delayBetweenMovements;
    private float _minPointX;
    private float _maxPointX;

    private void Start()
    {
        Spawn();
    }

    private void Awake()
    {
        var camera=Camera.main;
        _minPointX = camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
        _maxPointX = camera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
    }

    private bool ShouldSpawnOnLeftSide()// метод будет возвращать true or false при выполнении рандома 
    {
        var randomSpawn = Random.Range(0f, 1f);
        return randomSpawn == 1f;
    }
    // будем вызывать данный метод по факту возвращения игрока на стартовую точку
    [UsedImplicitly]
    public void Spawn()
    {
        var spawnPoint = ShouldSpawnOnLeftSide() ? _leftSpawnPoint.position : _rightSpawnPoint.position;
        var currentEnemy = Instantiate(_enemyPrefab, spawnPoint, Quaternion.identity, transform);
        currentEnemy.Initialize(_minPointX,_maxPointX,_delayBetweenMovements);
    }
}
