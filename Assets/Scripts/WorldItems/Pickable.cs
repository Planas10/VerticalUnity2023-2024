using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public bool picked;

    private PlayerMovement player;

    private void Update()
    {
        if (picked) {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 1f);
        }
    }
}
