using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class Water : MonoBehaviour {

    FireFighter fireFighter;
    ParticleSystem water;

	// Use this for initialization
	void Start () {
        fireFighter = transform.parent.gameObject.GetComponent<FireFighter>();
        water = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if(fireFighter.health <= 0)
        {
            water.enableEmission = false;
        }
	
	}
    void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Player")
        {
            PlayerMovement.health -= 1;
            PlayerMovement.health = PlayerMovement.health < 0 ? 0 : PlayerMovement.health;
           // print(PlayerMovement.health);
        }
    }
}
