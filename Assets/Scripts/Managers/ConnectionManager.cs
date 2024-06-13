using UnityEngine;
using Mirror;
using System.Collections;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviour
{
    public float searchTime = 5f; // Time to search for a host
    private NetworkManager networkManager;

    [SerializeField] private GameObject connectingPanel;

    private void Start()
    {
        networkManager = FindObjectOfType<NetworkManager>();
    }

    public void OnConnectButtonClicked()
    {
        connectingPanel.SetActive(true);

        StartCoroutine(SearchForHost());
    }

    private IEnumerator SearchForHost()
    {
        bool connected = false;

        // Start searching for a host
        networkManager.StartClient();
        float startTime = Time.time;

        while (Time.time - startTime < searchTime)
        {
            if (networkManager.isNetworkActive && NetworkClient.isConnected)
            {
                connected = true;
                Debug.Log("Connected to a host.");
                break;
            }

            yield return null; // Wait until the next frame
        }

        if (!connected)
        {
            // If not connected within the search time, become a host
            Debug.Log("No host found, starting as host.");
            networkManager.StopClient();
            networkManager.StartHost();
        }
    }
}
