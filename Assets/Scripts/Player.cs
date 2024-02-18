using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : NetworkBehaviour
{
    public static event EventHandler OnAnyPlayerSpawned;

    public static Player LocalInstance { get; private set; }

    [SerializeField] private List<GameObject> cardPrefabs;

    [SerializeField] private List<Vector3> spawnPositionList;
    [SerializeField] private PlayerVisual playerVisual;
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private List<Vector3> cardLocations;
    [SerializeField] private List<CardDisplay> playerCardDisplays;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            LocalInstance = this;
        }

        PlayerData playerData = GameMultiplayer.Instance.GetPlayerDataFromClientId(OwnerClientId);
        playerVisual.SetPlayerColor(GameMultiplayer.Instance.GetPlayerColor(playerData.portraitColorId));
        playerName.text = playerData.playerName.ToString();

        transform.position = spawnPositionList[GameMultiplayer.Instance.GetPlayerDataIndexFromClientId(OwnerClientId)];

        OnAnyPlayerSpawned?.Invoke(this, EventArgs.Empty);

        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;

        //SpawnPlayerCardObjectServerRpc();
        for (int i = 0; i < cardLocations.Count; i++)
        {
            int randomCard = UnityEngine.Random.Range(0, cardPrefabs.Count);

            GameObject newCard = Instantiate(cardPrefabs[randomCard]);

            CardDisplay newCardDisplay = newCard.GetComponent<CardDisplay>();
            // newCardDisplay.SetupCard(CardSelection.Instance.GetPickedCards()[cardIndex]);

            // Setting position
            Vector3 playerLocation = gameObject.transform.position;
            newCard.transform.position = new Vector3(playerLocation.x + cardLocations[i].x, playerLocation.y + cardLocations[i].y, playerLocation.z + cardLocations[i].z);

            playerCardDisplays.Add(newCardDisplay);

            NetworkObject newCardNetworkObject = newCard.GetComponent<NetworkObject>();
            newCardNetworkObject.Spawn(true);
        }

    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnPlayerCardObjectServerRpc()
    {
        for (int i = 0; i < cardLocations.Count; i++)
        {
            int randomCard = UnityEngine.Random.Range(0, cardPrefabs.Count);

            GameObject newCard = Instantiate(cardPrefabs[randomCard]);

            CardDisplay newCardDisplay = newCard.GetComponent<CardDisplay>();
            // newCardDisplay.SetupCard(CardSelection.Instance.GetPickedCards()[cardIndex]);

            // Setting position
            Vector3 playerLocation = gameObject.transform.position;
            newCard.transform.position = new Vector3(playerLocation.x + cardLocations[i].x, playerLocation.y + cardLocations[i].y, playerLocation.z + cardLocations[i].z);

            playerCardDisplays.Add(newCardDisplay);

            NetworkObject newCardNetworkObject = newCard.GetComponent<NetworkObject>();
            newCardNetworkObject.Spawn(true);
        }

    }

    private void OnMouseDown()
    {
        if (IsLocalPlayer)
        {
            Debug.Log("Click registers");
        }
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId)
    {
        if (clientId == OwnerClientId)
        {

        }
    }

    public NetworkObject GetNetworkObject()
    {
        return NetworkObject;
    }

    public bool IsLocalPlayerHovering()
    {
        return IsLocalPlayer;
    }

    public List<CardDisplay> GetPlayerCardDisplays()
    {
        return playerCardDisplays;
    }
}
