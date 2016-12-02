using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Music : MonoBehaviour {

    AudioSource music;

	// Use this for initialization
	void Start () {
        music = GetComponent<AudioSource>();

        
	}
	
	// Update is called once per frame
	void Update () {
	    if(PlayerPrefs.GetInt("music") == 1)
        {
            music.enabled = true;
        }
        else
        {
            music.enabled = false;
        }
	}
}
