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


        //newPlayer.GetComponent<PlayerManager>().SetPlayerNumber(NetworkServer.connections.Count);

        // Assign a player ID to the new player
        int playerId = nextPlayerIndex;
        playerIdDictionary[connection.connectionId] = playerId;
        nextPlayerIndex++;

        // Debug.Log(connection.connectionId);
        // connectionIdToPlayerIndex.Add(connection.connectionId, playerIndex++);

        if (NetworkServer.connections.Count == 2)
        {
            Debug.Log("Two Players");
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
