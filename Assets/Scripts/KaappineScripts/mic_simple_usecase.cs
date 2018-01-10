using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class mic_simple_usecase : MonoBehaviour {

	private AudioSource audio_;	
	public float sensitivity = 100;
	public float loudness = 0; 

	void Start() {
		audio_ = GetComponent<AudioSource>();	
		audio_.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
		audio_.loop = true;
		audio_.mute = true; //true if we don't want the player to hear it
		audio_.Play();
	}

	void Update(){
		loudness = GetAveragedVolume () * sensitivity;
	}
	//averaging function removes any pops or hiccups in the recording process
	public float GetAveragedVolume(){
		float[] data = new float[256];
		audio_.GetOutputData (data, 0);
		//average is a loop and a division: 
		float a = 0;
		foreach(float s in data)
			a += Mathf.Abs(s);
		return a/256; 
	}


}

/*
 * suggested before audio.Play() in Start() is this
while(!(Microphone.GetPosition(AudioInputDevice)>0)){}; // wait til recording has started
	//it's meant to pause the scripting until an audio device is recognized by the system
*/