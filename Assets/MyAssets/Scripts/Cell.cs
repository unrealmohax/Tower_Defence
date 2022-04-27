using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Material MainMaterial;
    public Material CanBuildMaterial;
    public Material CanUpgradeMaterial;
    public Material CantMaterial;
    public bool CanBuild;
    public bool CanUpgrade = false;

    public GameObject TowerPrefab;

    private ResourceManager _ResourceManager;
    private Tower _Tower;

    [SerializeField] private GameObject _TowerBuild;
    void Start()
    {
        _ResourceManager = FindObjectOfType<ResourceManager>();
        _Tower = TowerPrefab.GetComponent<Tower>();
    }

    private void OnMouseUp()
    {
        if (_TowerBuild == null) { 
            if (CanBuild && _ResourceManager.Gold >= _Tower.Cost)
            {
                Events.CreateTower?.Invoke(_Tower.Cost);
                _TowerBuild = Instantiate(TowerPrefab, transform.position, Quaternion.identity);
                _Tower = _TowerBuild.GetComponent<Tower>();
                CanBuild = false;
                CanUpgrade = true;
            } 
        }
        else
            if (CanUpgrade && _ResourceManager.Gold >= _Tower.Cost) 
            {
                Events.CreateTower?.Invoke(_Tower.Cost);
                _Tower.Upgrade();
                print("Апгрейт");
            }
                
    }

    private void OnMouseOver()
    {
        if (CanBuild && _ResourceManager.Gold >= _Tower.Cost)
            GetComponent<Renderer>().material = CanBuildMaterial;
        else if (CanUpgrade && _ResourceManager.Gold >= _Tower.Cost)
            GetComponent<Renderer>().material = CanUpgradeMaterial;
        else
            GetComponent<Renderer>().material = CantMaterial;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material = MainMaterial;
    }
}
