using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	static AudioManager _instance;
	static AudioManager instance {
		get {
			if (_instance == null) {
				_instance = new GameObject("AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
			}
			return _instance;
		}
	}

	public Dictionary<string, AudioSource> uniqueSources = new Dictionary<string, AudioSource>();



	public static void PlayUniqueLooping(string soundName, string tag, float volume = 1f) {
		instance.instancePlayLooping(soundName, tag, volume);
	}

	private void instancePlayLooping(string soundName, string tag, float volume) {
		AudioSource source;
	if(!uniqueSources.TryGetValue(tag, out source)){
			source = gameObject.AddComponent<AudioSource>();
			uniqueSources[tag] = source;
		}
		source.Stop();
		source.loop = true;
		source.clip = Resources.Load<AudioClip>("Sounds/" + soundName);
		source.volume = volume;
		source.Play();
	}

	private static List<AudioSource> availableSources = new List<AudioSource>();

	public static void PlaySound(string soundName, float volume) {
		AudioSource source;
		int x = 0;
		do {
			if (x >= availableSources.Count) {
				source = instance.gameObject.AddComponent<AudioSource>();
			} else {
				source = availableSources[x];
			}
			x++;
		} while (!source.isPlaying);
		source.loop = false;
		source.clip = Resources.Load<AudioClip>("Sounds/" + soundName);
		source.volume = volume;
		source.Play();
	}

}