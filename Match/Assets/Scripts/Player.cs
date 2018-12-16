using System.Collections;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private Behaviour[] disablePlayerOnDeath;
    private bool[] checkEnabled;

    [SyncVar]
    public int currentHealth;
    [SyncVar]
    public int killCount; 
    [SyncVar]
    private bool isPlayerDead = false; 
    public bool isDead
    {
        get { return isPlayerDead; }
        protected set { isPlayerDead = value;  }
    }

    public void Update()
    {
        if (!isLocalPlayer)
        {
            return; 

        }

    } 

    public void SetUp()
    {
        checkEnabled = new bool[disablePlayerOnDeath.Length];
        for (int i = 0; i < checkEnabled.Length; i++)
        {
            checkEnabled[i] = disablePlayerOnDeath[i].enabled; 
        }
        SetDafault();
    }


    public void SetDafault()
    {
        isPlayerDead = false; 
        currentHealth = maxHealth;

        for (int i = 0; i < disablePlayerOnDeath.Length; i++)
        {
            disablePlayerOnDeath[i].enabled = checkEnabled[i]; 
        }

        Collider playerCollider = GetComponent<Collider>();
        if (playerCollider != null)
            playerCollider.enabled = true; 
    }

    [ClientRpc]
    public void RpcTakeDamage(int damage)
    {
        if (isPlayerDead)
            Dead();

        if (!isPlayerDead)
        {
            if (currentHealth <= 20)
                Dead();
            
            
            currentHealth -= damage;
            Debug.Log(transform.name + " has " + currentHealth + " left");
            

        }

        return; 
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.gameSetting.respawnTime);
        SetDafault();

        Transform start = NetworkManager.singleton.GetStartPosition();
        transform.position = start.position;
        transform.rotation = start.rotation; 
    }

    private void Dead()
    {
        isDead = true;

        for (int i = 0; i < disablePlayerOnDeath.Length; i++)
        {
            disablePlayerOnDeath[i].enabled = false; 
        }

        Collider playerCollider = GetComponent<Collider>();
        if (playerCollider != null)
            playerCollider.enabled = false;

        Debug.Log(transform.name + " is dead");

        Debug.Log(transform.name + " has respawned");
        StartCoroutine(Respawn());  

    }
}
