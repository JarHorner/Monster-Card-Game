using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DragDrop : NetworkBehaviour
{
    private GameObject Canvas;
    public PlayerManager playerManager;

    [SerializeField] private bool isDragging = false;
    private bool isDraggable = true;
    private GameObject startParent;
    private Vector2 startPosition;
    private GameObject dropZonePosition;
    private bool isOverDropzone;

    void Start()
    {
        Canvas = GameObject.Find("Main Canvas");

        if (!isOwned)
        {
            isDraggable = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        dropZonePosition = collision.gameObject;

        if (!DropZone.Instance.IsPositionFilled(dropZonePosition))
        {
            isOverDropzone = true;
        }
        else
        {
            dropZonePosition = null;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        dropZonePosition = collision.gameObject;

        if (!DropZone.Instance.IsPositionFilled(dropZonePosition))
        {
            isOverDropzone = true;
        }
        else
        {
            dropZonePosition = null;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropzone = false;
        dropZonePosition = null;
    }

    public void StartDrag()
    {
        if (!isDraggable) return;

        isDragging = true;

        startParent = transform.parent.gameObject;
        startPosition = transform.position;
    }

    public void EndDrag()
    {
        if (!isDraggable) return;

        isDragging = false;

        if (isOverDropzone && DropZone.Instance.GetPostions().Contains(dropZonePosition) && !DropZone.Instance.IsPositionFilled(dropZonePosition))
        {
            transform.SetParent(dropZonePosition.transform, false);
            DropZone.Instance.FillPosition(dropZonePosition);
            isDraggable = false;

            NetworkIdentity networkIdentity = NetworkClient.connection.identity;
            playerManager = networkIdentity.GetComponent<PlayerManager>();
            playerManager.PlayCard(gameObject, dropZonePosition);
            
        }
        else
        {
            transform.SetParent(startParent.transform, false);
            transform.position = startPosition;
        }
    }


    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }


}
