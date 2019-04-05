using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorTracker : MonoBehaviour {
    public AudioSource impactSource;
    public DayManager dayManager;
    public GameObject particle;
    public GameObject go;
    public GameObject vulcano;
    private bool stayAtPlace;
    private float waitForKinematic = 1f;
    public GameObject vulcanoMeteorite;
    public Material meteorNoOutline;
    public AudioSource geiserSound;
    // Use this for initialization
    void Start () {
        stayAtPlace = false;
        impactSource = this.GetComponent<AudioSource>();
        if (this.GetComponent<MeshRenderer>() != null && this.GetComponent<SphereCollider>() != null) {
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<SphereCollider>().enabled = true;
        }
        vulcano.SetActive(false);
        vulcanoMeteorite.SetActive(false);
        geiserSound.Play();
        geiserSound.pitch = Random.Range(0.8f, 1f);
    }
	
	// Update is called once per frame
	void Update () {
        if (stayAtPlace) {
            StayLanded();
            waitForKinematic -= Time.deltaTime;
            if (waitForKinematic <= 0) {
                go.transform.parent.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
	}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Meteorite" && !stayAtPlace) {
            Debug.Log("Landed");
            Debug.Log(go);
            go = other.gameObject;
            this.GetComponent<SphereCollider>().enabled = false;
            impactSource.Play();
            impactSource.pitch = Random.Range(0.7f, 1.1f);
            particle.SetActive(false);
            dayManager.puzzlesCleared += 1;
            stayAtPlace = true;
            if (dayManager.puzzlesCleared >= 3) {
                 vulcano.SetActive(true);
                vulcanoMeteorite.SetActive(true);
            }
            
        }
    }

    void StayLanded() {
        go.GetComponent<Renderer>().material = meteorNoOutline;
        go.transform.GetComponentInParent<PlanarGravity>().enabled = false;
        go.transform.position = Vector3.Lerp(go.transform.position, transform.position, Time.deltaTime*10);
        go.transform.rotation = Quaternion.Slerp(go.transform.rotation, transform.rotation, Time.deltaTime * 10);
        go.transform.parent.SetParent(this.transform);
        geiserSound.Stop();
            }
}
