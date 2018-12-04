using UnityEngine;
using UnityEngine.Networking; 

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] components;

    GameObject gameCamera; 

    private void Start()
    {
        if(gameCamera != null)
        {
            gameCamera = Camera.main.gameObject;
        }

        DisableComponents(); 
    }

    private void DisableComponents()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < components.Length; i++)
            {
                components[i].enabled = false; 
            }
        } else
        {
            if(gameCamera != null)
            {
                gameCamera.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        if (gameCamera != null && !gameCamera.activeSelf)
            gameCamera.SetActive(true); 
    }
}
