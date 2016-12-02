using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

    private GameObject[] objects;
    private ArrayList flammables;

    public Canvas winScreen;

	// Use this for initialization
	void Start () {
        

        objects = GameObject.FindGameObjectsWithTag("Flammable");

        flammables = new ArrayList();

        flammables.AddRange(objects);
        winScreen.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	    foreach(GameObject flammable in flammables)
        {
            //print("here");
            if(flammable.GetComponent<Tree>().spriteAlpha <= .1)
            {
                flammables.Remove(flammable);
               
            }
        }


        if(flammables.Count == 0)
        {
            if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                
                winScreen.gameObject.SetActive(true);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().enabled = false;

                GameObject.Find("Top Camera").GetComponent<Camera>().orthographicSize = Mathf.Lerp(GameObject.Find("Top Camera").GetComponent<Camera>().orthographicSize, 20f, .1f);

                PlayerPrefs.SetInt((Application.loadedLevel - 3 + 1 + 1).ToString(), 1);
            }
        }
	}
}
