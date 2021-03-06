using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public List<GameObject> EnemiesInRange;
    public int Cost { get; private set; } = 50;

    [SerializeField] private int Lvl = 1;

    private void Start()
    {
        EnemiesInRange = new List<GameObject>();
    }

    private void OnEnemyDestroy(GameObject enemy)
    {
        EnemiesInRange.Remove(enemy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemiesInRange.Add(other.gameObject);
            EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            EnemiesInRange.Remove(other.gameObject);
            EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }
    
    public void Upgrade()
    {
        Lvl++;
    }
}
