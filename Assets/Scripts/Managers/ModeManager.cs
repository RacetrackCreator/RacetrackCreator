using System;
using UnityEngine;

namespace Managers
{
    public enum Mode
    {
        Selecting,
        ControlPoint,
        
    }
    
    public class ModeManager : MonoBehaviour
    {
        
        private GameObject _sphere;

        public float snapDistance = 50;
        private void Start()
        {
            _sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        private void Update()
        {
            int? closest = NodeManager.GetClosestNode(Input.mousePosition, out float d);
            if (closest != null)
            {
                if (d < snapDistance) {
                    _sphere.SetActive(true);
                    int a = (int) closest;
                    Vector3 pos = NodeManager.Instance.Get(a).pos;
                    _sphere.transform.position = pos;
                }
                else
                {
                    _sphere.SetActive(false);
                }
            }
        }
    }
}