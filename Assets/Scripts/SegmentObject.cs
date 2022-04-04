using System;
using System.Collections;
using System.Collections.Generic;
using Networks.Road;
using UnityEngine;

public class SegmentObject : MonoBehaviour
{
    private Segment s;
    // Start is called before the first frame update
    void Start()
    {
        
        s = new Segment(new Node(new Vector3(-1,0,-1), new Vector3(-1,0,1), 0), 
            new Node(new Vector3(1, 0, -1), new Vector3(1, 0, 1), 1), new Vector3(0,-1,2));
        GetComponent<MeshFilter>().mesh = s.BuildMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
