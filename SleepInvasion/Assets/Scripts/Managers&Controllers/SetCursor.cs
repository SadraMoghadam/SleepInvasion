using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    // You must set the cursor in the inspector.
    public Texture2D crosshair; 

    void Start()
    {
#if UNITY_STANDALONE_OSX && !UNITY_EDITOR
        Debug.Log("MAC OS BUILD");
#else
        //set the cursor origin to its centre. (default is upper left corner)
        Vector2 cursorOffset = new Vector2(crosshair.width/2, crosshair.height/2);
     
        //Sets the cursor to the Crosshair sprite with given offset 
        //and automatic switching to hardware default if necessary
        try
        {
            Cursor.SetCursor(crosshair, cursorOffset, CursorMode.Auto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
#endif
    }
}
