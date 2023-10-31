using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraS : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;

    public float playerDistance;

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, playerDistance, player.transform.position.z);
    }
}
