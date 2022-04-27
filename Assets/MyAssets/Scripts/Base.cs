using UnityEngine;

public class Base : MonoBehaviour
{
    private Spawner _Spawner;

    private void Start()
    {
        _Spawner = FindObjectOfType<Spawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") 
        {
            _Spawner.DieEnemy(other.gameObject);
            Destroy(other.gameObject);
            Events.LossHearth?.Invoke();
        }
            
    }
}
