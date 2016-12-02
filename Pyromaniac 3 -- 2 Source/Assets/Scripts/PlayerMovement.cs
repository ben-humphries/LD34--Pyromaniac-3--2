using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    public GameObject fireSystem;
    public GameObject fireBall;

    [HideInInspector]
    public static ArrayList fireSystems;
    [HideInInspector]
    public static float health = 100;

    Rigidbody rigidbody;

    public float speed = .1f;

    public Canvas deathScreen;

    private bool isGrounded;
    private bool spawnFire;
    private int fireIndex = 0;
    bool canJump;
    bool canShoot;

	void Start () {
        health = 100;
        rigidbody = GetComponent<Rigidbody>();
        fireSystems = new ArrayList();
        deathScreen.gameObject.SetActive(false);
	}
	
    void Update()
    {
        if (Input.GetKeyDown("space") && isGrounded)
        {
            canJump = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            canShoot = true;
        }
    }
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        //health = 100;
        //Left and Right
        transform.Translate(new Vector2 (Input.GetAxis("Horizontal") * speed, 0f));

        //Jump
        if(canJump)
        {
            print("here");
            rigidbody.velocity += new Vector3(0f, 27f, 0f);
            canJump = false;
        }

        //Fire Trail
        spawnFire = true;
        foreach (GameObject fireSystem in fireSystems)
        {
            if((Mathf.Abs(fireSystem.transform.position.x - transform.position.x) < 5))
            {
                spawnFire = false;
            }
        }
        if (spawnFire)
        {
            Vector3 firePosition = transform.position;

            GameObject[] flammables = GameObject.FindGameObjectsWithTag("Flammable");

            foreach(GameObject flammable in flammables)
            {
                if(Mathf.Abs(flammable.transform.position.x - transform.position.x) < 5)
                {
                    firePosition.z = flammable.transform.position.z;
                }
            }

            firePosition.y += Random.Range(-2f, 2f);
            fireSystems.Add (Instantiate(fireSystem, firePosition, transform.rotation));
            fireIndex++;
        }

        //Fire Ball
        if (canShoot)
        {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir = mousePos - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            angle -= 90;

             Instantiate(fireBall, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            canShoot = false;
            
        }
        //Death Condition
        if(health <= 0)
        {
            Die();
        }
    }
    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
    private void Die()
    {
        deathScreen.gameObject.SetActive(true);
        Destroy(gameObject);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
    
}
