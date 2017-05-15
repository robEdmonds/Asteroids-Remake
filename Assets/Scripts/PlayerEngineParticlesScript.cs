using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEngineParticlesScript : MonoBehaviour {

    public AudioController backEngineAudio;
    public AudioController rightEngineAudio;
    public AudioController leftEngineAudio;

    public ParticleSystem backJetCoreParticles;
    public ParticleSystem backJetFlareParticles;

    public ParticleSystem rightJetCoreParticles;
    public ParticleSystem rightJetFlareParticles;

    public ParticleSystem leftJetCoreParticles;
    public ParticleSystem leftJetFlareParticles;

    private float backJetCoreMinSize;
    private float backJetCoreMaxSize;
    private float backJetFlareMinSize;
    private float backJetFlareMaxSize;

    private float rightJetCoreMinSize;
    private float rightJetCoreMaxSize;
    private float rightJetFlareMinSize;
    private float rightJetFlareMaxSize;

    private float leftJetCoreMinSize;
    private float leftJetCoreMaxSize;
    private float leftJetFlareMinSize;
    private float leftJetFlareMaxSize;

    // Use this for initialization
    void Start () {
        
        /* Get the size of the engine particles */
        // Back engine
        backJetCoreMinSize = backJetCoreParticles.main.startSize.constantMin;
        backJetCoreMaxSize = backJetCoreParticles.main.startSize.constantMax;
        backJetFlareMinSize = backJetFlareParticles.main.startSize.constantMin;
        backJetFlareMaxSize = backJetFlareParticles.main.startSize.constantMax;
        // Right engine
        rightJetCoreMinSize = rightJetCoreParticles.main.startSize.constantMin;
        rightJetCoreMaxSize = rightJetCoreParticles.main.startSize.constantMax;
        rightJetFlareMinSize = rightJetFlareParticles.main.startSize.constantMin;
        rightJetFlareMaxSize = rightJetFlareParticles.main.startSize.constantMax;
        // Left engine
        leftJetCoreMinSize = leftJetCoreParticles.main.startSize.constantMin;
        leftJetCoreMaxSize = leftJetCoreParticles.main.startSize.constantMax;
        leftJetFlareMinSize = leftJetFlareParticles.main.startSize.constantMin;
        leftJetFlareMaxSize = leftJetFlareParticles.main.startSize.constantMax;

        Update();

        backJetCoreParticles.gameObject.SetActive(true);
        backJetFlareParticles.gameObject.SetActive(true);
        rightJetCoreParticles.gameObject.SetActive(true);
        rightJetFlareParticles.gameObject.SetActive(true);
        leftJetCoreParticles.gameObject.SetActive(true);
        leftJetFlareParticles.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {

        float backEngineMultiplier = 0.0f;
        float rightEngineMultiplier = 0.0f;
        float leftEngineMultiplier = 0.0f;

        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");

        // Moving Forward
        if (Input.GetAxis("Vertical") >= 0.0f)
        {
            // Determine the size of the back engine particles 
            backEngineMultiplier += vAxis * 0.65f;
        }
        // Moving Backward
        else
        {
            // Determine the size of the left and right engine particles 
            rightEngineMultiplier += -vAxis * 0.6f;
            leftEngineMultiplier += -vAxis * 0.6f;
        }

        // Turning Right
        if (Input.GetAxis("Horizontal") > 0.0f)
        {
            // Determine the size of the right engine particles 
            leftEngineMultiplier += hAxis * 0.4f;
            backEngineMultiplier += hAxis * 0.35f;
        }
        // Turning Left
        else
        {
            // Determine the size of the left engine particles 
            rightEngineMultiplier += -hAxis * 0.4f;
            backEngineMultiplier += -hAxis * 0.35f;
        }

        // Control the size of the back engine particles 
        ParticleSystem.MainModule backJetFlareMain = backJetFlareParticles.main;
        ParticleSystem.MinMaxCurve backJetFlareCurve = new ParticleSystem.MinMaxCurve(backEngineMultiplier * backJetFlareMinSize, backEngineMultiplier * backJetFlareMaxSize);
        backJetFlareMain.startSize = backJetFlareCurve;

        ParticleSystem.MainModule backJetCoreMain = backJetCoreParticles.main;
        ParticleSystem.MinMaxCurve backJetCoreCurve = new ParticleSystem.MinMaxCurve(backEngineMultiplier * backJetCoreMinSize, backEngineMultiplier * backJetCoreMaxSize);
        backJetCoreMain.startSize = backJetCoreCurve;

        // Control the size of the right engine particles 
        ParticleSystem.MainModule rightJetFlareMain = rightJetFlareParticles.main;
        ParticleSystem.MinMaxCurve rightJetFlareCurve = new ParticleSystem.MinMaxCurve(rightEngineMultiplier * rightJetFlareMinSize, rightEngineMultiplier * rightJetFlareMaxSize);
        rightJetFlareMain.startSize = rightJetFlareCurve;

        ParticleSystem.MainModule rightJetCoreMain = rightJetCoreParticles.main;
        ParticleSystem.MinMaxCurve rightJetCoreCurve = new ParticleSystem.MinMaxCurve(rightEngineMultiplier * rightJetCoreMinSize, rightEngineMultiplier * rightJetCoreMaxSize);
        rightJetCoreMain.startSize = rightJetCoreCurve;

        // Control the size of the left engine particles 
        ParticleSystem.MainModule leftJetFlareMain = leftJetFlareParticles.main;
        ParticleSystem.MinMaxCurve leftJetFlareCurve = new ParticleSystem.MinMaxCurve(leftEngineMultiplier * leftJetFlareMinSize, leftEngineMultiplier * leftJetFlareMaxSize);
        leftJetFlareMain.startSize = leftJetFlareCurve;

        ParticleSystem.MainModule leftJetCoreMain = leftJetCoreParticles.main;
        ParticleSystem.MinMaxCurve leftJetCoreCurve = new ParticleSystem.MinMaxCurve(leftEngineMultiplier * leftJetCoreMinSize, leftEngineMultiplier * leftJetCoreMaxSize);
        leftJetCoreMain.startSize = leftJetCoreCurve;

        //set engine audio volume
        backEngineAudio.SetVolume(backEngineMultiplier);
        rightEngineAudio.SetVolume(rightEngineMultiplier);
        leftEngineAudio.SetVolume(leftEngineMultiplier);
    }      
}
