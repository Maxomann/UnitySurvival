using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

    int i = 0;
    private Transform cameraRef;

	// Use this for initialization
	void Start () {
        cameraRef = GameObject.Find("Main Camera").transform;
	}
	
	// Update is called once per frame
	void Update () {
        i++;
        cameraRef.position.Set(i, 0, 0);
	}
}
