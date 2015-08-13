﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubbleInteractionTrigger : InteractionTrigger {
    public List<SpeechBubbleTextSet> textSets = null;
    
    protected override void Awake() {
        Initialize();
    }
    
    // Use this for initialization
    protected override void Start() {
    }
    
    // Update is called once per frame
    protected override void Update() {
    }
    
    protected override void Initialize() {
    }
    
    public virtual void SetTextSets(List<SpeechBubbleTextSet> newTextSets) {
        textSets = newTextSets;
    }
    
    public virtual SpeechBubbleTextSet PopNextTextSet() {
        SpeechBubbleTextSet textSetToReturn = null;
        if(textSets.Count > 0) {
            textSetToReturn = textSets[0];
            //
            textSets.Remove(textSetToReturn);
            textSets.Add(textSetToReturn);
        }
        return textSetToReturn;
    }
    
    public override void Entered(GameObject gameObjectEntering) {
        // Debug.Log("SpeechBubble Trigger Entered");
        ShowSpeechBubble(0.25f);
    }
    
    public override void Exited(GameObject gameObjectExiting) {
        // Debug.Log("SpeechBubble Trigger Exited");
        HideSpeechBubble(0.25f);
    }
    
    public override void Execute(GameObject gameObjectToExecute) {
        // Perform only if it's the first iteration, or it should loop
        if(currentIteration < 1
            || loop) {
            ExecuteLogic(gameObjectToExecute);
            // This isn't used in the execute logic, this is iterated in the ExecuteLogic function
            // currentIteration++;
            
            if(loop == false) {
                this.GetComponent<Collider2D>().enabled = false;
            }
        }
    }
    
    public override void ExecuteLogic(GameObject gameObjectExecuting) {
        // Debug.Log("SpeechBubble Triggered Interaction");
        Player player = gameObjectExecuting.GetComponent<Player>();
        if(player != null
            && speechBubble != null) {
            // Starts the speech bubble
            if(!speechBubble.IsInUse()) {
                speechBubble.SetTextSet(PopNextTextSet().GetTextSet().ToArray());
                
                player.StartInteraction();
                speechBubble.StartInteraction();
            }
            else {
                // Ends the speech bubble
                if(speechBubble.HasFinished()) {
                    // Start player gamestate first so that if events are fired
                    // at the end of the speech bubble they can override any
                    // changes that could occur in the player's state
                    player.StartGameplayState();
                    speechBubble.FinishInteraction();
                    
                    currentIteration++;
                }
                // Proceeds in the speech bubble
                else {
                    speechBubble.MoveToNextText();
                }
            }
        }
    }
}
