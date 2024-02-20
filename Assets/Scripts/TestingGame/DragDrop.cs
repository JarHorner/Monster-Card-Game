using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private GameObject Canvas;

    [SerializeField] private bool isDragging;
    private GameObject startParent;
    private Vector2 startPosition;
    private GameObject dropZonePosition;
    private bool isOverDropzone;

    void Start()
    {
        Canvas = GameObject.Find("Main Canvas");
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
        isDragging = true;

        startParent = transform.parent.gameObject;
        startPosition = transform.position;
    }

    public void EndDrag()
    {
        isDragging = false;

        if (isOverDropzone && DropZone.Instance.GetPostions().Contains(dropZonePosition) && !DropZone.Instance.IsPositionFilled(dropZonePosition))
        {
            transform.SetParent(dropZonePosition.transform, false);
            DropZone.Instance.FillPosition(dropZonePosition);
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
