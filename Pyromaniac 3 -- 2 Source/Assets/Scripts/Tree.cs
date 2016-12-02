using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Tree : MonoBehaviour {

    SpriteRenderer renderer;
    [HideInInspector]
    public float spriteAlpha;

    private float currentVelocity = 0.0F;
    public float fadeTime = 1f;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();
        spriteAlpha = 1f;
	}
	
	// Update is called once per frame
	void Update () {
	
        foreach(GameObject fireSystem in PlayerMovement.fireSystems)
        {
            if (Mathf.Abs(fireSystem.transform.position.x - transform.position.x) < 5 && Mathf.Abs(fireSystem.transform.position.y - transform.position.y) < 10 && Mathf.Abs(fireSystem.transform.position.z - transform.position.z) < 5)
            {
                spriteAlpha = Mathf.SmoothDamp(spriteAlpha, 0f, ref currentVelocity, fadeTime);
                renderer.color = new Color(spriteAlpha, spriteAlpha, spriteAlpha, 1f);
            }
        }
	}
}
