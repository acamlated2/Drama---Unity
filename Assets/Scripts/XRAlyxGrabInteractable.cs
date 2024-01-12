using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRAlyxGrabInteractable : XRGrabInteractable
{
    [SerializeField] private float velocityThreshold = 2;

    [SerializeField] private float jumpAngleInDegree = 60;
    
    private XRRayInteractor _rayInteractor;
    private Vector3 previousPos;

    private Rigidbody _interactableRigidbody;

    private bool _canJump = true;

    [SerializeField] private ParticleSystem breakingSystem;

    [SerializeField] private GameObject spawnPointPrefab;
    private GameObject _spawnPoint;
    
    protected override void Awake()
    {
        base.Awake();
        _interactableRigidbody = GetComponent<Rigidbody>();

        GameObject newSpawnPoint = Instantiate(spawnPointPrefab, transform.position, Quaternion.identity);
        _spawnPoint = newSpawnPoint;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _spawnPoint.GetComponent<VaseSpawnPointScript>().StartTimer();
    }

    private void Update()
    {
        if (isSelected && firstInteractorSelecting is XRRayInteractor && _canJump)
        {
            Vector3 velocity = (_rayInteractor.transform.position - previousPos) / Time.deltaTime;
            previousPos = _rayInteractor.transform.position;

            if (velocity.magnitude > velocityThreshold)
            {
                Drop();
                _interactableRigidbody.velocity = ComputeVelocity() / 2;
                _canJump = false;
            }
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRRayInteractor)
        {
            trackPosition = false;
            trackRotation = false;
            throwOnDetach = false;

            _rayInteractor = (XRRayInteractor)args.interactorObject;
            previousPos = _rayInteractor.transform.position;
            _canJump = true;
        }
        else
        {
            trackPosition = true;
            trackRotation = true;
            throwOnDetach = true;
        }
        
        base.OnSelectEntered(args);
    }

    public Vector3 ComputeVelocity()
    {
        Vector3 diff = _rayInteractor.transform.position - transform.position;
        Vector3 diffXZ = new Vector3(diff.x, 0, diff.z);
        float diffXZLength = diffXZ.magnitude;
        float diffYLength = diff.y;

        float angleInRadian = jumpAngleInDegree * Mathf.Deg2Rad;

        float jumpSpeed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(diffXZLength, 2) /
                                     (2 * Mathf.Cos(angleInRadian) * Mathf.Cos(angleInRadian) *
                                      (diffXZ.magnitude * Mathf.Tan(angleInRadian) - diffYLength)));

        Vector3 jumpVelocityVector = diffXZ.normalized * Mathf.Cos(angleInRadian) * jumpSpeed + Vector3.up *
                                     Mathf.Sin(angleInRadian) * jumpSpeed;

        return jumpVelocityVector;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
        
        // spawn particles
        Vector3 pos = transform.position;
        pos.y += 1;
        ParticleSystem system = Instantiate(breakingSystem, pos, transform.rotation);
        
        // lure
        GameObject medea = GameObject.FindGameObjectWithTag("Medea");
        medea.GetComponent<PlayerController>().Lure(transform.position);
    }
}
