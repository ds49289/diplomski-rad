using Mirror;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    Camera sceneCamera;

    void Start()
    {
        if (!isLocalPlayer)
        {
            foreach(var component in componentsToDisable)
            {
                component.enabled = false;
            }
        } else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
            
        }
    }

    void AssignRemoteLayer ()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void OnDisable()
    {
        if(sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }
    }
}
