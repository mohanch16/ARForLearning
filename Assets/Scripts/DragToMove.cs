using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToMove : MonoBehaviour
{
    private Touch touch;

    private float speedModifier; // controls how fast a model moves

    // Start is called before the first frame update
    void Start()
    {
        this.speedModifier = 0.001f; // increase this value to make the object move faster.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) // when a finger is touching the screen
        {
            this.touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved) // when a gesture is performed
            {
                // changes position of object in each frame. 
                // original x value + how much x value has changes in that frame * speedModifier, keeps original y, same as x except for z value of object and y value of the finger. 
                // deltaposition is the position of finger on the screen (2D - only have x & y).
                this.transform.position = new Vector3(this.transform.position.x + this.touch.deltaPosition.x * this.speedModifier, transform.position.y, this.transform.position.z + this.touch.deltaPosition.y * this.speedModifier);
            } 
        }
    }
}
