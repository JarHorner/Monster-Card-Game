using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardGameUIManager : NetworkBehaviour
{

    public static CardGameUIManager Instance { get; private set; }

    [SerializeField] private GameObject UICanvas;

    [SerializeField] private GameObject countdownTimerUIPrefab;
    private GameObject countdownTimerUISpawnedObject;


    private void Awake()
    {
        Instance = this;
    }

    public void SpawnCountdownTimerUI()
    {
        GameObject spawnedObject = Instantiate(countdownTimerUIPrefab);

        countdownTimerUISpawnedObject = spawnedObject;

        NetworkServer.Spawn(spawnedObject);

        RpcMoveSpawnedObject(spawnedObject, 0f, 200f, 0f);

    }

    [ClientRpc]
    private void RpcMoveSpawnedObject(GameObject gameObject, float xPos, float yPos, float zPos)
    {
        RectTransform spawnedObjectRectTransform = gameObject.GetComponent<RectTransform>();
        spawnedObjectRectTransform.SetParent(UICanvas.transform);
        spawnedObjectRectTransform.localPosition = new Vector3(xPos, yPos, zPos);
    }

    [ClientRpc]
    public void RpcDestoryCountdownTimerUI()
    {
        if (countdownTimerUISpawnedObject)
            NetworkServer.Destroy(countdownTimerUISpawnedObject);
    }
}
