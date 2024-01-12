using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class VaseSpawnPointScript : MonoBehaviour
{
    private float _timer = 5;
    private bool _started;
    [SerializeField] private GameObject vasePrefab;
    
    public void StartTimer()
    {
        _started = true;
    }

    private void Update()
    {
        if (_started)
        {
            _timer -= 1 * Time.deltaTime;

            if (_timer <= 0)
            {
                GameObject newVase = Instantiate(vasePrefab, transform.position, quaternion.identity);
                Vector3 rotation = new Vector3(-90, 0, 0);
                newVase.transform.rotation = Quaternion.Euler(rotation);
                
                Destroy(gameObject);
            }
        }
    }
}
