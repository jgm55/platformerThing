using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerPos = player.transform.position;
        this.transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
	}
}
