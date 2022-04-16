using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private GameObject _Target;
    private Vector3 _Direction;
    private float _AngularVelocity;

    private void Start()
    {
        _Direction = Vector3.zero;
        _AngularVelocity = 25f;
    }

    public void NewTarget(GameObject Target)
    {
        _Target = Target;
    }

    private void Update()
    {
        if (_Target != null)
            _Direction = transform.position - _Target.transform.position;

        Quaternion _CurrentRotation = Quaternion.LookRotation(_Direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, _CurrentRotation, _AngularVelocity * Time.deltaTime);
    }
}
