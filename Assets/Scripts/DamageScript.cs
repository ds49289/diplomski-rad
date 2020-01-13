using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageScript : NetworkBehaviour
{
    public float damageRate;
    private GameObject otherPlayer;

    //[SerializeField]
    //private float destroyAnimationTime = 1f;
    //private bool isWaitingDone = false;


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
        //if (isWaitingDone)
        //{
        //    CmdDestroyMagicBall();
        //}
        if (hasCollided)
        {
            return;
        }
        Debug.Log("pogodio sam KURAC: " + other.tag);
        if (other.tag == "Player")
        {
            otherPlayer = other.gameObject;
            DealDamage(otherPlayer);
            Destroy(this.gameObject);
        }
        hasCollided = true;
    }

    public void DealDamage(GameObject otherPlayer)
    {
        otherPlayer.GetComponent<Health>().health -= damageRate;
        //CmdDestroyMagicBall();
    }

    //[Command]
    //public void CmdDestroyMagicBall()
    //{
    //    RpcDestroy();
    //}

    //[ClientRpc]
    //public void RpcDestroy()
    //{
    //    if (isLocalPlayer)
    //    {
    //        return;
    //    }
    //    Destroy(this.gameObject);
    //}
}
