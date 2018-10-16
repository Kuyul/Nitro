using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternScript : MonoBehaviour {

    public Vector3 GetTopPositionOffset()
    {
        Vector3 Pos = new Vector3(0,0,0);
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name == "Top") {
                Pos = child.localPosition;
            }
        }
        return Pos;
    }

    public Vector3 GetBottomPositionOffset()
    {
        Vector3 Pos = new Vector3(0, 0, 0);
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.name == "Bottom")
            {
                Pos = child.localPosition;
            }
        }
        return Pos;
    }
}
