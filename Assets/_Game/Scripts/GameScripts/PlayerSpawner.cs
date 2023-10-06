using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    /*public GameObject playerPrefab;
    public Transform playerSpawnPoint1;
    public Transform playerSpawnPoint2;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        GameObject player1 = Instantiate(playerPrefab, playerSpawnPoint1.position, Quaternion.identity);
        GameObject player2 = Instantiate(playerPrefab, playerSpawnPoint2.position, Quaternion.identity);

        // You may need to configure the Input System Player Input component on the player prefabs.
        // Ensure that player controllers and health do not overlap as needed.
    }*/

    int index = 0;
    [SerializeField] List<GameObject> fighters = new List<GameObject>();
    PlayerInputManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<PlayerInputManager>();
        index = Random.Range(0, fighters.Count);
        manager.playerPrefab = fighters[index];
    }

    public void SpawnCharacter(PlayerInput input)
    {
        index = Random.Range(0, fighters.Count);
        manager.playerPrefab = fighters[index];
    }
}
