using UnityEngine;

public class AttackEnemies : MonoBehaviour
{
    private Tower _Tower;
    private LookAtTarget _LookAtTarget;

    [SerializeField] private GameObject _Bullets;

    [SerializeField] private int _Price;
    [SerializeField] private int _FiringRate; 

    private float _Delay; 

    private void Start()
    {
        _Delay = 1 / _FiringRate;
        _Tower = GetComponent<Tower>();
    }

    private void Update()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
            if (child.tag == "Turell")
                 _LookAtTarget = child.GetComponent<LookAtTarget>();

        _Delay -= Time.deltaTime;
        GameObject target = null;
        
        float minEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in _Tower.EnemiesInRange)
        {
            float distanceToFinish = enemy.GetComponent<EnemyMoving>().DistanceToFinish();
            if (distanceToFinish < minEnemyDistance)
            {
                target = enemy;
                minEnemyDistance = distanceToFinish;
            }
        }
        
        if (target != null)
        {
            if (_Delay <= 0)
            {
                _Delay = 1 / _FiringRate;
                Shoot(target.GetComponent<Collider>());
            }
        }
    }

    private void Shoot(Collider target)
    {
        GameObject newBullet = Instantiate(_Bullets, transform.position + new Vector3( 0 , 1.2f , 0), Quaternion.identity);
        Bullet _Bullet = newBullet.GetComponent<Bullet>();

        _Bullet.Target = target.gameObject;
        _LookAtTarget.NewTarget(target.gameObject);
    }

    
}
