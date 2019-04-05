using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void Puzzles();
    public static event Puzzles RunningPuzzles;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (RunningPuzzles != null) {
            RunningPuzzles();
        }

	}
}
