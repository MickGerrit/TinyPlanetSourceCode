using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//put this script on a parent object that has a child that needs to rotate
public class PlanetRotationV3 : ArduinoManager {
    public float speed;
    public Vector3 onButtonGyroRot;
    public Vector3 lastRot;
    public Vector3 wantedRot;
    public bool setLastRot;
    public bool wind = false;
    private GameObject planet;
    private GameObject mic;

    private bool OnButtonDown;

    
    private void Start() {
        planet = transform.GetChild(0).gameObject;
        if (mic == null) {
            mic = transform.GetChild(1).gameObject;
        }

        transform.DetachChildren();
        if (mic != null) mic.transform.parent = this.gameObject.transform;
    }

    // Update is called once per frame
    void Update() {
        

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler
        (arduinoInput.gyroRot.x, -arduinoInput.gyroRot.z, arduinoInput.gyroRot.y), Time.deltaTime * speed);

        if (!arduinoInput.button0 || !Input.GetButton("Button 4")) {
            OnButtonDown = true;
            transform.DetachChildren();
            if (mic != null)  mic.transform.parent = this.gameObject.transform;
            Debug.Log("No Button Pressed");
        }

        if (Input.GetButton("Button 4")) Debug.Log("Button 4");
        if (Input.GetButton("Button 5")) Debug.Log("Button 5");

        if (arduinoInput.button0 || Input.GetButton("Button 4")) {
            if (OnButtonDown) {
                planet.transform.parent = this.gameObject.transform;
                Debug.Log("Button Press Down");
                OnButtonDown = false;
            }
        }

    }
}
