using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardNetworkManager : NetworkManager
{
    [SerializeField] private List<GameObject> players;

    private int nextPlayerIndex = 1;
    private Dictionary<int, int> playerIdDictionary = new Dictionary<int, int>();

    private bool hostDisconnected = false;

    public override void OnServerAddPlayer(NetworkConnectionToClient connection)
    {
        GameObject newPlayer = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(connection, newPlayer);
        players.Add(newPlayer);

        // Assign a player ID to the new player
        int playerId = nextPlayerIndex;
        playerIdDictionary[connection.connectionId] = playerId;
        nextPlayerIndex++;

        if (NetworkServer.connections.Count == 2)
        {
            int id = 1;
            Debug.Log("Two Players");
            foreach(GameObject player in players)
            {
                PlayerManager playerManager = player.GetComponent<PlayerManager>();
                CardGameManager.Instance.RpcAssignPlayerId(playerManager, id);
                id++;
            }

            CardGameUIManager.Instance.SpawnStartGameUI();
        }
    }

    // Gets the players index for a connection
    public int GetPlayerId(int connectionId)
    {
        if (playerIdDictionary.ContainsKey(connectionId))
        {
            return playerIdDictionary[connectionId];
        }
        else
        {
            Debug.LogError("Player index not found for connection: " + connectionId);
            return -1;
        }
    }

    public void DisconnectHost()
    {
        StopHost();
        hostDisconnected = true;
        Debug.Log("Host disconnected.");
    }

    public override void OnStopServer()
    {
        if (hostDisconnected)
        {
            // This prevents clients from being disconnected when the host disconnects
            Debug.Log("Server stopped but connections preserved.");
            hostDisconnected = false;
        }
        else
        {
            base.OnStopServer();
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        Debug.Log("Client disconnected.");
        base.OnServerDisconnect(conn);
    }
}
