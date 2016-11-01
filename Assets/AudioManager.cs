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
        
        /*do//TODO: figure out why this is broken and fix it. This will work but it's NOT GOOD CODE (also it leaks objects CONSTANtLY)
        {
            if (x >= availableSources.Count)
            {
                source = instance.gameObject.AddComponent<AudioSource>();
                source.Stop();
                availableSources.Add(source);
            }
            else
            {
                source = availableSources[x];
            }
            x++;
            Debug.Log("lol " + x.ToString());
        } while (!source.isPlaying||x>10);*/
        
        source = instance.gameObject.AddComponent<AudioSource>();
        //DEBUG
		source.loop = false;
		source.clip = Resources.Load<AudioClip>("Sounds/" + soundName);
        if(source.clip== null) { throw new UnityException("Could not find sound "+soundName); }
		source.volume = volume;
		source.Play();
	}

}