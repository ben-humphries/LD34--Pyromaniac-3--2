using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class FireBall : MonoBehaviour {

    // Use this for initialization

    private Rigidbody rigidbody;
    private Vector3 dir;
    public float speed = 10f;
    public float lifeSpan = 10f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.up * speed);

        Destroy(gameObject, lifeSpan);
    }
	
	
	
}
