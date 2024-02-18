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

    [SerializeField] private GameObject cardPrefab;

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

        transform.position = spawnPositionList[GameMultiplayer.Instance.GetPlayerDataIndexFromClientId(OwnerClientId)];

        OnAnyPlayerSpawned?.Invoke(this, EventArgs.Empty);

        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;

        for (int i = 0; i < cardLocations.Count; i++)
        {
            SpawnPlayerCardObjectServerRpc(i);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnPlayerCardObjectServerRpc(int cardIndex)
    {
        GameObject newCard = Instantiate(cardPrefab);

        CardDisplay newCardDisplay = newCard.GetComponent<CardDisplay>();
       // newCardDisplay.SetupCard(CardSelection.Instance.GetPickedCards()[cardIndex]);

        // Setting position
        Vector3 playerLocation = gameObject.transform.position;
        newCard.transform.position = new Vector3(playerLocation.x + cardLocations[cardIndex].x, playerLocation.y + cardLocations[cardIndex].y, playerLocation.z + cardLocations[cardIndex].z);

        playerCardDisplays.Add(newCardDisplay);

        NetworkObject newCardNetworkObject = newCard.GetComponent<NetworkObject>();
        newCardNetworkObject.Spawn(true);
    }


        private void Start()
    {
        PlayerData playerData = GameMultiplayer.Instance.GetPlayerDataFromClientId(OwnerClientId);
        playerVisual.SetPlayerColor(GameMultiplayer.Instance.GetPlayerColor(playerData.portraitColorId));
        playerName.text = playerData.playerName.ToString();
    }

    private void Update()
    {

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
