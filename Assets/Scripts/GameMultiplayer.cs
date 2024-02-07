using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMultiplayer : NetworkBehaviour
{
    public const int MAX_PLAYER_AMT = 2;
    private const string PLAYER_PREFS_PLAYER_NAME_MULTIPLAYER = "PlayerNameMultiplayer";

    public static GameMultiplayer Instance;

    public static bool playMultiplayer;

    public event EventHandler OnTryingToJoinGame;
    public event EventHandler OnFailedToJoinGame;
    public event EventHandler OnPlayerDataNetworkListChanged;

    //[SerializeField] private KitchenObjectListSO kitchenObjectListSO;
    [SerializeField] private List<Color> portraitColorList;

    private NetworkList<PlayerData> playerDataNetworkList;
    private string playerName;


    void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);

        playerName = PlayerPrefs.GetString(PLAYER_PREFS_PLAYER_NAME_MULTIPLAYER, "playerName" + UnityEngine.Random.Range(100, 1000));
        playerDataNetworkList = new NetworkList<PlayerData>();
        playerDataNetworkList.OnListChanged += playerDataNetworkList_OnListChanged;
    }

    private void Start()
    {
        if (!playMultiplayer)
        {
            // Singleplayer
            StartHost();
            Loader.LoadNetwork(Loader.Scene.GameScene);
        }
    }

    private void playerDataNetworkList_OnListChanged(NetworkListEvent<PlayerData> changeEvent)
    {
        OnPlayerDataNetworkListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void StartHost()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += NetworkManager_ConnectionApprovalCallback;
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_Server_OnClientDisconnectCallback;

        NetworkManager.Singleton.StartHost();
    }

    private void NetworkManager_Server_OnClientDisconnectCallback(ulong clientId)
    {
        for (int i = 0; i < playerDataNetworkList.Count; i++)
        {
            PlayerData playerData = playerDataNetworkList[i];
            if (playerData.clientId == clientId)
            {
                playerDataNetworkList.RemoveAt(i);
            }
        }
    }

    private void NetworkManager_OnClientConnectedCallback(ulong clientId)
    {
        playerDataNetworkList.Add(new PlayerData
        {
            clientId = clientId,
        });
        SetPlayerNameServerRpc(GetPlayerName());
        SetPlayerIdServerRpc(AuthenticationService.Instance.PlayerId);
    }

    private void NetworkManager_ConnectionApprovalCallback(NetworkManager.ConnectionApprovalRequest connectionApprovalRequest, NetworkManager.ConnectionApprovalResponse connectionApprovalResponse)
    {
        if (SceneManager.GetActiveScene().name != Loader.Scene.CharacterSelectScene.ToString())
        {
            connectionApprovalResponse.Approved = false;
            connectionApprovalResponse.Reason = "Game has already started";
            return;
        }

        if (NetworkManager.Singleton.ConnectedClientsIds.Count >= MAX_PLAYER_AMT)
        {
            connectionApprovalResponse.Approved = false;
            connectionApprovalResponse.Reason = "Game has max amount of players";
            return;
        }

        connectionApprovalResponse.Approved = true;
    }

    public void StartClient()
    {
        OnTryingToJoinGame?.Invoke(this, EventArgs.Empty);

        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_Client_OnDisconnectCallback;
        NetworkManager.Singleton.OnClientConnectedCallback += NetworkManager_Client_OnClientConnectedCallback;
        NetworkManager.Singleton.StartClient();
    }

    private void NetworkManager_Client_OnClientConnectedCallback(ulong clientId)
    {
        SetPlayerNameServerRpc(GetPlayerName());
        SetPlayerIdServerRpc(AuthenticationService.Instance.PlayerId);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetPlayerNameServerRpc(string playerName, ServerRpcParams serverRpcParams = default)
    {
        int playerDataIndex = GetPlayerDataIndexFromClientId(serverRpcParams.Receive.SenderClientId);

        PlayerData playerData = playerDataNetworkList[playerDataIndex];

        playerData.playerName = playerName;

        playerDataNetworkList[playerDataIndex] = playerData;
    }

    [ServerRpc(RequireOwnership = false)]
    private void SetPlayerIdServerRpc(string playerId, ServerRpcParams serverRpcParams = default)
    {
        int playerDataIndex = GetPlayerDataIndexFromClientId(serverRpcParams.Receive.SenderClientId);

        PlayerData playerData = playerDataNetworkList[playerDataIndex];

        playerData.playerId = playerId;

        playerDataNetworkList[playerDataIndex] = playerData;
    }

    private void NetworkManager_Client_OnDisconnectCallback(ulong clientId)
    {
        OnFailedToJoinGame?.Invoke(this, EventArgs.Empty);
    }

    //public void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
    //{
    //    SpawnKitchenObjectServerRpc(GetKitchenObjectSOIndex(kitchenObjectSO), kitchenObjectParent.GetNetworkObject());
    //}

    //[ServerRpc(RequireOwnership = false)]
    //private void SpawnKitchenObjectServerRpc(int kitchenObjectSOIndex, NetworkObjectReference kitchenObjectParentNetworkObjectReference)
    //{
    //    KitchenObjectSO kitchenObjectSO = GetKitchenObjectSOFromIndex(kitchenObjectSOIndex);

    //    kitchenObjectParentNetworkObjectReference.TryGet(out NetworkObject kitchenObjectParentNetworkObject);
    //    IKitchenObjectParent kitchenObjectParent = kitchenObjectParentNetworkObject.GetComponent<IKitchenObjectParent>();

    //    if (kitchenObjectParent.HasKitchenObject())
    //    {
    //        // Parent already spawned object
    //        return;
    //    }

    //    Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

    //    NetworkObject kitchenObjectNetworkObject = kitchenObjectTransform.GetComponent<NetworkObject>();
    //    kitchenObjectNetworkObject.Spawn(true);

    //    KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

    //    kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
    //}

    //public int GetKitchenObjectSOIndex(KitchenObjectSO kitchenObjectSO)
    //{
    //    return kitchenObjectListSO.kitchenObjectSOList.IndexOf(kitchenObjectSO);
    //}

    //public KitchenObjectSO GetKitchenObjectSOFromIndex(int kitchenObjectSOIndex)
    //{
    //    return kitchenObjectListSO.kitchenObjectSOList[kitchenObjectSOIndex];
    //}

    //public void DestoryKitchenObject(KitchenObject kitchenObject)
    //{
    //    DestoryKitchenObjectServerRpc(kitchenObject.NetworkObject);
    //}

    //[ServerRpc(RequireOwnership = false)]
    //private void DestoryKitchenObjectServerRpc(NetworkObjectReference kitchenObjectParentNetworkObjectReference)
    //{
    //    kitchenObjectParentNetworkObjectReference.TryGet(out NetworkObject kitchenObjectNetworkObject);
    //    if (kitchenObjectNetworkObject == null)
    //    {
    //        // Object already destroyed
    //        return;
    //    }
    //    KitchenObject kitchenObject = kitchenObjectNetworkObject.GetComponent<KitchenObject>();

    //    ClearKitchenObjectOnParentClientRpc(kitchenObjectParentNetworkObjectReference);

    //    kitchenObject.DestroySelf();
    //}

    //[ClientRpc]
    //private void ClearKitchenObjectOnParentClientRpc(NetworkObjectReference kitchenObjectParentNetworkObjectReference)
    //{
    //    kitchenObjectParentNetworkObjectReference.TryGet(out NetworkObject kitchenObjectNetworkObject);
    //    KitchenObject kitchenObject = kitchenObjectNetworkObject.GetComponent<KitchenObject>();

    //    kitchenObject.ClearKitchenObjectOnParent();
    //}

    public bool IsPlayerIndexConnected(int playerIndex)
    {
        return playerIndex < playerDataNetworkList.Count;
    }

    public int GetPlayerDataIndexFromClientId(ulong clientId)
    {
        for (int i = 0; i < playerDataNetworkList.Count; i++)
        {
            if (playerDataNetworkList[i].clientId == clientId)
                return i;
        }
        return -1;
    }

    public PlayerData GetPlayerDataFromClientId(ulong clientId)
    {
        foreach (PlayerData playerData in playerDataNetworkList)
        {
            if (playerData.clientId == clientId)
                return playerData;
        }
        return default;
    }

    public PlayerData GetPlayerData()
    {
        return GetPlayerDataFromClientId(NetworkManager.Singleton.LocalClientId);
    }

    public PlayerData GetPlayerDataFromPlayerIndex(int playerIndex)
    {
        return playerDataNetworkList[playerIndex];
    }

    public Color GetPlayerColor(int colorId)
    {
        return portraitColorList[colorId];
    }

    public void ChangePlayerColor(int colorId)
    {
        ChangePlayerColorServerRpc(colorId);
    }

    [ServerRpc(RequireOwnership = false)]
    private void ChangePlayerColorServerRpc(int colorId, ServerRpcParams serverRpcParams = default)
    {
        if (!IsColorAvailable(colorId))
        {
            return;
        }

        int playerDataIndex = GetPlayerDataIndexFromClientId(serverRpcParams.Receive.SenderClientId);

        PlayerData playerData = playerDataNetworkList[playerDataIndex];

        playerData.portraitColorId = colorId;

        playerDataNetworkList[playerDataIndex] = playerData;
    }

    private bool IsColorAvailable(int colorId)
    {
        foreach (PlayerData playerData in playerDataNetworkList)
        {
            if (playerData.portraitColorId == colorId)
            {
                return false;
            }
        }
        return true;
    }

    private int GetFirstUnusedColorId()
    {
        for (int i = 0; i < portraitColorList.Count; i++)
        {
            if (IsColorAvailable(i))
            {
                return i;
            }
        }
        return -1;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public void SetPlayerName(string newPlayerName)
    {
        playerName = newPlayerName;

        PlayerPrefs.SetString(PLAYER_PREFS_PLAYER_NAME_MULTIPLAYER, playerName);
    }

    public void KickPlayer(ulong clientId)
    {
        NetworkManager.Singleton.DisconnectClient(clientId);

        NetworkManager_Server_OnClientDisconnectCallback(clientId);
    }
}
