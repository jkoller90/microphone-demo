using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnByLoudness : MonoBehaviour {
	public GameObject audioInputObject;
	public GameObject objectToSpawn;
	public float threshold = 1.0f;
	MicrophoneInput_ micIn;

	void Start () {
		if(objectToSpawn == null)
			Debug.LogError ("You need to set a prefab to Object To Spawn -parameter in the editor!");
		if (audioInputObject == null)
			audioInputObject = GameObject.Find ("AudioInputObject"); 
	    micIn = (MicrophoneInput_)audioInputObject.GetComponent ("MicrophoneInput_");
	}

	void Update () {

		float l = micIn.loudness;
		if (l > threshold) {
			Vector3 scale = new Vector3 (l, l, l);
			GameObject newObject = (GameObject)Instantiate (objectToSpawn, this.transform.position, Quaternion.identity);
			newObject.transform.localScale += scale;
		}
	}
}
