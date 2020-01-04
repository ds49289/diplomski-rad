using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : NetworkBehaviour
{

    [SerializeField]
    private GameObject shield;

    private bool isShieldUp = false;

    // Start is called before the first frame update
    void Start()
    {
        shield.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if(Input.GetButton("Fire2"))
        {
            if (!isShieldUp)
            {
                ActivateShield();
                if(isServer)
                {
                    RpcActivateShield();
                }
                else
                {
                    CmdActivateShield();
                }
            }
        }
        else
        {
            if (isShieldUp)
            {
                DisableShield();
                if (isServer)
                {
                    RpcDisableShield();
                }
                else
                {
                    CmdDisableShield();
                }
            }
        }

    }

    public void ActivateShield()
    {
        shield.SetActive(true);
        isShieldUp = true;
    }

    public void DisableShield()
    {
        isShieldUp = false;
        shield.SetActive(false);
    }

    [Command]
    void CmdActivateShield()
    {
        ActivateShield();
        RpcActivateShield();
    }

    [ClientRpc]
    void RpcActivateShield()
    {
        if(isLocalPlayer)
        {
            return;
        }
        ActivateShield();
    }

    [Command]
    void CmdDisableShield()
    {
        ActivateShield();
        RpcDisableShield();
    }

    [ClientRpc]
    void RpcDisableShield()
    {
        if (isLocalPlayer)
        {
            return;
        }
        DisableShield();
    }
}
