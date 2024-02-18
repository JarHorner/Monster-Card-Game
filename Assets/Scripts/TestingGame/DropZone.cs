using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{

    public static DropZone Instance;

    [SerializeField] private List<GameObject> postions;
    [SerializeField] private List<bool> postionsFilled;

    private void Awake()
    {
        Instance = this;
    }

    public List<GameObject> GetPostions()
    {
        return postions;
    }

    public bool IsPositionFilled(GameObject go)
    {
        int positionIndex = postions.IndexOf(go);
        return postionsFilled[positionIndex-1];
    }

    public void FillPosition(GameObject go)
    {
        int positionIndex = postions.IndexOf(go);
        postionsFilled[positionIndex-1] = true;
    }


}
