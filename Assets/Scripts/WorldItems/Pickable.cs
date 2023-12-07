using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public bool picked;

    private GameObject player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    private void Update()
    {
        if (picked) {
            transform.position = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y, player.gameObject.transform.position.z + 1f);
        }
    }
}
