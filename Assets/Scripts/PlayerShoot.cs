using UnityEngine;
using Mirror;

public class PlayerShoot : NetworkBehaviour
{
    public PlayerWeapon weapon;

    [SerializeField]
    private GameObject wand;

    [SerializeField]
    private LayerMask mask;

    void Start()
    {
        if (wand == null)
        {
            Debug.LogError("PlayerShoot: No wand referenced!");
            this.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit _hit;
        
        // We hit something
        if (Physics.Raycast(wand.transform.position, wand.transform.up, out _hit, weapon.range, mask))
        {
            Debug.Log("We hit " + _hit.collider.name);
        }
    }
}
