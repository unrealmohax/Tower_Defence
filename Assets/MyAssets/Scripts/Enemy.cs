using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Spawner _Spawner;
    private UI _UI;

    public float MaXHealth { get { return _MaXHeath; } }
    [SerializeField] private float _MaXHeath;

    public float Health { get { return _Health; } }
    [SerializeField] private float _Health;

    private int Difficulty = 1;

    private bool First = true;

    public int Reward { get; private set; } = 10;

    private void Start()
    {
        _UI = GameObject.Find("Canvas").GetComponent<UI>();
        _Spawner = GameObject.Find("SpawnerEnemy").GetComponent<Spawner>();

        if (_UI.Lvl_status == "Easy")
            Difficulty = 0;
        else if (_UI.Lvl_status == "Medium")
            Difficulty = 1;
        else if(_UI.Lvl_status == "Hard")
            Difficulty = 2;

        _Health = _MaXHeath = 50 + 50 * Difficulty + 10 * _Spawner.Wave; 
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "Bullet")
        {
            _Health -= 25f;
        }
            
    }

    private void Update()
    {
        if(_Health <= 0 && First)
        {
            Events.KillEnemy?.Invoke(gameObject);
            Destroy(gameObject);
        }
            
    }
}
