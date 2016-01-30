using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UIEndDrag : MonoBehaviour, IDropHandler, IEndDragHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // stop mouse follow
        print("drop");
        OnEndDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UIDragManager.instance.EndDrag();
    }

    protected virtual void OnEndDrag() { }
}
