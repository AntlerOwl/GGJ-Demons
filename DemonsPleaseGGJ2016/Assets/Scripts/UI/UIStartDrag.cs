using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIStartDrag : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public Sprite image;

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // start mouse follow
        if (image)
        {
            UIDragManager.instance.SetPosition();
        }
//        OnDrag();
    }

//    protected virtual void OnDrag() { }
    protected virtual void OnBeginDrag() { }
}