using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class FireTruck : MonoBehaviour {


    public float timeInBetweenSpawns = 5f;
    private float nextSpawn;

    public GameObject fireFighter;
    private GameObject player;

    public float health = 500f;

    private bool isDead = false;
    private float spriteDarkness = 1f;
    private float spriteAlpha = 1f;

    private SpriteRenderer renderer;
    private Rigidbody rigidbody;

    private float currentVelocity = 0.0F;
    public float fadeTime = 1f;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "FireBall")
        {
            health -= 50;

        }
    }

    // Use this for initialization
    void Start () {
        renderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody>();

        nextSpawn = Time.time + timeInBetweenSpawns;

        player = GameObject.Find("Player");
        
	}
	
	// Update is called once per frame
	void Update () {

        foreach (GameObject fireSystem in PlayerMovement.fireSystems)
        {
            if (Mathf.Abs(fireSystem.transform.position.x - transform.position.x) < 5 && Mathf.Abs(fireSystem.transform.position.y - transform.position.y) < 10 && Mathf.Abs(fireSystem.transform.position.z - transform.position.z) < 2)
            {
                health -= .5f;
                health = health < 0 ? 0 : health;
            }
        }
        //print(health);
        spriteDarkness = (health / 100);
        renderer.color = new Color(spriteDarkness, spriteDarkness, spriteDarkness, spriteAlpha);

        if (health < 1)
        {
            isDead = true;
        }
        if (isDead)
        {
            rigidbody.freezeRotation = false;

            spriteAlpha = Mathf.SmoothDamp(spriteAlpha, 0f, ref currentVelocity, fadeTime);

        }
        if (spriteAlpha <= .1)
        {

            Destroy(gameObject);
        }
        if(nextSpawn <= Time.time && Mathf.Abs(transform.position.x - player.transform.position.x) < 30)
        {
            //print("here");
            Instantiate(fireFighter,new Vector2(transform.position.x - 10f, transform.position.y), Quaternion.identity);
            nextSpawn += timeInBetweenSpawns;
        }else if(nextSpawn <= Time.time && Mathf.Abs(transform.position.x - player.transform.position.x) >= 30)
        {
            nextSpawn += timeInBetweenSpawns;
        }

    }
}
