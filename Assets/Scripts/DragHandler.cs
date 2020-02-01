using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform outline;
    public float threshold = 10f;
    public Vector3 originalPosition;
    public minigameLogic minigameLogic;
    public void OnDrag(PointerEventData eventData)
    {
        //transform.localPosition = (Vector3)Input.mousePosition;
        //transform.localPosition = Input.mousePosition- new Vector3(1920/2,1080/2,0);
        //transform.localPosition = Input.mousePosition - new Vector3 (Screen.currentResolution.width/2, Screen.currentResolution.height / 2,0);
        transform.localPosition += (Vector3)eventData.delta;
        //Debug.Log(Input.mousePosition);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if((outline.localPosition - transform.localPosition).magnitude < threshold)
        {
            transform.localPosition = outline.localPosition;
            this.enabled = false;
            minigameLogic.PartCorrectlyPlaced();
            return;
        }
        transform.localPosition = originalPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
    }
}
