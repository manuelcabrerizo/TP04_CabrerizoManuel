using UnityEngine;
using UnityEngine.EventSystems;

public class UIOnEnter : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.PlaySelectSound();
    }


}
