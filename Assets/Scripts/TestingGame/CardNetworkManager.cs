using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardNetworkManager : NetworkManager
{
    [SerializeField] private List<GameObject> players;
    public override void OnServerAddPlayer(NetworkConnectionToClient connection)
    {
        GameObject newPlayer = Instantiate(playerPrefab);
        NetworkServer.AddPlayerForConnection(connection, newPlayer);
        players.Add(newPlayer);


        if (NetworkServer.connections.Count == 2)
        {
            Debug.Log("Two Players");

            //foreach (var conn in NetworkServer.connections)
            //{
            //    if (conn.Value != null && conn.Value.identity != null)
            //    {
            //        NetworkIdentity playerIdentity = conn.Value.identity;

            //        PlayerManager playerManager = playerIdentity.GetComponent<PlayerManager>();

            //        playerManager.CmdDealCards();

            //        Debug.Log($"Dealing Cards!");
            //    }
            //}
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {

        base.OnServerDisconnect(conn);
    }
}
