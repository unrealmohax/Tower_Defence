using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    public static UnityEvent SpawnEnemy = new UnityEvent();
    public static UnityEvent<GameObject> KillEnemy = new UnityEvent<GameObject>();
    public static UnityEvent WaveComplited = new UnityEvent();
    public static UnityEvent Victory = new UnityEvent();
    public static UnityEvent LossHearth = new UnityEvent();
    public static UnityEvent Defeat = new UnityEvent();
    public static UnityEvent<int> CreateTower = new UnityEvent<int>();
}

