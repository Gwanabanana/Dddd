using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] GameObject cursor;
    Camera myCamera;

    float displayMulti = 9f / 2.75f;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = transform.GetComponent<Camera>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    void FollowMouse()
    {
        Vector3 target = myCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        
        cursor.transform.position = new Vector2(-target.x * displayMulti, target.y * displayMulti); //Because Display is inverted

    }

    public Vector2 GetCursorPosition()
    {
        return cursor.transform.position;
    }
}
