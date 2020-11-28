using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour {
    public Player player;
    public Vector3 offset;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        offset = this.gameObject.transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position = offset + player.transform.position;	
	}
}
