using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] ParticleSystem particulasCursor;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
                PartCursor(hit.point);
            }
        }
    }

    void PartCursor(Vector3 pos)
    {
        ParticleSystem part = Instantiate(particulasCursor);
        part.transform.position = pos;
        pos.y += 1f;
        part.Play();
        Destroy(part, part.main.duration);
    }

}
