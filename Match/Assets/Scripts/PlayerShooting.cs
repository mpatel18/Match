using UnityEngine;
using UnityEngine.Networking; 

public class PlayerShooting : NetworkBehaviour {

    private const string PLAYER_TAG = "Player"; 
    
    public PlayerWeapon weapon;
    public ParticleSystem muzzleFlash;


    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private LayerMask playerMask; 

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && isLocalPlayer)
        {
            ShootWeapon(); 
        }
    }

    [Client]
    private void ShootWeapon()
    {
        RaycastHit playerHit;
        muzzleFlash.Play();

        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out playerHit, weapon.range, playerMask))
        {
            if (playerHit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerDamaged(playerHit.collider.name, weapon.weaponDamage); 
            }
        }
    }

    [Command]
    private void CmdPlayerDamaged(string playerID, int damageTaken)
    {
        Debug.Log(playerID + " has been damaged");

        Player playerDamaged = GameManager.GetPlayer(playerID);
        playerDamaged.RpcTakeDamage(damageTaken);        
    }
}
