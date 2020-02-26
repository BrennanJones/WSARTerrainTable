using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTeamIcon : MonoBehaviour {

    public Color color;

    // Use this for initialization
	void Start () {
        this.GetComponent<Renderer>().material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
