﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubbleValuesEvent : CustomEventObject {
    public SpeechBubble speechBubble = null;
    public SpeechBubbleImage speechBubbleImage = SpeechBubbleImage.None;
    public List<string> textSet = null;
    public float duration = 1f;
    public bool isEnabled = true;
    public bool show = false;
    public bool hide = false;
    public bool startInteraction = false;
    public bool finishInteraction = false;
    public List<CustomEventsManager> onSpeechBubbleFinish = null;
    
    // // Use this for initialization
    // protected override void Awake(){
    // base.Awake();
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    // base.Start();
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    // base.Update();
    // }
    
    // // public override void Execute() {
    // base.Execute();
    // }
    
    protected override void Initialize() {
        base.Initialize();
        if(speechBubble == null) {
            this.gameObject.LogComponentError("speechBubble", this.GetType());
        }
        if(textSet == null) {
            textSet = new List<string>();
        }
    }
    
    public override void ExecuteLogic() {
        SetSpeechBubbleValues();
    }
    
    public void SetSpeechBubbleValues() {
        //------------------------------
        // Enable/Disable
        //------------------------------
        // this needs to happen first because the speech bubble has logic based
        // on if it's enabled or not
        speechBubble.enabled = isEnabled;
        
        //------------------------------
        // SpeechBubbleImage
        //------------------------------
        speechBubble.SetSpeechBubbleImage(speechBubbleImage);
        
        //------------------------------
        // Text Set
        //------------------------------
        if(textSet.Count > 0) {
            speechBubble.SetTextSet(textSet.ToArray());
        }
        
        //------------------------------
        // Show/Hide
        //------------------------------
        if(show
            && hide) {
            Debug.LogError("Show and hide cannot BOTH be TRUE. Please fix this on " + this.gameObject.name);
        }
        else if(show) {
            speechBubble.Show(duration);
        }
        else if(hide) {
            speechBubble.Hide(duration);
        }
        
        if(startInteraction) {
            speechBubble.StartInteraction();
        }
        if(finishInteraction) {
            speechBubble.FinishInteraction();
        }
        
        //------------------------------
        // Speech Bubble - On Finish
        //------------------------------
        if(onSpeechBubbleFinish != null) {
            // ToArray() is used to get a copy so that this doesn't trip over itself recursively
            // when it modifies the finish
            foreach(CustomEventsManager customEventsManager in onSpeechBubbleFinish.ToArray()) {
                speechBubble.onFinishInteraction.Add(customEventsManager);
            }
        }
    }
}