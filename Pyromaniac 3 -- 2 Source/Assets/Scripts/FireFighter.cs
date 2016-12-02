using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class FireFighter : MonoBehaviour {

    public Sprite Bono;

    Rigidbody rigidbody;
    SpriteRenderer renderer;
    float spriteDarkness;
    float spriteAlpha;

    bool shootWater = false;
    bool moveTowardsPlayer = false;

    private float currentVelocity = 0.0F;
    public float fadeTime = 1f;
    public GameObject target;

    [HideInInspector]
    public float health = 100;

    bool isDead;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "FireBall")
        {
            health -= 50;

        }
    }
    // Use this for initialization
    void Start () {

        rigidbody = GetComponent<Rigidbody>();
        renderer = GetComponent<SpriteRenderer>();
        spriteDarkness = 1f;
        spriteAlpha = 1f;
        isDead = false;
        target = GameObject.FindGameObjectWithTag("Player");

        int randomNum = Random.Range(0, 100);

        if (randomNum == 55)
        {
            renderer.sprite = Bono;
            health = 1000;
        }
    }

    // Update is called once per frame
    void Update() {

        //Death Conditions
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
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            rigidbody.useGravity = false;
            spriteAlpha = Mathf.SmoothDamp(spriteAlpha, 0f, ref currentVelocity, fadeTime);
            moveTowardsPlayer = false;

            
        }
        if (spriteAlpha <= .1)
        {
            
            Destroy(gameObject);
        }

        //Looks for player
        RaycastHit hit;
        Debug.DrawRay(transform.position, (target.transform.position - transform.position).normalized);
        if(Physics.Raycast(transform.position, (target.transform.position - transform.position).normalized, out hit, 30))
        {
            if(hit.collider.tag == "Player")
            {
                shootWater = true;
                moveTowardsPlayer = true;
            }
        }
        else
        {
            shootWater = false;
            moveTowardsPlayer = false;
        }


        if (moveTowardsPlayer)
        {
            //Move
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 3f * Time.deltaTime);
            //Rotate
            if(transform.position.x - target.transform.position.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }

        }
        if (shootWater)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
