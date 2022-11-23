using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public abstract class Base_UI : MonoBehaviour
{
    public enum UIEvent
    {
        Click,
        Enter,
        Exit,
        Up
    }

    public static void BindEvent(GameObject uiObject, Action<PointerEventData> action, UIEvent type = UIEvent.Click)
    {
        UIPointerHandler evt = GetAddedComponent<UIPointerHandler>(uiObject);

        switch (type)
        {
            case UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;

            //����� Ŭ�� �̺�Ʈ�� ����
            //case UIEvent.Enter:
            //    evt.OnEnterHandler -= action;
            //    evt.OnEnterHandler += action;
            //    break;
            //case UIEvent.Exit:
            //    evt.OnExitHandler -= action;
            //    evt.OnExitHandler += action;
            //    break;
            //case UIEvent.Up:
            //    evt.OnUpHandler -= action;
            //    evt.OnUpHandler += action;
            //    break;
        }
    }

    public static T GetAddedComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }
}