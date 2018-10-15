using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{

    // The target marker.
    public Transform LeaderSphere;
    public Transform[] FollowerSpheres;
    public Transform[] target;
    public float speed;
    public PathTypes PathType;

    private float step;
    private int Pathindex;
    private List<List<Vector3>> ListOfStoredPositions;

    #region Enums
    public enum PathTypes //Types of movement paths
    {
        linear,
        loop
    }
    #endregion //Enums

    // Use this for initialization
    void Start()
    {
        step = Time.deltaTime * speed;
        Pathindex = 0;
        //Populate path storage
        ListOfStoredPositions = new List<List<Vector3>>();
        for (int i = 0; i < FollowerSpheres.Length; i++)
        {
            ListOfStoredPositions.Add(new List<Vector3>());
        }
    }

    void Update()
    {
        //if the number of follower spheres have not been set, return

        //Follow the leader sphere
        ListOfStoredPositions[0].Add(LeaderSphere.localPosition);
        LeaderSphere.localPosition = Vector3.MoveTowards(LeaderSphere.localPosition, target[Pathindex].localPosition, step);

        if (LeaderSphere.localPosition == target[Pathindex].localPosition)
        {
            Pathindex++;
        }

        if (Pathindex >= target.Length)
        {
            if (PathType == PathTypes.loop)
            {
                Pathindex = 0;
            }
            else
            {
                Pathindex--;//Dummy
            }
        }
    }
}
