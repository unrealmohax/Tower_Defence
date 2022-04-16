using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _TimeLife;
    [SerializeField] private float _Speed;
    [SerializeField] private int _Damage;

    public GameObject Target;

    private void Start()
    {
         _Speed = 10f;
         _TimeLife = 5f;
    }

    private void Update()
    {
        _TimeLife -= Time.deltaTime;
        if (_TimeLife <= 0)
            Destroy(gameObject);

        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position + new Vector3(0 , 0.25f , 0), Time.deltaTime * _Speed );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

}
