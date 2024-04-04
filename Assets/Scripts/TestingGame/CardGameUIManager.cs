using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CardGameUIManager : NetworkBehaviour
{

    public static CardGameUIManager Instance { get; private set; }

    [SerializeField] private GameObject UICanvas;

    [SerializeField] private GameObject StartGameUIPrefab;
    private GameObject StartGameUISpawnedObject;

    [SerializeField] private GameObject countdownTimerUIPrefab;
    private GameObject countdownTimerUISpawnedObject;

    [SerializeField] private GameObject turnTimerUIPrefab;
    private GameObject turnTimerUISpawnedObject;

    [SerializeField] private GameObject EndGameUIPrefab;
    private GameObject endGameUISpawnedObject;

    [SerializeField] private GameObject changeTurnUIPrefab;
    private GameObject changeTurnUISpawnedObject;



    private void Awake()
    {
        Instance = this;
    }

    public void SpawnStartGameUI()
    {
        GameObject spawnedObject = Instantiate(StartGameUIPrefab);

        StartGameUISpawnedObject = spawnedObject;

        NetworkServer.Spawn(spawnedObject);

        RpcMoveSpawnedObject(spawnedObject, 0f, 0f, 0f);
    }

    public void SpawnCountdownTimerUI()
    {
        GameObject spawnedObject = Instantiate(countdownTimerUIPrefab);

        countdownTimerUISpawnedObject = spawnedObject;

        NetworkServer.Spawn(spawnedObject);

        RpcMoveSpawnedObject(spawnedObject, 0f, 200f, 0f);
    }

    public void SpawnTurnTimerUI()
    {
        GameObject spawnedObject = Instantiate(turnTimerUIPrefab);

        turnTimerUISpawnedObject = spawnedObject;

        NetworkServer.Spawn(spawnedObject);

        RpcMoveSpawnedObject(spawnedObject, 0f, 550f, 0f);
    }

    public void SpawnEndGameUI()
    {
        GameObject spawnedObject = Instantiate(EndGameUIPrefab);

        endGameUISpawnedObject = spawnedObject;

        NetworkServer.Spawn(spawnedObject);

        RpcMoveSpawnedObject(spawnedObject, 0f, 0f, 0f);
    }

    public void SpawnChangeTurnUI(string turnText)
    {
        GameObject spawnedObject = Instantiate(changeTurnUIPrefab);

        changeTurnUISpawnedObject = spawnedObject;

        NetworkServer.Spawn(spawnedObject);

        spawnedObject.GetComponent<ChangeTurnUI>().SetPlayerTurnText(turnText);

        RpcMoveSpawnedObject(spawnedObject, 0f, 0f, 0f);
    }

    [ClientRpc]
    private void RpcMoveSpawnedObject(GameObject gameObject, float xPos, float yPos, float zPos)
    {
        RectTransform spawnedObjectRectTransform = gameObject.GetComponent<RectTransform>();
        spawnedObjectRectTransform.SetParent(UICanvas.transform);
        spawnedObjectRectTransform.localPosition = new Vector3(xPos, yPos, zPos);
    }

    [ClientRpc]
    public void RpcDestoryStartGameUI()
    {
        if (StartGameUISpawnedObject)
            NetworkServer.Destroy(StartGameUISpawnedObject);
    }

    [ClientRpc]
    public void RpcDestoryCountdownTimerUI()
    {
        if (countdownTimerUISpawnedObject)
            NetworkServer.Destroy(countdownTimerUISpawnedObject);
    }

    [ClientRpc]
    public void RpcDestoryTurnTimerUI()
    {
        if (turnTimerUISpawnedObject)
            NetworkServer.Destroy(turnTimerUISpawnedObject);
    }

    [ClientRpc]
    public void RpcDestoryChangeTurnUI()
    {
        if (changeTurnUISpawnedObject)
            NetworkServer.Destroy(changeTurnUISpawnedObject);
    }


}
