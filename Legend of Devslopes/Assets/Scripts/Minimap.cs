using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    [SerializeField] Transform player;

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        transform.position = new Vector3(newPosition.x, transform.position.y, newPosition.z);
    }

}
