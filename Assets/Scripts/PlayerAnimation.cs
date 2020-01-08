using Mirror;
using UnityEngine;

public class PlayerAnimation : NetworkBehaviour

{
    [SyncVar]
    public bool Moving = false;
    [SerializeField]
    private Animator anim;

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        PlayerAnimate(horizontal, vertical);
    }

    public void PlayerAnimate(float horizontal, float vertical)
    {

        if (vertical == 1 || vertical == -1 || horizontal == 1 || horizontal == -1)
        {
            if (!Moving)
            {
                Moving = true;
                Move(Moving);
                if (isServer)
                {
                    RpcMove(Moving);
                }
                else
                {
                    CmdMove(Moving);
                }
            }
        }
        else
        {
            if (Moving)
            {
                Moving = false;
                Move(Moving);
                if (isServer)
                {
                    RpcMove(Moving);
                }
                else
                {
                    CmdMove(Moving);
                }
            }
        }
    }

    void Move(bool moving)
    {
        anim.SetBool("Moving", moving);
    }

    [Command]
    void CmdMove(bool moving)
    {
        RpcMove(moving);
    }

    [ClientRpc]
    void RpcMove(bool moving)
    {
        if (isLocalPlayer)
            return;

        Move(moving);
    }
}