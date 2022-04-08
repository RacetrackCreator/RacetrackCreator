using Managers;
using Networks.Road;
using UnityEngine;
using UnityEngine.Serialization;

public class SegmentObject : MonoBehaviour
{
    private Segment _s;

    

    // Start is called before the first frame update
    void Start()
    {
        int start = NodeManager.Instance.Push(new Node(new Vector3(-1, 0, -1), new Vector3(-1, 0, 1)));
        int end = NodeManager.Instance.Push(new Node(new Vector3(1, 0, -1), new Vector3(1, 0, 1)));
        _s = new Segment(start, end, new Vector3(0, -1, 2));
        transform.Translate(_s.Pos, Space.World);
        GetComponent<MeshFilter>().mesh = _s.BuildMesh();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}