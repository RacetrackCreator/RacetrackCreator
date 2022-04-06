using Managers;
using Networks.Road;
using UnityEngine;

public class SegmentObject : MonoBehaviour
{
    private Segment s;

    // Start is called before the first frame update
    void Start()
    {
        int start = NodeManager.Instance.Push(new Node(new Vector3(-1, 0, -1), new Vector3(-1, 0, 1)));
        int end = NodeManager.Instance.Push(new Node(new Vector3(1, 0, -1), new Vector3(1, 0, 1)));
        s = new Segment(start, end, new Vector3(0, -1, 2));
        GetComponent<MeshFilter>().mesh = s.BuildMesh();
    }

    // Update is called once per frame
    void Update()
    {
    }
}