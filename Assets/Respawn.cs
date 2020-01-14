using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : NetworkBehaviour
{
    [SyncVar]
    public int numberOfKills = 0;
    [SyncVar]
    public int numberOfDeaths = 0;

    List<Transform> AllStartPositions = NetworkManager.startPositions;
    private Transform _startPoint;
    [SyncVar]
    private bool firstSpawnPoint = true;
    [SyncVar]
    public bool isDead = false;
    // Start is called before the first frame update
    private int playerHealth;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        playerHealth = GetComponent<Health>().health;
        if (playerHealth <= 0)
        {
            CmdRespawnPlayer();
        }
        //if (isDead)
        //{
        //    RespawnPlayer();
        //    if (isServer)
        //    {
        //        RpcRespawnPlayer();
        //    }
        //    else
        //    {
        //        CmdRespawnPlayer();
        //    }
        //}
    }

    private void RespawnPlayer()
    {

        if (firstSpawnPoint)
        {
            firstSpawnPoint = false;
            _startPoint = AllStartPositions[0];
            isDead = false;
        }
        else
        {
            firstSpawnPoint = true;
            _startPoint = AllStartPositions[1];
            isDead = false;
        }
        transform.position = _startPoint.position;
        GetComponent<Health>().health = 100;
        //numberOfKills++;

    }

    [Command]
    void CmdRespawnPlayer()
    {
        RespawnPlayer();
        //RpcRespawnPlayer();
    }

    //[ClientRpc]
    //void RpcRespawnPlayer()
    //{
    //    if (isLocalPlayer)
    //    {
    //        return;
    //    }
    //    RespawnPlayer();
    //}
}
