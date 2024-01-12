using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _agent;

    [SerializeField] private float timerMax = 5;
    private float _timer = 5;

    public GameObject returnPos;
    
    [SerializeField] private ParticleSystem luringParticleSystem;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        
        returnPos = GameObject.FindGameObjectWithTag("ReturnPosition");
    }

    public void Lure(Vector3 pos)
    {
        float distance = Vector3.Distance(transform.position, pos);
        RaycastHit hit;
        Vector3 direction = transform.position - pos;
        
        if (Physics.Raycast(pos, direction, out hit, distance))
        {
            Debug.Log("Raycast hit something: " + hit.transform.name);
        }
        else
        {
            _agent.SetDestination(pos);
            _timer = timerMax;
                
            // particle
            Vector3 particlePos = pos;
            particlePos.y += 1;
            ParticleSystem system =
                Instantiate(luringParticleSystem, particlePos, Quaternion.LookRotation(direction));
        }
    }

    private void Update()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance && _agent.remainingDistance != Mathf.Infinity)
        {
            _timer -= 1 * Time.deltaTime;

            if (_timer <= 0)
            {
                _agent.SetDestination(returnPos.transform.position);

                _timer = timerMax;
            }
        }
    }
}
