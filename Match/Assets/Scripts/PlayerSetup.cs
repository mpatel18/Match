using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI; 

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] components;
    [SerializeField]
    string remotePlayer;
    [SerializeField]
    GameObject playerUI;

    private GameObject playerUIinstance;

    GameObject gameCamera;

    private void Start()
    {   
        if(gameCamera != null)
        {
            gameCamera = Camera.main.gameObject;
        }

        DisableComponents();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player player = GetComponent<Player>();

        GameManager.RegisterPlayer(netID, player);
        GetComponent<Player>().SetUp(); 
    }

    private void DisableComponents()
    {
        if (!isLocalPlayer)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            for (int i = 0; i < components.Length; i++)
            {
                components[i].enabled = false; 
            }

            gameObject.layer = LayerMask.NameToLayer(remotePlayer); 

        } else
        {
            playerUIinstance = Instantiate(playerUI);
            if(gameCamera != null)
            {
                gameCamera.SetActive(false);
            }
        }
    }

    private void OnDisable()
    {
        if (gameCamera != null && !gameCamera.activeSelf)
        {
            gameCamera.SetActive(true);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameManager.DeRegisterPlayer(transform.name);
        Destroy(playerUIinstance); 
    }

}
