using UnityEngine;
using UnityEngine.Events;

public class Events : MonoBehaviour
{
    public static UnityEvent SpawnEnemy = new UnityEvent();
    public static UnityEvent KillEnemy = new UnityEvent();
    public static UnityEvent WaveComplited = new UnityEvent();
    public static UnityEvent Victory = new UnityEvent();
}

