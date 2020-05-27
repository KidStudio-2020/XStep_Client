﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WidgetViewer : MonoBehaviour
{
    public void WidgetsOpen(Image parentWidget, params object[] childWidgets){
        StartCoroutine(WidgetsOpenCoroutine(parentWidget, childWidgets));
    } 

    public void WidgetsClose(Image parentWidget, Action closeAction, params object[] childWidgets){
        StartCoroutine(WidgetsCloseCoroutine(parentWidget, closeAction, childWidgets));
    }

    private IEnumerator WidgetsOpenCoroutine(Image parentWidget, params object[] childWidgets){
        yield return StartCoroutine(ScaleUpParentWidget(parentWidget));
        FadeInChildWidgets(childWidgets);
    }

    private IEnumerator WidgetsCloseCoroutine(Image parentWidget, Action closeAction, params object[] childWidgets){
        yield return StartCoroutine(FadeOutChildWidgets(childWidgets));
        yield return StartCoroutine(ScaleDownParenWidget(parentWidget));
        closeAction();
    }

    private IEnumerator ScaleUpParentWidget(Image parentWidget){
        Vector3 upperVector = Vector3.up / 60;
        for(int i = 0; i < 60; i++){
            parentWidget.gameObject.transform.localScale += upperVector;
            yield return YieldInstructionCache.WaitFrame;
        }
    }

    private IEnumerator ScaleDownParenWidget(Image parentWidget){
        Vector3 lowerVector = Vector3.up / 60;
        for(int i = 0; i < 60; i++){
            parentWidget.gameObject.transform.localScale -= lowerVector;
            yield return YieldInstructionCache.WaitFrame;
        }
    }

    private void FadeInChildWidgets(params object[] childWidgets){
        for(int i = 0; i < childWidgets.Length; i++){
            if(childWidgets[i] is Image image){
                StartCoroutine(GameManager.instance.fadeManager.ImageFadeIn(image, 0.5f));
            }
            else if(childWidgets[i] is Text text){
                StartCoroutine(GameManager.instance.fadeManager.TextFadeIn(text, 0.5f));
            }
        }
    }


    // FIXME : 영 코드가 안 이쁨
    // THINK : CustomYieldInstruction 만드는 거 생각해 보기
    private IEnumerator FadeOutChildWidgets(params object[] childWidgets){
        Image image;
        Text text;

        for(int i = 0; i < childWidgets.Length - 1; i++){
            if(image = childWidgets[i] as Image){
                StartCoroutine(GameManager.instance.fadeManager.ImageFadeOut(image, 0.5f));
            }
            else if(text = childWidgets[i] as Text){
                StartCoroutine(GameManager.instance.fadeManager.TextFadeOut(text, 0.5f));
            }
        }

        if(image = childWidgets[childWidgets.Length - 1] as Image){
            yield return StartCoroutine(GameManager.instance.fadeManager.ImageFadeOut(image, 0.5f));
        }
        else if(text = childWidgets[childWidgets.Length - 1] as Text){
            yield return StartCoroutine(GameManager.instance.fadeManager.TextFadeOut(text, 0.5f));
        }

    }
}
