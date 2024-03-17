using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardNetworkManager : NetworkManager
{
    [SerializeField] private List<GameObject> players;

    private int nextPlayerIndex = 1;
    private Dictionary<int, int> playerIdDictionary = new Dictionary<int, int>();

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

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {

        base.OnServerDisconnect(conn);
    }
}
