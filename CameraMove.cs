using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    [SerializeField]
    private DialogueTrigger dialogue;
    [SerializeField]
    private DialogueManager dialogueManager;
    public float zoomSensitivity;

    public bool debugMode = false;

    public Vector3 addToPosition;
    public Vector3 destPosition;

    public float smoothing;

    public float maxZoomOut;
    private bool startDialogue;

    private bool nextPuzzle;
    public int nextPuzzleTrigger;

    public DayManager dayManager;

    public float hintTriggerDistance;
    private bool triggeredHint;

    private void Start() {
        startDialogue = true;
        destPosition = transform.position;
        nextPuzzle = false;
        triggeredHint = false;
    }
    // Update is called once per frame
    void Update () {
        Cursor.lockState = CursorLockMode.Locked;
        if (!debugMode && transform.position.z >= maxZoomOut) {
            addToPosition = new Vector3(0, 0, Input.mouseScrollDelta.y * zoomSensitivity);
        } else if (transform.position.z >= maxZoomOut) {
            addToPosition = new Vector3(0, 0, GetZoomInt() * zoomSensitivity);
        } else addToPosition = new Vector3(0, 0, zoomSensitivity);
        destPosition += addToPosition * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, destPosition, Time.deltaTime * smoothing);

        if (dayManager.puzzlesCleared >= nextPuzzleTrigger) {
            nextPuzzle = true;
        }
        if (Input.GetKeyDown(KeyCode.Z) || transform.position.z < hintTriggerDistance) {
            Debug.Log("Button");
            triggeredHint = true;

        }
        if (triggeredHint && (Input.GetMouseButtonDown(0) || Input.GetButton("X Button"))) {
            if (!dialogueManager.animator.GetBool("IsOpen")) {
                if (startDialogue && !nextPuzzle) {
                    dialogue.TriggerDialogue(2);
                    triggeredHint = false;
                } else if (startDialogue && nextPuzzle) {
                    dialogue.TriggerDialogue(3);
                    triggeredHint = false;
                }
            }
            if (!startDialogue) dialogueManager.DisplayNextSentence();
            startDialogue = false;
            if (dialogueManager.animator.GetBool("IsOpen")) {
                startDialogue = true;
            }
        }
        if (startDialogue) {
            
        }
	}

    int GetZoomInt() {
        if (Input.GetButton("B Button")) {
            Debug.Log("1");
            return 1;
        } else if (Input.GetButton("A Button")) {
            Debug.Log("-1");
            return -1;
        } else return 0;
    }
}
