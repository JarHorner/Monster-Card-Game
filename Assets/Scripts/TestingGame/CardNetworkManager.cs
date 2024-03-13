using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardNetworkManager : NetworkManager
{
    [SerializeField] private List<GameObject> players;

    private int playerIndex = 0;
    private Dictionary<int, int> connectionIdToPlayerIndex = new Dictionary<int, int>();

    public override void OnServerAddPlayer(NetworkConnectionToClient connection)
    {
        GameObject newPlayer = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(connection, newPlayer);
        players.Add(newPlayer);


        // Assign a player index to the new player
        connectionIdToPlayerIndex[connection.connectionId] = playerIndex++;

        if (NetworkServer.connections.Count == 2)
        {
            Debug.Log("Two Players");

        }
    }

    // Gets the players index for a connection
    public int GetPlayerIndex(NetworkConnection conn)
    {
        if (connectionIdToPlayerIndex.ContainsKey(conn.connectionId))
        {
            return connectionIdToPlayerIndex[conn.connectionId];
        }
        else
        {
            Debug.LogError("Player index not found for connection: " + conn.connectionId);
            return -1;
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {

        base.OnServerDisconnect(conn);
    }
}
