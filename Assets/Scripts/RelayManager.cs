using UnityEngine;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Networking.Transport.Relay;
using Mirror;
using System.Threading.Tasks;
using Unity.Netcode.Transports.UTP;

public class RelayManager : MonoBehaviour
{
    private CardNetworkManager networkManager;
    private void Start()
    {
        networkManager = GameObject.Find("NetworkManager").GetComponent<CardNetworkManager>();
        Debug.Log(networkManager);
    }

    public async Task<string> StartHost()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(2);
        string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        networkManager.joinCode = joinCode;
        Debug.Log($"Join Code: {joinCode}");

        RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
        var transport = networkManager.GetComponent<UnityTransport>();
        transport.SetRelayServerData(relayServerData);

        networkManager.StartHost();
        return joinCode;
    }

    public async Task JoinHost(string joinCode)
    {
        JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");
        var transport = networkManager.GetComponent<UnityTransport>();
        transport.SetRelayServerData(relayServerData);

        networkManager.StartClient();
    }
}
