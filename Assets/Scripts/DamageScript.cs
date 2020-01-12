using Mirror.Examples.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : MonoBehaviour
{
    public float damageRate;
    private GameObject otherPlayer;

    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    public bool hasCollided;
    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        hasCollided = false;
    }
    
    void Update()
    {
        
    }

    void OnParticleCollision(UnityEngine.GameObject other)
    {
        if (hasCollided)
        {
            return;
        }
        Debug.Log("pogodio sam KURAC: " + other.tag);
        if(other.tag == "Player")
        {
            otherPlayer = other.gameObject;
            DealDamage(otherPlayer);
        }
        hasCollided = true;
    }

    public void DealDamage(GameObject otherPlayer)
    {
       otherPlayer.GetComponent<Health>().health -= damageRate;
       Destroy(this.gameObject);
    }
}
