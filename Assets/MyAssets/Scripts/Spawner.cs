using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _Enemy;

    private const int _MaxWave = 10;

    public int LeftEnemy { get { return _EnemyCount - _Count; } }
    public int Wave {get { return _Wave + 1; } }
    [SerializeField] private int _Wave;

    [SerializeField] private int _EnemyToWave;
    [SerializeField] private int _EnemyCount;
    [SerializeField] private int _EnemyDie;

    public int EnemyDie { get { return _SummEnemyDie; } }
    private int _SummEnemyDie;


    private int _Count;

    [SerializeField] private float _TimeToSpawn;
    private float _CurrentTime;

    private void Start()
    {
        Events.KillEnemy.AddListener(DieEnemy);

        _CurrentTime = 0;
        _TimeToSpawn = 2f;

        _EnemyToWave = 5;
        _SummEnemyDie = _EnemyDie = _Count = 0;
        _EnemyCount = 10 + _EnemyToWave * _Wave;

        _Wave = 0;
    }

    private void DieEnemy()
    {
        _EnemyDie++;
        _SummEnemyDie++;

        if (_EnemyDie == _EnemyCount)
        {
            _EnemyDie = 0;
            WaveComplited();
        } 
    }

    private void WaveComplited()
    {
        _Count = 0;
        Events.WaveComplited?.Invoke();
        if (_Wave != _MaxWave)
        {
            _Wave++;
            _EnemyCount = 10 + _EnemyToWave * _Wave;
        }
            
        else
            Events.Victory?.Invoke();
    }

    private void Update()
    {
        if (_Count != _EnemyCount)
        {
            _CurrentTime += Time.deltaTime;
            if (_CurrentTime >= _TimeToSpawn)
            {
                _CurrentTime = 0;
                _Count++;

                Events.SpawnEnemy?.Invoke();
                Instantiate(_Enemy, transform.position, Quaternion.identity);
            }
        }
    }

}
