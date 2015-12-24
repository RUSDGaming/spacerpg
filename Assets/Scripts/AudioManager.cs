using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AudioManager: MonoBehaviour {



    public static AudioManager instance;


    List<AudioSource> audioList;
    [SerializeField]    int numAudio = 16;

    AudioClip[] sounds;

    // Use this for initialization
    void Start () {


        audioList = new List<AudioSource>(16);        
        
        for(int i = 0; i< numAudio; i++)
        {
            audioList.Add(gameObject.AddComponent<AudioSource>());
        }

        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public static void playSound(AudioClip clip)
    {
        //Debug.Log(audioList.Count);
        //Debug.Log(audioList[0]);
        playSound(clip, 1, .5f);

    }

    public static void PlaySoundById(int id)
    {
        //if(instance != null)
        //{
        //    playSound(instance.sounds[id], 1, .5f);
        //   // instance.audioList[0].;
        //}
    }

    private static void PlaySoundByName()
    {

    }

    public static void playSound(AudioClip clip, float pitch, float volume)
    {
        var query = from src in instance.audioList where !src.isPlaying select src;
        AudioSource audioSource = query.FirstOrDefault();
        if (audioSource)
        {
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(clip,volume);
        }
        else
        {
            Debug.Log("There were No Audio Sources");
        }
    }

}
