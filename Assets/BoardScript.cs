using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0)) {
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            if(clickPosition.y < 4.0 && clickPosition.y > -4.0 && clickPosition.x > -4.0 && clickPosition.x < 4.0) {
                int clickX = Mathf.FloorToInt(clickPosition.x) + 4;
                int clickY = Mathf.FloorToInt(clickPosition.y) + 4;
                print(clickX + " " + clickY);
            }
            
        }
	}
}
