using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] public int Hearth { get; private set; } = 5;
    [SerializeField] public int Gold { get; private set; } = 100;

    private void Start()
    {
        Events.LossHearth.AddListener(LossHearth);
        Events.KillEnemy.AddListener(KillEnemy);
        Events.CreateTower.AddListener(CreateTower);
    }

    private void LossHearth() 
    {
        Hearth--;
        if (Hearth <= 0)
            Events.Defeat?.Invoke();
    }

    private void KillEnemy(GameObject Enemy) 
    {
        Gold += Enemy.GetComponent<Enemy>().Reward;
    }

    private void CreateTower(int Cost)
    {
        Gold -= Cost;
    }
}
