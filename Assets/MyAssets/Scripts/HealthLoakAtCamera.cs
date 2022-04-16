using UnityEngine;

public class HealthLoakAtCamera : MonoBehaviour
{
    private Transform _Target;

    private void Start()
    {
        _Target = GameObject.Find("ProgressBarTarget").transform;
    }

    
    private void Update()
    {
        transform.LookAt(_Target);
    }
}
