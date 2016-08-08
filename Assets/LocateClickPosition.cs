using UnityEngine;
using System.Collections;

public class LocateClickPosition : MonoBehaviour {

    Vector3 mousePos;
   public  bool clickedRightSide = false;
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {          
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos.x >gameObject.transform.position.x)
            {
                clickedRightSide = true;
                Debug.Log("Right");
            }
            else
            {
                clickedRightSide = false;
                Debug.Log("Left");
          }  
        }
	
	}
}
