using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    public void OnClick()
    {
        Debug.Log("OnClick()");
        FadeManager.FadeOut(1,1.0f);
    }
}