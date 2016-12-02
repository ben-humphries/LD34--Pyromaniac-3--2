using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LockLevels : MonoBehaviour {

    private GameObject[] levels;
    private Button[] buttons;

	// Use this for initialization
	void Start () {

        PlayerPrefs.SetInt("1", 1);

        buttons = GetComponentsInChildren<Button>();

        levels = new GameObject[buttons.Length];

        for(int i = 0; i < buttons.Length; i++)
        {
            levels[i] = buttons[i].gameObject;
        }

        for(int i = 0; i < levels.Length; i++)
        {
            if(PlayerPrefs.GetInt((i+1).ToString()) == 1)
            {
                levels[i].SetActive(true);
            }
            else
            {
                levels[i].SetActive(false);
            }
        }
	}
}
