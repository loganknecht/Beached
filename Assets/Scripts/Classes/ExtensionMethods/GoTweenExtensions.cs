﻿using UnityEngine;
using System.Collections;

public static class GoTweenExtensions {
    public static void AddGoTween(this GameObject currentGameObject, GoTween newGoTween) {
        GoTweenManager goTweenManager = currentGameObject.GetGoTweenManager();
        goTweenManager.Add(newGoTween);
    }
    public static GoTweenManager GetGoTweenManager(this GameObject currentGameObject) {
        GoTweenManager goTweenManager = currentGameObject.GetComponent<GoTweenManager>();
        if(goTweenManager == null) {
            goTweenManager = (currentGameObject.AddComponent<GoTweenManager>()).GetComponent<GoTweenManager>();
        }
        return goTweenManager;
    }
    public static GoTweenManager CompleteGoTweens(this GameObject currentGameObject) {
        GoTweenManager goTweenManager = currentGameObject.GetGoTweenManager();
        goTweenManager.CompleteTweens();
        return goTweenManager;
    }
    public static GoTweenManager DestroyGoTweens(this GameObject currentGameObject) {
        GoTweenManager goTweenManager = currentGameObject.GetGoTweenManager();
        goTweenManager.DestroyTweens();
        return goTweenManager;
    }
}
