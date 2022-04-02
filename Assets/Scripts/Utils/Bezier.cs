using UnityEngine;

namespace Utils
{
    public class Bezier
    {
        private Vector3 a;
        private Vector3 b;
        private Vector3 ctrl;

        public Bezier(Vector3 a, Vector3 ctrl, Vector3 b)
        {
            this.a = a;
            this.b = b;
            this.ctrl = ctrl;
        }

        public bool IsLinear()
        {
            return (a - b).normalized == (a - ctrl).normalized;
        }

        public static Vector3 GetLinearPoint(Vector3 a, Vector3 b, float t)
        {
            return a + (b - a) * t;
        }

        public static Vector3 GetCuadraticPoint(Vector3 a, Vector3 ctrl, Vector3 b, float t)
        {
            Vector3 c = GetLinearPoint(a, ctrl, t);
            Vector3 d = GetLinearPoint(ctrl, b, t);
            return GetLinearPoint(c, d, t);
        }

        public Vector3 GetIntermediate(float t)
        {
            if (IsLinear())
            {
                return GetLinearPoint(a, b, t);
            }
            else
            {
                return GetCuadraticPoint(a, ctrl, b, t);
            }
        }
    }
}