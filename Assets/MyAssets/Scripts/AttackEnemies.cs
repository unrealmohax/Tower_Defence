using UnityEngine;

public class AttackEnemies : MonoBehaviour
{
    private Tower _Tower;

    [SerializeField] private GameObject _Target, _Bullets;

    [SerializeField] private int _Price;
    [SerializeField] private int _FiringRate;
    
    private Vector3 _Direction;

    private Transform _BulletSpawn, _Gun;

    private float _Delay; 

    private void Start()
    {
        _Delay = 1 / _FiringRate;
        _Tower = GetComponent<Tower>();

        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child.name == "BulletSpawn")
                _BulletSpawn = child;
            if (child.name == "Gun")
                _Gun = child;
        }
    }

    private void Update()
    {
        _Delay -= Time.deltaTime;

        FindEnemy();

        if (_Target != null)
        {
            LookAtTarget();
            if (_Delay <= 0)
            {
                _Delay = 1 / _FiringRate;
                Shoot();
            }
        }
    }

    private void FindEnemy() 
    {
        float minEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in _Tower.EnemiesInRange)
        {
            float DistanceToFinish = enemy.GetComponent<EnemyMoving>().DistanceToFinish();
            if (DistanceToFinish < minEnemyDistance)
            {
                _Target = enemy;
                minEnemyDistance = DistanceToFinish;
            }
        }
    }

    private void Shoot ()
    {
        GameObject newBullet = Instantiate(_Bullets, _BulletSpawn.position, Quaternion.identity);
        Bullet _Bullet = newBullet.GetComponent<Bullet>();

        _Bullet.Target = _Target;
    }

    private void LookAtTarget ()
    {
        _Direction = _Target.transform.position;
        _Direction.y = _Gun.transform.position.y;
        _Direction = _Gun.transform.position - _Direction;

        _Gun.transform.rotation = Quaternion.LookRotation(_Direction);
    }
}
