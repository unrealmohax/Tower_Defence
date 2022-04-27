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

    [SerializeField] private List<GameObject> Enemies = new List<GameObject>();
    public int EnemyDie { get { return _SummEnemyDie; } }
    private int _SummEnemyDie;

    [SerializeField] private bool IsNotWin = true;
    private int _Count;

    [SerializeField] private float _TimeToSpawn;
    private float _CurrentTime;

    private void Start()
    {
        Events.KillEnemy.AddListener(DieEnemy);

        _CurrentTime = 0;
        _TimeToSpawn = 2f;

        _EnemyToWave = 5;
        _SummEnemyDie = _Count = 0;
        _EnemyCount = 10 + _EnemyToWave * _Wave;

        _Wave = 0;
    }

    public void DieEnemy(GameObject enemy)
    {
        _SummEnemyDie++;
        Enemies.Remove(enemy);

        if (Enemies.Count == 0)
            WaveComplited();
        
    }

    private void WaveComplited()
    {
        _Count = 0;
        Events.WaveComplited?.Invoke();
        if (_Wave != _MaxWave - 1)
        {
            _Wave++;
            _EnemyCount = 10 + _EnemyToWave * _Wave;
        }
        else 
        {
            IsNotWin = false;
            Events.Victory?.Invoke();
        } 
    }

    private void Update()
    {
        if (IsNotWin)
        if (_Count != _EnemyCount)
        {
            _CurrentTime += Time.deltaTime;
            if (_CurrentTime >= _TimeToSpawn)
            {
                    _Count++;
                    _CurrentTime = 0;

                Events.SpawnEnemy?.Invoke();
                GameObject Enemy = Instantiate(_Enemy, transform.position, Quaternion.identity);
                Enemies.Add(Enemy);
            }
        }
    }

}
