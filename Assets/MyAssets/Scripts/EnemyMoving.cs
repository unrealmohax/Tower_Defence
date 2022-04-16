using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour
{
    [SerializeField] private float _Speed;

    [SerializeField] private Animator _Anim;

    [SerializeField] private Transform[] _Point;
    [SerializeField] private Transform _CurrentPoint;

    [SerializeField] private float _AngularVelocity;
    [SerializeField] private Vector3 _Direction;

    [SerializeField] private int _Index;

    void Start()
    {
        _Index = 0;
        _Speed = 2f;
        _AngularVelocity = 10;

        _Point = GameObject.Find("Points").GetComponentsInChildren<Transform>();
        _Anim = GetComponent<Animator>();
        _Anim.SetBool("Run", false);

        _CurrentPoint = _Point[_Index];
        _Direction = Vector3.zero;

        _Direction = _Point[_Index].position - transform.position;
        transform.rotation = Quaternion.LookRotation(_Direction);
    }

    public float DistanceToFinish()
    {
        float distance = 0;
        if (TryNextIndex())
            distance += Vector2.Distance(transform.position, _Point[_Index + 1].transform.position);
        for (int i = _Index + 1; i < _Point.Length - 2; i++)
        {
            Vector3 startPosition = _Point[i].transform.position;
            Vector3 endPosition = _Point[i + 1].transform.position;
            distance += Vector2.Distance(startPosition, endPosition);
        }
        return distance;
    }

    private bool TryNextIndex() 
    {
        return (_Index + 1 != _Point.Length);
    }

    private void Move()
    {
        if (transform.position == _CurrentPoint.position)
        {
            if (TryNextIndex())
                _Index++;

            _CurrentPoint = _Point[_Index];
        }

        transform.position = Vector3.MoveTowards(transform.position, _CurrentPoint.position, _Speed * Time.deltaTime);
    }

    private void Rotation()
    {
        if (Vector3.Distance(transform.position, _CurrentPoint.position) < 0.3f)
        {
            if (TryNextIndex())
                _Direction = _Point[_Index + 1].position - transform.position;

            Quaternion _CurrentRotation = Quaternion.LookRotation(_Direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, _CurrentRotation, _AngularVelocity * Time.deltaTime);
        }
    }

    private bool Running() 
    {
        return _Speed != 0;
    }

    private void Update()
    {
        _Anim.SetBool("Run", Running());

        Move();
        Rotation();
    }
}
