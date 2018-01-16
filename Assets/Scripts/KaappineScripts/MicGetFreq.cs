using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MicGetFreq : MonoBehaviour {
	private AudioSource audio_;	
	public float sensitivity = 100;
	public float loudness = 10; 
	public int sampleCount = 12;
	public int sampleRate = 11024;
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
		float[] data = new float[sampleCount];
		audio_.GetOutputData (data, 0);
		//average is a loop and a division: 
		float a = 0;
		foreach(float s in data)
			a += Mathf.Abs(s);
		return a/sampleCount; 
	}

	public float GetFrequency(){
		float fundamentallFrequency = 0.0f;
		float[] data = new float[8192];
		audio_.GetSpectrumData (data, 0, FFTWindow.BlackmanHarris);
		float s = 0.0f;
		int i = 0;
		for (int n = 0; n < 8192; n++) {
			if (s < data [n]) {
				s = data [n]; 
				i = n;
			}
		}
		fundamentallFrequency = i * sampleCount / 8192;
		return fundamentallFrequency;
	}
}
