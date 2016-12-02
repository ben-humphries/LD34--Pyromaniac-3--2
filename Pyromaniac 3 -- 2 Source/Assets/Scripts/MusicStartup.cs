using UnityEngine;
using System.Collections;

public class MusicStartup : MonoBehaviour {

	void Start () {
        PlayerPrefs.SetInt("music", 1);
    }
	
}
