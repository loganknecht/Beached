﻿using UnityEngine;
using System.Collections;

public class CinematicDetonatorEvent : CustomEventsManager {
    public CinematicDetonator detonator = null;
    public bool finishedCinematic = true;
    
    // protected override void Awake() {
    //     base.Awake();
    // }
    
    // // Use this for initialization
    // protected override void Start() {
    //     base.Start();
    // }
    
    // // Update is called once per frame
    // protected override void Update() {
    //     base.Update();
    // }
    
    protected override void Initialize() {
        base.Initialize();
        if(detonator == null) {
            Debug.LogError("The 'detonator' object must be set ahead of time for the object: " + this.gameObject.name);
        }
    }
    
    public override void Execute() {
        detonator.finishedCinematic = finishedCinematic;
    }
}