using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour {

    private Transform target;

	// Use this for initialization
	void Start () {
        target = transform.parent;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = (new Vector3(target.position.x, transform.position.y, target.position.z));
	}
}
