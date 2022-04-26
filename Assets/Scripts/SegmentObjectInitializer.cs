using Managers;
using Networks.Road;
using UnityEngine;
using UnityEngine.Serialization;

public class SegmentObjectInitializer : MonoBehaviour
{
    private int _s;
    public Material m;

    // Start is called before the first frame update
    void Start()
    {
        int start = NodeManager.Instance.Push(new Node(new Vector3(-1, 0, -1), new Vector3(-1, 0, 1)));
        int end = NodeManager.Instance.Push(new Node(new Vector3(1, 0, -1), new Vector3(1, 0, 1)));
        _s = SegmentManager.Add(start, end, new Vector3(0, 0, -1), new Vector3(0,0,1), m);
    }


    // Update is called once per frame
    // void Update()
    // {
    //     
    // }
}