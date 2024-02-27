using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   

public class KattMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public Camera _camera;
    public Transform locationMarker;
    public Renderer markerRenderer;
    [Space]
    public Color validColor;
    public Color invalidColor;
    [Space]
    public float maxDistance = 100;

    private Ray _ray;
    private RaycastHit _hit;

    public static bool followingPlayer = true;
    bool holdingLMB = false;

    private void Start()
    {
        agent.destination = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            holdingLMB = true;
        }

        if(Input.GetMouseButtonUp(0) && holdingLMB)
        {
            holdingLMB = false;
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _hit, maxDistance))
            {
                followingPlayer = false;
                agent.destination = _hit.point;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            followingPlayer = true;
        }

        if (holdingLMB)
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            bool raycast = Physics.Raycast(_ray, out _hit, maxDistance);
            if (raycast)
            {
                locationMarker.position = _hit.point;
                markerRenderer.material.color = validColor;
            }
            else
            {
                locationMarker.position = _ray.GetPoint(maxDistance);
                markerRenderer.material.color = invalidColor;
            }

            locationMarker.gameObject.SetActive(true);
        }
        else
        {
            locationMarker.gameObject.SetActive(false);
        }

        if (followingPlayer)
        {
            agent.destination = player.position;
        }
    }
}
