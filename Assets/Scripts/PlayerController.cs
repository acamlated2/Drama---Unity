using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _agent;

    [SerializeField] private float timerMax = 2;
    private float _timer = 5;

    private GameObject _returnPos;

    [SerializeField] private float lureDistance = 20;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        
        _returnPos = GameObject.FindGameObjectWithTag("ReturnPosition");
    }

    public void Lure(Vector3 pos)
    {
        float distance = Vector3.Distance(transform.position, pos);
        if (distance <= lureDistance)
        {
            _agent.SetDestination(pos);
            _timer = timerMax;
        }
        
        Debug.Log(distance);
    }

    private void Update()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance && _agent.remainingDistance != Mathf.Infinity)
        {
            _timer -= 1 * Time.deltaTime;

            if (_timer <= 0)
            {
                _agent.SetDestination(_returnPos.transform.position);

                _timer = timerMax;
            }
        }
    }
}
