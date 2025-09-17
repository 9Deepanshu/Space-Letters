using UnityEngine;
using System.Collections;

public class RotateSkyBox : MonoBehaviour {

	public Camera SkyCamera;
	
	public Vector3 SkyBoxRotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		SkyCamera.transform.Rotate(SkyBoxRotation * Time.deltaTime);
	}
}
