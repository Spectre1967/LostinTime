using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   

public class KattMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Camera _camera;

    private Ray _ray;
    private RaycastHit _hit;
   
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(_ray, out _hit, 1000f))
            {
                agent.destination = _hit.point;

            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            agent.SetDestination(player.position);
        }
    }
}
