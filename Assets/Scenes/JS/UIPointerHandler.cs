using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPointerHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    // 각 이벤트에 대한 델리게이트
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnEnterHandler = null;
    public Action<PointerEventData> OnExitHandler = null;
    public Action<PointerEventData> OnUpHandler = null;

    // 클릭 이벤트
    public void OnPointerClick(PointerEventData eventData)      //null이 아니면 실행.
        => OnClickHandler?.Invoke(eventData);

    // 마우스 오버 이벤트 (마우스가 버튼 위에 올라갈 때 실행)
    public void OnPointerEnter(PointerEventData eventData)
        => OnEnterHandler?.Invoke(eventData);

    // 마우스 오버 종료 (마우스가 버튼을 벗어날 때 실행)
    public void OnPointerExit(PointerEventData eventData)
        => OnExitHandler?.Invoke(eventData);

    // 마우스 클릭(또는 드래그)가 끝난 시점에 실행
    public void OnPointerUp(PointerEventData eventData)
        => OnUpHandler?.Invoke(eventData);
}
