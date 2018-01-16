using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput_ : MonoBehaviour {
	private AudioSource audio_;	
	public float sensitivity = 100;
	public float loudness = 10; 
	public int SampleCount = 12;
	void Start() {
		audio_ = GetComponent<AudioSource>();	
		audio_.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
		audio_.loop = true;
		audio_.mute = false; //true if we don't want the player to hear it
		audio_.Play();
	}

	void Update(){
		loudness = GetAveragedVolume () * sensitivity;
		Debug.Log (loudness);
	}
	//averaging function removes any pops or hiccups in the recording process
	public float GetAveragedVolume(){
		float[] data = new float[SampleCount];
		audio_.GetOutputData (data, 0);
		//average is a loop and a division: 
		float a = 0;
		foreach(float s in data)
			a += Mathf.Abs(s);
		return a/SampleCount; 
	}
}
