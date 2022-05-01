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

        /// <summary>
        ///  Calculates the closest point in the bezier to exterior
        /// </summary>
        /// <param name="exterior">Exterior point</param>
        public void GetClosestPoints(Vector3 exterior, out Vector3 sol1, out Vector3 sol2, out Vector3 sol3)
        {
            float a = this.a.x;
            float b = this.a.y;
            float c = this.a.z;

            float d = this.b.x;
            float e = this.b.y;
            float f = this.b.z;

            float g = this.ctrl.x;
            float h = this.ctrl.y;
            float i = this.ctrl.z;

            float j = exterior.x;
            float k = exterior.y;
            float l = exterior.z;

            float m = 2 * g - 2 * a;
            float n = 2 * h - 2 * b;
            float o = 2 * i - 2 * c;

            float p = a + d - 2 * g;
            float q = p + e - 2 * h;
            float r = c + f - 2 * i;

            // Solution 1
            float x1 = -(m * p + n * q + o * r) / (2 * (Mathf.Pow(p, 2) + Mathf.Pow(q, 2) + Mathf.Pow(r, 2))) +
                       Mathf.Pow(
                           (-108 * b * n * Mathf.Pow(p, 4) + 108 * k * n * Mathf.Pow(p, 4) -
                               108 * c * o * Mathf.Pow(p, 4) +
                               108 * l * o * Mathf.Pow(p, 4) + 54 * m * Mathf.Pow(n, 2) * Mathf.Pow(p, 3) +
                               54 * m * Mathf.Pow(o, 2) * Mathf.Pow(p, 3) + 108 * b * m * q * Mathf.Pow(p, 3) -
                               108 * k * m * q * Mathf.Pow(p, 3) + 108 * a * n * q * Mathf.Pow(p, 3) -
                               108 * j * n * q * Mathf.Pow(p, 3) + 108 * c * m * r * Mathf.Pow(p, 3) -
                               108 * l * m * r * Mathf.Pow(p, 3) + 108 * a * o * r * Mathf.Pow(p, 3) -
                               108 * j * o * r * Mathf.Pow(p, 3) - 108 * a * m * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                               108 * j * m * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                               108 * b * n * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                               108 * k * n * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                               216 * c * o * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                               216 * l * o * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                               108 * a * m * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                               108 * j * m * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) -
                               216 * b * n * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                               216 * k * n * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) -
                               108 * c * o * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                               108 * l * o * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                               54 * Mathf.Pow(n, 3) * q * Mathf.Pow(p, 2) +
                               54 * n * Mathf.Pow(o, 2) * q * Mathf.Pow(p, 2) -
                               108 * Mathf.Pow(m, 2) * n * q * Mathf.Pow(p, 2) +
                               54 * Mathf.Pow(o, 3) * r * Mathf.Pow(p, 2) -
                               108 * Mathf.Pow(m, 2) * o * r * Mathf.Pow(p, 2) +
                               54 * Mathf.Pow(n, 2) * o * r * Mathf.Pow(p, 2) + 108 * c * n * q * r * Mathf.Pow(p, 2) -
                               108 * l * n * q * r * Mathf.Pow(p, 2) + 108 * b * o * q * r * Mathf.Pow(p, 2) -
                               108 * k * o * q * r * Mathf.Pow(p, 2) + 108 * b * m * Mathf.Pow(q, 3) * p -
                               108 * k * m * Mathf.Pow(q, 3) * p + 108 * a * n * Mathf.Pow(q, 3) * p -
                               108 * j * n * Mathf.Pow(q, 3) * p + 108 * c * m * Mathf.Pow(r, 3) * p -
                               108 * l * m * Mathf.Pow(r, 3) * p + 108 * a * o * Mathf.Pow(r, 3) * p -
                               108 * j * o * Mathf.Pow(r, 3) * p + 54 * Mathf.Pow(m, 3) * Mathf.Pow(q, 2) * p -
                               108 * m * Mathf.Pow(n, 2) * Mathf.Pow(q, 2) * p +
                               54 * m * Mathf.Pow(o, 2) * Mathf.Pow(q, 2) * p +
                               54 * Mathf.Pow(m, 3) * Mathf.Pow(r, 2) * p +
                               54 * m * Mathf.Pow(n, 2) * Mathf.Pow(r, 2) * p -
                               108 * m * Mathf.Pow(o, 2) * Mathf.Pow(r, 2) * p + 108 * b * m * q * Mathf.Pow(r, 2) * p -
                               108 * k * m * q * Mathf.Pow(r, 2) * p + 108 * a * n * q * Mathf.Pow(r, 2) * p -
                               108 * j * n * q * Mathf.Pow(r, 2) * p + 108 * c * m * Mathf.Pow(q, 2) * r * p -
                               108 * l * m * Mathf.Pow(q, 2) * r * p + 108 * a * o * Mathf.Pow(q, 2) * r * p -
                               108 * j * o * Mathf.Pow(q, 2) * r * p - 324 * m * n * o * q * r * p -
                               108 * a * m * Mathf.Pow(q, 4) + 108 * j * m * Mathf.Pow(q, 4) -
                               108 * c * o * Mathf.Pow(q, 4) +
                               108 * l * o * Mathf.Pow(q, 4) - 108 * a * m * Mathf.Pow(r, 4) +
                               108 * j * m * Mathf.Pow(r, 4) -
                               108 * b * n * Mathf.Pow(r, 4) + 108 * k * n * Mathf.Pow(r, 4) +
                               54 * n * Mathf.Pow(o, 2) * Mathf.Pow(q, 3) + 54 * Mathf.Pow(m, 2) * n * Mathf.Pow(q, 3) +
                               54 * Mathf.Pow(m, 2) * o * Mathf.Pow(r, 3) + 54 * Mathf.Pow(n, 2) * o * Mathf.Pow(r, 3) +
                               108 * c * n * q * Mathf.Pow(r, 3) - 108 * l * n * q * Mathf.Pow(r, 3) +
                               108 * b * o * q * Mathf.Pow(r, 3) - 108 * k * o * q * Mathf.Pow(r, 3) -
                               216 * a * m * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                               216 * j * m * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) -
                               108 * b * n * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                               108 * k * n * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) -
                               108 * c * o * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                               108 * l * o * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                               54 * Mathf.Pow(n, 3) * q * Mathf.Pow(r, 2) -
                               108 * n * Mathf.Pow(o, 2) * q * Mathf.Pow(r, 2) +
                               54 * Mathf.Pow(m, 2) * n * q * Mathf.Pow(r, 2) + 108 * c * n * Mathf.Pow(q, 3) * r -
                               108 * l * n * Mathf.Pow(q, 3) * r + 108 * b * o * Mathf.Pow(q, 3) * r -
                               108 * k * o * Mathf.Pow(q, 3) * r + 54 * Mathf.Pow(o, 3) * Mathf.Pow(q, 2) * r +
                               54 * Mathf.Pow(m, 2) * o * Mathf.Pow(q, 2) * r -
                               108 * Mathf.Pow(n, 2) * o * Mathf.Pow(q, 2) * r + Mathf.Sqrt(
                                   4 * Mathf.Pow(
                                       (6 * (Mathf.Pow(m, 2) + Mathf.Pow(n, 2) + Mathf.Pow(o, 2) + 2 * a * p -
                                            2 * j * p +
                                            2 * b * q - 2 * k * q + 2 * c * r - 2 * l * r) *
                                        (Mathf.Pow(p, 2) + Mathf.Pow(q, 2) + Mathf.Pow(r, 2)) -
                                        9 * Mathf.Pow((m * p + n * q + o * r), 2)), 3) + Mathf.Pow(
                                       (-108 * b * n * Mathf.Pow(p, 4) + 108 * k * n * Mathf.Pow(p, 4) -
                                        108 * c * o * Mathf.Pow(p, 4) + 108 * l * o * Mathf.Pow(p, 4) +
                                        54 * m * Mathf.Pow(n, 2) * Mathf.Pow(p, 3) +
                                        54 * m * Mathf.Pow(o, 2) * Mathf.Pow(p, 3) + 108 * b * m * q * Mathf.Pow(p, 3) -
                                        108 * k * m * q * Mathf.Pow(p, 3) + 108 * a * n * q * Mathf.Pow(p, 3) -
                                        108 * j * n * q * Mathf.Pow(p, 3) + 108 * c * m * r * Mathf.Pow(p, 3) -
                                        108 * l * m * r * Mathf.Pow(p, 3) + 108 * a * o * r * Mathf.Pow(p, 3) -
                                        108 * j * o * r * Mathf.Pow(p, 3) -
                                        108 * a * m * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                                        108 * j * m * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                                        108 * b * n * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                                        108 * k * n * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                                        216 * c * o * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                                        216 * l * o * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                                        108 * a * m * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                                        108 * j * m * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) -
                                        216 * b * n * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                                        216 * k * n * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) -
                                        108 * c * o * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                                        108 * l * o * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                                        54 * Mathf.Pow(n, 3) * q * Mathf.Pow(p, 2) +
                                        54 * n * Mathf.Pow(o, 2) * q * Mathf.Pow(p, 2) -
                                        108 * Mathf.Pow(m, 2) * n * q * Mathf.Pow(p, 2) +
                                        54 * Mathf.Pow(o, 3) * r * Mathf.Pow(p, 2) -
                                        108 * Mathf.Pow(m, 2) * o * r * Mathf.Pow(p, 2) +
                                        54 * Mathf.Pow(n, 2) * o * r * Mathf.Pow(p, 2) +
                                        108 * c * n * q * r * Mathf.Pow(p, 2) - 108 * l * n * q * r * Mathf.Pow(p, 2) +
                                        108 * b * o * q * r * Mathf.Pow(p, 2) - 108 * k * o * q * r * Mathf.Pow(p, 2) +
                                        108 * b * m * Mathf.Pow(q, 3) * p - 108 * k * m * Mathf.Pow(q, 3) * p +
                                        108 * a * n * Mathf.Pow(q, 3) * p - 108 * j * n * Mathf.Pow(q, 3) * p +
                                        108 * c * m * Mathf.Pow(r, 3) * p - 108 * l * m * Mathf.Pow(r, 3) * p +
                                        108 * a * o * Mathf.Pow(r, 3) * p - 108 * j * o * Mathf.Pow(r, 3) * p +
                                        54 * Mathf.Pow(m, 3) * Mathf.Pow(q, 2) * p -
                                        108 * m * Mathf.Pow(n, 2) * Mathf.Pow(q, 2) * p +
                                        54 * m * Mathf.Pow(o, 2) * Mathf.Pow(q, 2) * p +
                                        54 * Mathf.Pow(m, 3) * Mathf.Pow(r, 2) * p +
                                        54 * m * Mathf.Pow(n, 2) * Mathf.Pow(r, 2) * p -
                                        108 * m * Mathf.Pow(o, 2) * Mathf.Pow(r, 2) * p +
                                        108 * b * m * q * Mathf.Pow(r, 2) * p - 108 * k * m * q * Mathf.Pow(r, 2) * p +
                                        108 * a * n * q * Mathf.Pow(r, 2) * p - 108 * j * n * q * Mathf.Pow(r, 2) * p +
                                        108 * c * m * Mathf.Pow(q, 2) * r * p - 108 * l * m * Mathf.Pow(q, 2) * r * p +
                                        108 * a * o * Mathf.Pow(q, 2) * r * p - 108 * j * o * Mathf.Pow(q, 2) * r * p -
                                        324 * m * n * o * q * r * p - 108 * a * m * Mathf.Pow(q, 4) +
                                        108 * j * m * Mathf.Pow(q, 4) - 108 * c * o * Mathf.Pow(q, 4) +
                                        108 * l * o * Mathf.Pow(q, 4) - 108 * a * m * Mathf.Pow(r, 4) +
                                        108 * j * m * Mathf.Pow(r, 4) - 108 * b * n * Mathf.Pow(r, 4) +
                                        108 * k * n * Mathf.Pow(r, 4) + 54 * n * Mathf.Pow(o, 2) * Mathf.Pow(q, 3) +
                                        54 * Mathf.Pow(m, 2) * n * Mathf.Pow(q, 3) +
                                        54 * Mathf.Pow(m, 2) * o * Mathf.Pow(r, 3) +
                                        54 * Mathf.Pow(n, 2) * o * Mathf.Pow(r, 3) + 108 * c * n * q * Mathf.Pow(r, 3) -
                                        108 * l * n * q * Mathf.Pow(r, 3) + 108 * b * o * q * Mathf.Pow(r, 3) -
                                        108 * k * o * q * Mathf.Pow(r, 3) -
                                        216 * a * m * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                                        216 * j * m * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) -
                                        108 * b * n * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                                        108 * k * n * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) -
                                        108 * c * o * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                                        108 * l * o * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                                        54 * Mathf.Pow(n, 3) * q * Mathf.Pow(r, 2) -
                                        108 * n * Mathf.Pow(o, 2) * q * Mathf.Pow(r, 2) +
                                        54 * Mathf.Pow(m, 2) * n * q * Mathf.Pow(r, 2) +
                                        108 * c * n * Mathf.Pow(q, 3) * r -
                                        108 * l * n * Mathf.Pow(q, 3) * r + 108 * b * o * Mathf.Pow(q, 3) * r -
                                        108 * k * o * Mathf.Pow(q, 3) * r + 54 * Mathf.Pow(o, 3) * Mathf.Pow(q, 2) * r +
                                        54 * Mathf.Pow(m, 2) * o * Mathf.Pow(q, 2) * r -
                                        108 * Mathf.Pow(n, 2) * o * Mathf.Pow(q, 2) * r), 2))), (1f / 3f)) /
                       (6 * Mathf.Pow(2, (1f / 3f)) * (Mathf.Pow(p, 2) + Mathf.Pow(q, 2) + Mathf.Pow(r, 2))) -
                       (6 * (Mathf.Pow(m, 2) + Mathf.Pow(n, 2) + Mathf.Pow(o, 2) + 2 * a * p - 2 * j * p + 2 * b * q -
                            2 * k * q + 2 * c * r - 2 * l * r) * (Mathf.Pow(p, 2) + Mathf.Pow(q, 2) + Mathf.Pow(r, 2)) -
                        9 * Mathf.Pow((m * p + n * q + o * r), 2)) / (3 * Mathf.Pow(2, (2f / 3f)) *
                                                                      (Mathf.Pow(p, 2) + Mathf.Pow(q, 2) +
                                                                       Mathf.Pow(r, 2)) *
                                                                      Mathf.Pow(
                                                                          (-108 * b * n * Mathf.Pow(p, 4) +
                                                                           108 * k * n * Mathf.Pow(p, 4) -
                                                                           108 * c * o * Mathf.Pow(p, 4) +
                                                                           108 * l * o * Mathf.Pow(p, 4) +
                                                                           54 * m * Mathf.Pow(n, 2) * Mathf.Pow(p, 3) +
                                                                           54 * m * Mathf.Pow(o, 2) * Mathf.Pow(p, 3) +
                                                                           108 * b * m * q * Mathf.Pow(p, 3) -
                                                                           108 * k * m * q * Mathf.Pow(p, 3) +
                                                                           108 * a * n * q * Mathf.Pow(p, 3) -
                                                                           108 * j * n * q * Mathf.Pow(p, 3) +
                                                                           108 * c * m * r * Mathf.Pow(p, 3) -
                                                                           108 * l * m * r * Mathf.Pow(p, 3) +
                                                                           108 * a * o * r * Mathf.Pow(p, 3) -
                                                                           108 * j * o * r * Mathf.Pow(p, 3) -
                                                                           108 * a * m * Mathf.Pow(q, 2) *
                                                                           Mathf.Pow(p, 2) +
                                                                           108 * j * m * Mathf.Pow(q, 2) *
                                                                           Mathf.Pow(p, 2) -
                                                                           108 * b * n * Mathf.Pow(q, 2) *
                                                                           Mathf.Pow(p, 2) +
                                                                           108 * k * n * Mathf.Pow(q, 2) *
                                                                           Mathf.Pow(p, 2) -
                                                                           216 * c * o * Mathf.Pow(q, 2) *
                                                                           Mathf.Pow(p, 2) +
                                                                           216 * l * o * Mathf.Pow(q, 2) *
                                                                           Mathf.Pow(p, 2) -
                                                                           108 * a * m * Mathf.Pow(r, 2) *
                                                                           Mathf.Pow(p, 2) +
                                                                           108 * j * m * Mathf.Pow(r, 2) *
                                                                           Mathf.Pow(p, 2) -
                                                                           216 * b * n * Mathf.Pow(r, 2) *
                                                                           Mathf.Pow(p, 2) +
                                                                           216 * k * n * Mathf.Pow(r, 2) *
                                                                           Mathf.Pow(p, 2) -
                                                                           108 * c * o * Mathf.Pow(r, 2) *
                                                                           Mathf.Pow(p, 2) +
                                                                           108 * l * o * Mathf.Pow(r, 2) *
                                                                           Mathf.Pow(p, 2) +
                                                                           54 * Mathf.Pow(n, 3) * q * Mathf.Pow(p, 2) +
                                                                           54 * n * Mathf.Pow(o, 2) * q *
                                                                           Mathf.Pow(p, 2) -
                                                                           108 * Mathf.Pow(m, 2) * n * q *
                                                                           Mathf.Pow(p, 2) +
                                                                           54 * Mathf.Pow(o, 3) * r * Mathf.Pow(p, 2) -
                                                                           108 * Mathf.Pow(m, 2) * o * r *
                                                                           Mathf.Pow(p, 2) +
                                                                           54 * Mathf.Pow(n, 2) * o * r *
                                                                           Mathf.Pow(p, 2) +
                                                                           108 * c * n * q * r * Mathf.Pow(p, 2) -
                                                                           108 * l * n * q * r * Mathf.Pow(p, 2) +
                                                                           108 * b * o * q * r * Mathf.Pow(p, 2) -
                                                                           108 * k * o * q * r * Mathf.Pow(p, 2) +
                                                                           108 * b * m * Mathf.Pow(q, 3) * p -
                                                                           108 * k * m * Mathf.Pow(q, 3) * p +
                                                                           108 * a * n * Mathf.Pow(q, 3) * p -
                                                                           108 * j * n * Mathf.Pow(q, 3) * p +
                                                                           108 * c * m * Mathf.Pow(r, 3) * p -
                                                                           108 * l * m * Mathf.Pow(r, 3) * p +
                                                                           108 * a * o * Mathf.Pow(r, 3) * p -
                                                                           108 * j * o * Mathf.Pow(r, 3) * p +
                                                                           54 * Mathf.Pow(m, 3) * Mathf.Pow(q, 2) * p -
                                                                           108 * m * Mathf.Pow(n, 2) * Mathf.Pow(q, 2) *
                                                                           p +
                                                                           54 * m * Mathf.Pow(o, 2) * Mathf.Pow(q, 2) *
                                                                           p +
                                                                           54 * Mathf.Pow(m, 3) * Mathf.Pow(r, 2) * p +
                                                                           54 * m * Mathf.Pow(n, 2) * Mathf.Pow(r, 2) *
                                                                           p -
                                                                           108 * m * Mathf.Pow(o, 2) * Mathf.Pow(r, 2) *
                                                                           p +
                                                                           108 * b * m * q * Mathf.Pow(r, 2) * p -
                                                                           108 * k * m * q * Mathf.Pow(r, 2) * p +
                                                                           108 * a * n * q * Mathf.Pow(r, 2) * p -
                                                                           108 * j * n * q * Mathf.Pow(r, 2) * p +
                                                                           108 * c * m * Mathf.Pow(q, 2) * r * p -
                                                                           108 * l * m * Mathf.Pow(q, 2) * r * p +
                                                                           108 * a * o * Mathf.Pow(q, 2) * r * p -
                                                                           108 * j * o * Mathf.Pow(q, 2) * r * p -
                                                                           324 * m * n * o * q * r * p -
                                                                           108 * a * m * Mathf.Pow(q, 4) +
                                                                           108 * j * m * Mathf.Pow(q, 4) -
                                                                           108 * c * o * Mathf.Pow(q, 4) +
                                                                           108 * l * o * Mathf.Pow(q, 4) -
                                                                           108 * a * m * Mathf.Pow(r, 4) +
                                                                           108 * j * m * Mathf.Pow(r, 4) -
                                                                           108 * b * n * Mathf.Pow(r, 4) +
                                                                           108 * k * n * Mathf.Pow(r, 4) +
                                                                           54 * n * Mathf.Pow(o, 2) * Mathf.Pow(q, 3) +
                                                                           54 * Mathf.Pow(m, 2) * n * Mathf.Pow(q, 3) +
                                                                           54 * Mathf.Pow(m, 2) * o * Mathf.Pow(r, 3) +
                                                                           54 * Mathf.Pow(n, 2) * o * Mathf.Pow(r, 3) +
                                                                           108 * c * n * q * Mathf.Pow(r, 3) -
                                                                           108 * l * n * q * Mathf.Pow(r, 3) +
                                                                           108 * b * o * q * Mathf.Pow(r, 3) -
                                                                           108 * k * o * q * Mathf.Pow(r, 3) -
                                                                           216 * a * m * Mathf.Pow(q, 2) *
                                                                           Mathf.Pow(r, 2) +
                                                                           216 * j * m * Mathf.Pow(q, 2) *
                                                                           Mathf.Pow(r, 2) -
                                                                           108 * b * n * Mathf.Pow(q, 2) *
                                                                           Mathf.Pow(r, 2) +
                                                                           108 * k * n * Mathf.Pow(q, 2) *
                                                                           Mathf.Pow(r, 2) -
                                                                           108 * c * o * Mathf.Pow(q, 2) *
                                                                           Mathf.Pow(r, 2) +
                                                                           108 * l * o * Mathf.Pow(q, 2) *
                                                                           Mathf.Pow(r, 2) +
                                                                           54 * Mathf.Pow(n, 3) * q * Mathf.Pow(r, 2) -
                                                                           108 * n * Mathf.Pow(o, 2) * q *
                                                                           Mathf.Pow(r, 2) +
                                                                           54 * Mathf.Pow(m, 2) * n * q *
                                                                           Mathf.Pow(r, 2) +
                                                                           108 * c * n * Mathf.Pow(q, 3) * r -
                                                                           108 * l * n * Mathf.Pow(q, 3) * r +
                                                                           108 * b * o * Mathf.Pow(q, 3) * r -
                                                                           108 * k * o * Mathf.Pow(q, 3) * r +
                                                                           54 * Mathf.Pow(o, 3) * Mathf.Pow(q, 2) * r +
                                                                           54 * Mathf.Pow(m, 2) * o * Mathf.Pow(q, 2) *
                                                                           r -
                                                                           108 * Mathf.Pow(n, 2) * o * Mathf.Pow(q, 2) *
                                                                           r +
                                                                           Mathf.Sqrt(
                                                                               4 * Mathf.Pow(
                                                                                   (6 * (Mathf.Pow(m, 2) +
                                                                                           Mathf.Pow(n, 2) +
                                                                                           Mathf.Pow(o, 2) + 2 * a * p -
                                                                                           2 * j * p + 2 * b * q -
                                                                                           2 * k * q +
                                                                                           2 * c * r - 2 * l * r) *
                                                                                       (Mathf.Pow(p, 2) +
                                                                                           Mathf.Pow(q, 2) +
                                                                                           Mathf.Pow(r, 2)) -
                                                                                       9 * Mathf.Pow(
                                                                                           (m * p + n * q + o * r),
                                                                                           2)), 3) + Mathf.Pow(
                                                                                   (-108 * b * n * Mathf.Pow(p, 4) +
                                                                                       108 * k * n * Mathf.Pow(p, 4) -
                                                                                       108 * c * o * Mathf.Pow(p, 4) +
                                                                                       108 * l * o * Mathf.Pow(p, 4) +
                                                                                       54 * m * Mathf.Pow(n, 2) *
                                                                                       Mathf.Pow(p, 3) +
                                                                                       54 * m * Mathf.Pow(o, 2) *
                                                                                       Mathf.Pow(p, 3) +
                                                                                       108 * b * m * q *
                                                                                       Mathf.Pow(p, 3) -
                                                                                       108 * k * m * q *
                                                                                       Mathf.Pow(p, 3) +
                                                                                       108 * a * n * q *
                                                                                       Mathf.Pow(p, 3) -
                                                                                       108 * j * n * q *
                                                                                       Mathf.Pow(p, 3) +
                                                                                       108 * c * m * r *
                                                                                       Mathf.Pow(p, 3) -
                                                                                       108 * l * m * r *
                                                                                       Mathf.Pow(p, 3) +
                                                                                       108 * a * o * r *
                                                                                       Mathf.Pow(p, 3) -
                                                                                       108 * j * o * r *
                                                                                       Mathf.Pow(p, 3) -
                                                                                       108 * a * m * Mathf.Pow(q, 2) *
                                                                                       Mathf.Pow(p, 2) +
                                                                                       108 * j * m * Mathf.Pow(q, 2) *
                                                                                       Mathf.Pow(p, 2) -
                                                                                       108 * b * n * Mathf.Pow(q, 2) *
                                                                                       Mathf.Pow(p, 2) +
                                                                                       108 * k * n * Mathf.Pow(q, 2) *
                                                                                       Mathf.Pow(p, 2) -
                                                                                       216 * c * o * Mathf.Pow(q, 2) *
                                                                                       Mathf.Pow(p, 2) +
                                                                                       216 * l * o * Mathf.Pow(q, 2) *
                                                                                       Mathf.Pow(p, 2) -
                                                                                       108 * a * m * Mathf.Pow(r, 2) *
                                                                                       Mathf.Pow(p, 2) +
                                                                                       108 * j * m * Mathf.Pow(r, 2) *
                                                                                       Mathf.Pow(p, 2) -
                                                                                       216 * b * n * Mathf.Pow(r, 2) *
                                                                                       Mathf.Pow(p, 2) +
                                                                                       216 * k * n * Mathf.Pow(r, 2) *
                                                                                       Mathf.Pow(p, 2) -
                                                                                       108 * c * o * Mathf.Pow(r, 2) *
                                                                                       Mathf.Pow(p, 2) +
                                                                                       108 * l * o * Mathf.Pow(r, 2) *
                                                                                       Mathf.Pow(p, 2) +
                                                                                       54 * Mathf.Pow(n, 3) * q *
                                                                                       Mathf.Pow(p, 2) +
                                                                                       54 * n * Mathf.Pow(o, 2) * q *
                                                                                       Mathf.Pow(p, 2) -
                                                                                       108 * Mathf.Pow(m, 2) * n * q *
                                                                                       Mathf.Pow(p, 2) +
                                                                                       54 * Mathf.Pow(o, 3) * r *
                                                                                       Mathf.Pow(p, 2) -
                                                                                       108 * Mathf.Pow(m, 2) * o * r *
                                                                                       Mathf.Pow(p, 2) +
                                                                                       54 * Mathf.Pow(n, 2) * o * r *
                                                                                       Mathf.Pow(p, 2) +
                                                                                       108 * c * n * q * r *
                                                                                       Mathf.Pow(p, 2) -
                                                                                       108 * l * n * q * r *
                                                                                       Mathf.Pow(p, 2) +
                                                                                       108 * b * o * q * r *
                                                                                       Mathf.Pow(p, 2) -
                                                                                       108 * k * o * q * r *
                                                                                       Mathf.Pow(p, 2) +
                                                                                       108 * b * m * Mathf.Pow(q, 3) *
                                                                                       p -
                                                                                       108 * k * m * Mathf.Pow(q, 3) *
                                                                                       p +
                                                                                       108 * a * n * Mathf.Pow(q, 3) *
                                                                                       p -
                                                                                       108 * j * n * Mathf.Pow(q, 3) *
                                                                                       p +
                                                                                       108 * c * m * Mathf.Pow(r, 3) *
                                                                                       p -
                                                                                       108 * l * m * Mathf.Pow(r, 3) *
                                                                                       p +
                                                                                       108 * a * o * Mathf.Pow(r, 3) *
                                                                                       p -
                                                                                       108 * j * o * Mathf.Pow(r, 3) *
                                                                                       p +
                                                                                       54 * Mathf.Pow(m, 3) *
                                                                                       Mathf.Pow(q, 2) * p -
                                                                                       108 * m * Mathf.Pow(n, 2) *
                                                                                       Mathf.Pow(q, 2) * p +
                                                                                       54 * m * Mathf.Pow(o, 2) *
                                                                                       Mathf.Pow(q, 2) * p +
                                                                                       54 * Mathf.Pow(m, 3) *
                                                                                       Mathf.Pow(r, 2) * p +
                                                                                       54 * m * Mathf.Pow(n, 2) *
                                                                                       Mathf.Pow(r, 2) * p -
                                                                                       108 * m * Mathf.Pow(o, 2) *
                                                                                       Mathf.Pow(r, 2) * p +
                                                                                       108 * b * m * q *
                                                                                       Mathf.Pow(r, 2) *
                                                                                       p -
                                                                                       108 * k * m * q *
                                                                                       Mathf.Pow(r, 2) *
                                                                                       p +
                                                                                       108 * a * n * q *
                                                                                       Mathf.Pow(r, 2) *
                                                                                       p -
                                                                                       108 * j * n * q *
                                                                                       Mathf.Pow(r, 2) *
                                                                                       p +
                                                                                       108 * c * m * Mathf.Pow(q, 2) *
                                                                                       r *
                                                                                       p -
                                                                                       108 * l * m * Mathf.Pow(q, 2) *
                                                                                       r *
                                                                                       p +
                                                                                       108 * a * o * Mathf.Pow(q, 2) *
                                                                                       r *
                                                                                       p -
                                                                                       108 * j * o * Mathf.Pow(q, 2) *
                                                                                       r *
                                                                                       p - 324 * m * n * o * q * r * p -
                                                                                       108 * a * m * Mathf.Pow(q, 4) +
                                                                                       108 * j * m * Mathf.Pow(q, 4) -
                                                                                       108 * c * o * Mathf.Pow(q, 4) +
                                                                                       108 * l * o * Mathf.Pow(q, 4) -
                                                                                       108 * a * m * Mathf.Pow(r, 4) +
                                                                                       108 * j * m * Mathf.Pow(r, 4) -
                                                                                       108 * b * n * Mathf.Pow(r, 4) +
                                                                                       108 * k * n * Mathf.Pow(r, 4) +
                                                                                       54 * n * Mathf.Pow(o, 2) *
                                                                                       Mathf.Pow(q, 3) +
                                                                                       54 * Mathf.Pow(m, 2) * n *
                                                                                       Mathf.Pow(q, 3) +
                                                                                       54 * Mathf.Pow(m, 2) * o *
                                                                                       Mathf.Pow(r, 3) +
                                                                                       54 * Mathf.Pow(n, 2) * o *
                                                                                       Mathf.Pow(r, 3) +
                                                                                       108 * c * n * q *
                                                                                       Mathf.Pow(r, 3) -
                                                                                       108 * l * n * q *
                                                                                       Mathf.Pow(r, 3) +
                                                                                       108 * b * o * q *
                                                                                       Mathf.Pow(r, 3) -
                                                                                       108 * k * o * q *
                                                                                       Mathf.Pow(r, 3) -
                                                                                       216 * a * m * Mathf.Pow(q, 2) *
                                                                                       Mathf.Pow(r, 2) +
                                                                                       216 * j * m * Mathf.Pow(q, 2) *
                                                                                       Mathf.Pow(r, 2) -
                                                                                       108 * b * n * Mathf.Pow(q, 2) *
                                                                                       Mathf.Pow(r, 2) +
                                                                                       108 * k * n * Mathf.Pow(q, 2) *
                                                                                       Mathf.Pow(r, 2) -
                                                                                       108 * c * o * Mathf.Pow(q, 2) *
                                                                                       Mathf.Pow(r, 2) +
                                                                                       108 * l * o * Mathf.Pow(q, 2) *
                                                                                       Mathf.Pow(r, 2) +
                                                                                       54 * Mathf.Pow(n, 3) * q *
                                                                                       Mathf.Pow(r, 2) -
                                                                                       108 * n * Mathf.Pow(o, 2) * q *
                                                                                       Mathf.Pow(r, 2) +
                                                                                       54 * Mathf.Pow(m, 2) * n * q *
                                                                                       Mathf.Pow(r, 2) +
                                                                                       108 * c * n * Mathf.Pow(q, 3) *
                                                                                       r -
                                                                                       108 * l * n * Mathf.Pow(q, 3) *
                                                                                       r +
                                                                                       108 * b * o * Mathf.Pow(q, 3) *
                                                                                       r -
                                                                                       108 * k * o * Mathf.Pow(q, 3) *
                                                                                       r +
                                                                                       54 * Mathf.Pow(o, 3) *
                                                                                       Mathf.Pow(q, 2) * r +
                                                                                       54 * Mathf.Pow(m, 2) * o *
                                                                                       Mathf.Pow(q, 2) * r -
                                                                                       108 * Mathf.Pow(n, 2) * o *
                                                                                       Mathf.Pow(q, 2) * r), 2))),
                                                                          (1f / 3f)));

            // Solution 2
            float x2 = -(m * p + n * q + o * r) / (2 * (Mathf.Pow(p, 2) + Mathf.Pow(q, 2) + Mathf.Pow(r, 2))) -
                       ((1 - i * Mathf.Sqrt(3)) * Mathf.Pow(
                           (-108 * b * n * Mathf.Pow(p, 4) + 108 * k * n * Mathf.Pow(p, 4) -
                               108 * c * o * Mathf.Pow(p, 4) +
                               108 * l * o * Mathf.Pow(p, 4) + 54 * m * Mathf.Pow(n, 2) * Mathf.Pow(p, 3) +
                               54 * m * Mathf.Pow(o, 2) * Mathf.Pow(p, 3) + 108 * b * m * q * Mathf.Pow(p, 3) -
                               108 * k * m * q * Mathf.Pow(p, 3) + 108 * a * n * q * Mathf.Pow(p, 3) -
                               108 * j * n * q * Mathf.Pow(p, 3) + 108 * c * m * r * Mathf.Pow(p, 3) -
                               108 * l * m * r * Mathf.Pow(p, 3) + 108 * a * o * r * Mathf.Pow(p, 3) -
                               108 * j * o * r * Mathf.Pow(p, 3) - 108 * a * m * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                               108 * j * m * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                               108 * b * n * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                               108 * k * n * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                               216 * c * o * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                               216 * l * o * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                               108 * a * m * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                               108 * j * m * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) -
                               216 * b * n * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                               216 * k * n * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) -
                               108 * c * o * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                               108 * l * o * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                               54 * Mathf.Pow(n, 3) * q * Mathf.Pow(p, 2) +
                               54 * n * Mathf.Pow(o, 2) * q * Mathf.Pow(p, 2) -
                               108 * Mathf.Pow(m, 2) * n * q * Mathf.Pow(p, 2) +
                               54 * Mathf.Pow(o, 3) * r * Mathf.Pow(p, 2) -
                               108 * Mathf.Pow(m, 2) * o * r * Mathf.Pow(p, 2) +
                               54 * Mathf.Pow(n, 2) * o * r * Mathf.Pow(p, 2) + 108 * c * n * q * r * Mathf.Pow(p, 2) -
                               108 * l * n * q * r * Mathf.Pow(p, 2) + 108 * b * o * q * r * Mathf.Pow(p, 2) -
                               108 * k * o * q * r * Mathf.Pow(p, 2) + 108 * b * m * Mathf.Pow(q, 3) * p -
                               108 * k * m * Mathf.Pow(q, 3) * p + 108 * a * n * Mathf.Pow(q, 3) * p -
                               108 * j * n * Mathf.Pow(q, 3) * p + 108 * c * m * Mathf.Pow(r, 3) * p -
                               108 * l * m * Mathf.Pow(r, 3) * p + 108 * a * o * Mathf.Pow(r, 3) * p -
                               108 * j * o * Mathf.Pow(r, 3) * p + 54 * Mathf.Pow(m, 3) * Mathf.Pow(q, 2) * p -
                               108 * m * Mathf.Pow(n, 2) * Mathf.Pow(q, 2) * p +
                               54 * m * Mathf.Pow(o, 2) * Mathf.Pow(q, 2) * p +
                               54 * Mathf.Pow(m, 3) * Mathf.Pow(r, 2) * p +
                               54 * m * Mathf.Pow(n, 2) * Mathf.Pow(r, 2) * p -
                               108 * m * Mathf.Pow(o, 2) * Mathf.Pow(r, 2) * p + 108 * b * m * q * Mathf.Pow(r, 2) * p -
                               108 * k * m * q * Mathf.Pow(r, 2) * p + 108 * a * n * q * Mathf.Pow(r, 2) * p -
                               108 * j * n * q * Mathf.Pow(r, 2) * p + 108 * c * m * Mathf.Pow(q, 2) * r * p -
                               108 * l * m * Mathf.Pow(q, 2) * r * p + 108 * a * o * Mathf.Pow(q, 2) * r * p -
                               108 * j * o * Mathf.Pow(q, 2) * r * p - 324 * m * n * o * q * r * p -
                               108 * a * m * Mathf.Pow(q, 4) + 108 * j * m * Mathf.Pow(q, 4) -
                               108 * c * o * Mathf.Pow(q, 4) +
                               108 * l * o * Mathf.Pow(q, 4) - 108 * a * m * Mathf.Pow(r, 4) +
                               108 * j * m * Mathf.Pow(r, 4) -
                               108 * b * n * Mathf.Pow(r, 4) + 108 * k * n * Mathf.Pow(r, 4) +
                               54 * n * Mathf.Pow(o, 2) * Mathf.Pow(q, 3) + 54 * Mathf.Pow(m, 2) * n * Mathf.Pow(q, 3) +
                               54 * Mathf.Pow(m, 2) * o * Mathf.Pow(r, 3) + 54 * Mathf.Pow(n, 2) * o * Mathf.Pow(r, 3) +
                               108 * c * n * q * Mathf.Pow(r, 3) - 108 * l * n * q * Mathf.Pow(r, 3) +
                               108 * b * o * q * Mathf.Pow(r, 3) - 108 * k * o * q * Mathf.Pow(r, 3) -
                               216 * a * m * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                               216 * j * m * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) -
                               108 * b * n * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                               108 * k * n * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) -
                               108 * c * o * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                               108 * l * o * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                               54 * Mathf.Pow(n, 3) * q * Mathf.Pow(r, 2) -
                               108 * n * Mathf.Pow(o, 2) * q * Mathf.Pow(r, 2) +
                               54 * Mathf.Pow(m, 2) * n * q * Mathf.Pow(r, 2) + 108 * c * n * Mathf.Pow(q, 3) * r -
                               108 * l * n * Mathf.Pow(q, 3) * r + 108 * b * o * Mathf.Pow(q, 3) * r -
                               108 * k * o * Mathf.Pow(q, 3) * r + 54 * Mathf.Pow(o, 3) * Mathf.Pow(q, 2) * r +
                               54 * Mathf.Pow(m, 2) * o * Mathf.Pow(q, 2) * r -
                               108 * Mathf.Pow(n, 2) * o * Mathf.Pow(q, 2) * r + Mathf.Sqrt(
                                   4 * Mathf.Pow(
                                       (6 * (Mathf.Pow(m, 2) + Mathf.Pow(n, 2) + Mathf.Pow(o, 2) + 2 * a * p -
                                            2 * j * p +
                                            2 * b * q - 2 * k * q + 2 * c * r - 2 * l * r) *
                                        (Mathf.Pow(p, 2) + Mathf.Pow(q, 2) + Mathf.Pow(r, 2)) -
                                        9 * Mathf.Pow((m * p + n * q + o * r), 2)), 3) + Mathf.Pow(
                                       (-108 * b * n * Mathf.Pow(p, 4) + 108 * k * n * Mathf.Pow(p, 4) -
                                        108 * c * o * Mathf.Pow(p, 4) + 108 * l * o * Mathf.Pow(p, 4) +
                                        54 * m * Mathf.Pow(n, 2) * Mathf.Pow(p, 3) +
                                        54 * m * Mathf.Pow(o, 2) * Mathf.Pow(p, 3) + 108 * b * m * q * Mathf.Pow(p, 3) -
                                        108 * k * m * q * Mathf.Pow(p, 3) + 108 * a * n * q * Mathf.Pow(p, 3) -
                                        108 * j * n * q * Mathf.Pow(p, 3) + 108 * c * m * r * Mathf.Pow(p, 3) -
                                        108 * l * m * r * Mathf.Pow(p, 3) + 108 * a * o * r * Mathf.Pow(p, 3) -
                                        108 * j * o * r * Mathf.Pow(p, 3) -
                                        108 * a * m * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                                        108 * j * m * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                                        108 * b * n * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                                        108 * k * n * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                                        216 * c * o * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                                        216 * l * o * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                                        108 * a * m * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                                        108 * j * m * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) -
                                        216 * b * n * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                                        216 * k * n * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) -
                                        108 * c * o * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                                        108 * l * o * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                                        54 * Mathf.Pow(n, 3) * q * Mathf.Pow(p, 2) +
                                        54 * n * Mathf.Pow(o, 2) * q * Mathf.Pow(p, 2) -
                                        108 * Mathf.Pow(m, 2) * n * q * Mathf.Pow(p, 2) +
                                        54 * Mathf.Pow(o, 3) * r * Mathf.Pow(p, 2) -
                                        108 * Mathf.Pow(m, 2) * o * r * Mathf.Pow(p, 2) +
                                        54 * Mathf.Pow(n, 2) * o * r * Mathf.Pow(p, 2) +
                                        108 * c * n * q * r * Mathf.Pow(p, 2) - 108 * l * n * q * r * Mathf.Pow(p, 2) +
                                        108 * b * o * q * r * Mathf.Pow(p, 2) - 108 * k * o * q * r * Mathf.Pow(p, 2) +
                                        108 * b * m * Mathf.Pow(q, 3) * p - 108 * k * m * Mathf.Pow(q, 3) * p +
                                        108 * a * n * Mathf.Pow(q, 3) * p - 108 * j * n * Mathf.Pow(q, 3) * p +
                                        108 * c * m * Mathf.Pow(r, 3) * p - 108 * l * m * Mathf.Pow(r, 3) * p +
                                        108 * a * o * Mathf.Pow(r, 3) * p - 108 * j * o * Mathf.Pow(r, 3) * p +
                                        54 * Mathf.Pow(m, 3) * Mathf.Pow(q, 2) * p -
                                        108 * m * Mathf.Pow(n, 2) * Mathf.Pow(q, 2) * p +
                                        54 * m * Mathf.Pow(o, 2) * Mathf.Pow(q, 2) * p +
                                        54 * Mathf.Pow(m, 3) * Mathf.Pow(r, 2) * p +
                                        54 * m * Mathf.Pow(n, 2) * Mathf.Pow(r, 2) * p -
                                        108 * m * Mathf.Pow(o, 2) * Mathf.Pow(r, 2) * p +
                                        108 * b * m * q * Mathf.Pow(r, 2) * p - 108 * k * m * q * Mathf.Pow(r, 2) * p +
                                        108 * a * n * q * Mathf.Pow(r, 2) * p - 108 * j * n * q * Mathf.Pow(r, 2) * p +
                                        108 * c * m * Mathf.Pow(q, 2) * r * p - 108 * l * m * Mathf.Pow(q, 2) * r * p +
                                        108 * a * o * Mathf.Pow(q, 2) * r * p - 108 * j * o * Mathf.Pow(q, 2) * r * p -
                                        324 * m * n * o * q * r * p - 108 * a * m * Mathf.Pow(q, 4) +
                                        108 * j * m * Mathf.Pow(q, 4) - 108 * c * o * Mathf.Pow(q, 4) +
                                        108 * l * o * Mathf.Pow(q, 4) - 108 * a * m * Mathf.Pow(r, 4) +
                                        108 * j * m * Mathf.Pow(r, 4) - 108 * b * n * Mathf.Pow(r, 4) +
                                        108 * k * n * Mathf.Pow(r, 4) + 54 * n * Mathf.Pow(o, 2) * Mathf.Pow(q, 3) +
                                        54 * Mathf.Pow(m, 2) * n * Mathf.Pow(q, 3) +
                                        54 * Mathf.Pow(m, 2) * o * Mathf.Pow(r, 3) +
                                        54 * Mathf.Pow(n, 2) * o * Mathf.Pow(r, 3) + 108 * c * n * q * Mathf.Pow(r, 3) -
                                        108 * l * n * q * Mathf.Pow(r, 3) + 108 * b * o * q * Mathf.Pow(r, 3) -
                                        108 * k * o * q * Mathf.Pow(r, 3) -
                                        216 * a * m * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                                        216 * j * m * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) -
                                        108 * b * n * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                                        108 * k * n * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) -
                                        108 * c * o * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                                        108 * l * o * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                                        54 * Mathf.Pow(n, 3) * q * Mathf.Pow(r, 2) -
                                        108 * n * Mathf.Pow(o, 2) * q * Mathf.Pow(r, 2) +
                                        54 * Mathf.Pow(m, 2) * n * q * Mathf.Pow(r, 2) +
                                        108 * c * n * Mathf.Pow(q, 3) * r -
                                        108 * l * n * Mathf.Pow(q, 3) * r + 108 * b * o * Mathf.Pow(q, 3) * r -
                                        108 * k * o * Mathf.Pow(q, 3) * r + 54 * Mathf.Pow(o, 3) * Mathf.Pow(q, 2) * r +
                                        54 * Mathf.Pow(m, 2) * o * Mathf.Pow(q, 2) * r -
                                        108 * Mathf.Pow(n, 2) * o * Mathf.Pow(q, 2) * r), 2))), (1f / 3))) /
                       (12 * Mathf.Pow(2, (1f / 3)) * (Mathf.Pow(p, 2) + Mathf.Pow(q, 2) + Mathf.Pow(r, 2))) +
                       ((1 + i * Mathf.Sqrt(3)) *
                        (6 * (Mathf.Pow(m, 2) + Mathf.Pow(n, 2) + Mathf.Pow(o, 2) + 2 * a * p - 2 * j * p + 2 * b * q -
                             2 * k * q + 2 * c * r - 2 * l * r) *
                         (Mathf.Pow(p, 2) + Mathf.Pow(q, 2) + Mathf.Pow(r, 2)) -
                         9 * Mathf.Pow((m * p + n * q + o * r), 2))) / (6 * Mathf.Pow(2, (2f / 3)) *
                                                                        (Mathf.Pow(p, 2) + Mathf.Pow(q, 2) +
                                                                         Mathf.Pow(r, 2)) *
                                                                        Mathf.Pow(
                                                                            (-108 * b * n * Mathf.Pow(p, 4) +
                                                                                108 * k * n * Mathf.Pow(p, 4) -
                                                                                108 * c * o * Mathf.Pow(p, 4) +
                                                                                108 * l * o * Mathf.Pow(p, 4) +
                                                                                54 * m * Mathf.Pow(n, 2) *
                                                                                Mathf.Pow(p, 3) +
                                                                                54 * m * Mathf.Pow(o, 2) *
                                                                                Mathf.Pow(p, 3) +
                                                                                108 * b * m * q * Mathf.Pow(p, 3) -
                                                                                108 * k * m * q * Mathf.Pow(p, 3) +
                                                                                108 * a * n * q * Mathf.Pow(p, 3) -
                                                                                108 * j * n * q * Mathf.Pow(p, 3) +
                                                                                108 * c * m * r * Mathf.Pow(p, 3) -
                                                                                108 * l * m * r * Mathf.Pow(p, 3) +
                                                                                108 * a * o * r * Mathf.Pow(p, 3) -
                                                                                108 * j * o * r * Mathf.Pow(p, 3) -
                                                                                108 * a * m * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(p, 2) +
                                                                                108 * j * m * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(p, 2) -
                                                                                108 * b * n * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(p, 2) +
                                                                                108 * k * n * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(p, 2) -
                                                                                216 * c * o * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(p, 2) +
                                                                                216 * l * o * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(p, 2) -
                                                                                108 * a * m * Mathf.Pow(r, 2) *
                                                                                Mathf.Pow(p, 2) +
                                                                                108 * j * m * Mathf.Pow(r, 2) *
                                                                                Mathf.Pow(p, 2) -
                                                                                216 * b * n * Mathf.Pow(r, 2) *
                                                                                Mathf.Pow(p, 2) +
                                                                                216 * k * n * Mathf.Pow(r, 2) *
                                                                                Mathf.Pow(p, 2) -
                                                                                108 * c * o * Mathf.Pow(r, 2) *
                                                                                Mathf.Pow(p, 2) +
                                                                                108 * l * o * Mathf.Pow(r, 2) *
                                                                                Mathf.Pow(p, 2) +
                                                                                54 * Mathf.Pow(n, 3) * q *
                                                                                Mathf.Pow(p, 2) +
                                                                                54 * n * Mathf.Pow(o, 2) * q *
                                                                                Mathf.Pow(p, 2) -
                                                                                108 * Mathf.Pow(m, 2) * n * q *
                                                                                Mathf.Pow(p, 2) +
                                                                                54 * Mathf.Pow(o, 3) * r *
                                                                                Mathf.Pow(p, 2) -
                                                                                108 * Mathf.Pow(m, 2) * o * r *
                                                                                Mathf.Pow(p, 2) +
                                                                                54 * Mathf.Pow(n, 2) * o * r *
                                                                                Mathf.Pow(p, 2) +
                                                                                108 * c * n * q * r * Mathf.Pow(p, 2) -
                                                                                108 * l * n * q * r * Mathf.Pow(p, 2) +
                                                                                108 * b * o * q * r * Mathf.Pow(p, 2) -
                                                                                108 * k * o * q * r * Mathf.Pow(p, 2) +
                                                                                108 * b * m * Mathf.Pow(q, 3) * p -
                                                                                108 * k * m * Mathf.Pow(q, 3) * p +
                                                                                108 * a * n * Mathf.Pow(q, 3) * p -
                                                                                108 * j * n * Mathf.Pow(q, 3) * p +
                                                                                108 * c * m * Mathf.Pow(r, 3) * p -
                                                                                108 * l * m * Mathf.Pow(r, 3) * p +
                                                                                108 * a * o * Mathf.Pow(r, 3) * p -
                                                                                108 * j * o * Mathf.Pow(r, 3) * p +
                                                                                54 * Mathf.Pow(m, 3) * Mathf.Pow(q, 2) *
                                                                                p -
                                                                                108 * m * Mathf.Pow(n, 2) *
                                                                                Mathf.Pow(q, 2) *
                                                                                p +
                                                                                54 * m * Mathf.Pow(o, 2) *
                                                                                Mathf.Pow(q, 2) * p +
                                                                                54 * Mathf.Pow(m, 3) * Mathf.Pow(r, 2) *
                                                                                p +
                                                                                54 * m * Mathf.Pow(n, 2) *
                                                                                Mathf.Pow(r, 2) * p -
                                                                                108 * m * Mathf.Pow(o, 2) *
                                                                                Mathf.Pow(r, 2) *
                                                                                p + 108 * b * m * q * Mathf.Pow(r, 2) *
                                                                                p -
                                                                                108 * k * m * q * Mathf.Pow(r, 2) * p +
                                                                                108 * a * n * q * Mathf.Pow(r, 2) * p -
                                                                                108 * j * n * q * Mathf.Pow(r, 2) * p +
                                                                                108 * c * m * Mathf.Pow(q, 2) * r * p -
                                                                                108 * l * m * Mathf.Pow(q, 2) * r * p +
                                                                                108 * a * o * Mathf.Pow(q, 2) * r * p -
                                                                                108 * j * o * Mathf.Pow(q, 2) * r * p -
                                                                                324 * m * n * o * q * r * p -
                                                                                108 * a * m * Mathf.Pow(q, 4) +
                                                                                108 * j * m * Mathf.Pow(q, 4) -
                                                                                108 * c * o * Mathf.Pow(q, 4) +
                                                                                108 * l * o * Mathf.Pow(q, 4) -
                                                                                108 * a * m * Mathf.Pow(r, 4) +
                                                                                108 * j * m * Mathf.Pow(r, 4) -
                                                                                108 * b * n * Mathf.Pow(r, 4) +
                                                                                108 * k * n * Mathf.Pow(r, 4) +
                                                                                54 * n * Mathf.Pow(o, 2) *
                                                                                Mathf.Pow(q, 3) +
                                                                                54 * Mathf.Pow(m, 2) * n *
                                                                                Mathf.Pow(q, 3) +
                                                                                54 * Mathf.Pow(m, 2) * o *
                                                                                Mathf.Pow(r, 3) +
                                                                                54 * Mathf.Pow(n, 2) * o *
                                                                                Mathf.Pow(r, 3) +
                                                                                108 * c * n * q * Mathf.Pow(r, 3) -
                                                                                108 * l * n * q * Mathf.Pow(r, 3) +
                                                                                108 * b * o * q * Mathf.Pow(r, 3) -
                                                                                108 * k * o * q * Mathf.Pow(r, 3) -
                                                                                216 * a * m * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(r, 2) +
                                                                                216 * j * m * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(r, 2) -
                                                                                108 * b * n * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(r, 2) +
                                                                                108 * k * n * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(r, 2) -
                                                                                108 * c * o * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(r, 2) +
                                                                                108 * l * o * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(r, 2) +
                                                                                54 * Mathf.Pow(n, 3) * q *
                                                                                Mathf.Pow(r, 2) -
                                                                                108 * n * Mathf.Pow(o, 2) * q *
                                                                                Mathf.Pow(r, 2) +
                                                                                54 * Mathf.Pow(m, 2) * n * q *
                                                                                Mathf.Pow(r, 2) +
                                                                                108 * c * n * Mathf.Pow(q, 3) * r -
                                                                                108 * l * n * Mathf.Pow(q, 3) * r +
                                                                                108 * b * o * Mathf.Pow(q, 3) * r -
                                                                                108 * k * o * Mathf.Pow(q, 3) * r +
                                                                                54 * Mathf.Pow(o, 3) * Mathf.Pow(q, 2) *
                                                                                r +
                                                                                54 * Mathf.Pow(m, 2) * o *
                                                                                Mathf.Pow(q, 2) * r -
                                                                                108 * Mathf.Pow(n, 2) * o *
                                                                                Mathf.Pow(q, 2) *
                                                                                r + Mathf.Sqrt(
                                                                                    4 * Mathf.Pow(
                                                                                        (6 * (Mathf.Pow(m, 2) +
                                                                                                Mathf.Pow(n, 2) +
                                                                                                Mathf.Pow(o, 2) +
                                                                                                2 * a * p - 2 * j * p +
                                                                                                2 * b * q -
                                                                                                2 * k * q + 2 * c * r -
                                                                                                2 * l * r) *
                                                                                            (Mathf.Pow(p, 2) +
                                                                                                Mathf.Pow(q, 2) +
                                                                                                Mathf.Pow(r, 2)) -
                                                                                            9 * Mathf.Pow(
                                                                                                (m * p + n * q + o * r),
                                                                                                2)), 3) + Mathf.Pow(
                                                                                        (-108 * b * n *
                                                                                            Mathf.Pow(p, 4) +
                                                                                            108 * k * n * Mathf.Pow(p,
                                                                                                4) -
                                                                                            108 * c * o * Mathf.Pow(p,
                                                                                                4) +
                                                                                            108 * l * o * Mathf.Pow(p,
                                                                                                4) +
                                                                                            54 * m * Mathf.Pow(n, 2) *
                                                                                            Mathf.Pow(p, 3) +
                                                                                            54 * m * Mathf.Pow(o, 2) *
                                                                                            Mathf.Pow(p, 3) +
                                                                                            108 * b * m * q *
                                                                                            Mathf.Pow(p, 3) -
                                                                                            108 * k * m * q *
                                                                                            Mathf.Pow(p, 3) +
                                                                                            108 * a * n * q *
                                                                                            Mathf.Pow(p, 3) -
                                                                                            108 * j * n * q *
                                                                                            Mathf.Pow(p, 3) +
                                                                                            108 * c * m * r *
                                                                                            Mathf.Pow(p, 3) -
                                                                                            108 * l * m * r *
                                                                                            Mathf.Pow(p, 3) +
                                                                                            108 * a * o * r *
                                                                                            Mathf.Pow(p, 3) -
                                                                                            108 * j * o * r *
                                                                                            Mathf.Pow(p, 3) -
                                                                                            108 * a * m * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            108 * j * m * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            108 * b * n * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            108 * k * n * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            216 * c * o * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            216 * l * o * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            108 * a * m * Mathf.Pow(r,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            108 * j * m * Mathf.Pow(r,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            216 * b * n * Mathf.Pow(r,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            216 * k * n * Mathf.Pow(r,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            108 * c * o * Mathf.Pow(r,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            108 * l * o * Mathf.Pow(r,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            54 * Mathf.Pow(n, 3) * q *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            54 * n * Mathf.Pow(o, 2) *
                                                                                            q *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            108 * Mathf.Pow(m, 2) * n *
                                                                                            q *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            54 * Mathf.Pow(o, 3) * r *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            108 * Mathf.Pow(m, 2) * o *
                                                                                            r *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            54 * Mathf.Pow(n, 2) * o *
                                                                                            r *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            108 * c * n * q * r *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            108 * l * n * q * r *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            108 * b * o * q * r *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            108 * k * o * q * r *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            108 * b * m *
                                                                                            Mathf.Pow(q, 3) * p -
                                                                                            108 * k * m *
                                                                                            Mathf.Pow(q, 3) * p +
                                                                                            108 * a * n *
                                                                                            Mathf.Pow(q, 3) * p -
                                                                                            108 * j * n *
                                                                                            Mathf.Pow(q, 3) * p +
                                                                                            108 * c * m *
                                                                                            Mathf.Pow(r, 3) * p -
                                                                                            108 * l * m *
                                                                                            Mathf.Pow(r, 3) * p +
                                                                                            108 * a * o *
                                                                                            Mathf.Pow(r, 3) * p -
                                                                                            108 * j * o *
                                                                                            Mathf.Pow(r, 3) * p +
                                                                                            54 * Mathf.Pow(m, 3) *
                                                                                            Mathf.Pow(q, 2) * p -
                                                                                            108 * m * Mathf.Pow(n, 2) *
                                                                                            Mathf.Pow(q, 2) * p +
                                                                                            54 * m * Mathf.Pow(o, 2) *
                                                                                            Mathf.Pow(q, 2) * p +
                                                                                            54 * Mathf.Pow(m, 3) *
                                                                                            Mathf.Pow(r, 2) * p +
                                                                                            54 * m * Mathf.Pow(n, 2) *
                                                                                            Mathf.Pow(r, 2) * p -
                                                                                            108 * m * Mathf.Pow(o, 2) *
                                                                                            Mathf.Pow(r, 2) * p +
                                                                                            108 * b * m * q *
                                                                                            Mathf.Pow(r, 2) *
                                                                                            p -
                                                                                            108 * k * m * q *
                                                                                            Mathf.Pow(r, 2) *
                                                                                            p +
                                                                                            108 * a * n * q *
                                                                                            Mathf.Pow(r, 2) *
                                                                                            p -
                                                                                            108 * j * n * q *
                                                                                            Mathf.Pow(r, 2) *
                                                                                            p +
                                                                                            108 * c * m *
                                                                                            Mathf.Pow(q, 2) * r *
                                                                                            p -
                                                                                            108 * l * m *
                                                                                            Mathf.Pow(q, 2) * r *
                                                                                            p +
                                                                                            108 * a * o *
                                                                                            Mathf.Pow(q, 2) * r *
                                                                                            p -
                                                                                            108 * j * o *
                                                                                            Mathf.Pow(q, 2) * r *
                                                                                            p - 324 * m * n * o * q *
                                                                                            r * p -
                                                                                            108 * a * m * Mathf.Pow(q,
                                                                                                4) +
                                                                                            108 * j * m * Mathf.Pow(q,
                                                                                                4) -
                                                                                            108 * c * o * Mathf.Pow(q,
                                                                                                4) +
                                                                                            108 * l * o * Mathf.Pow(q,
                                                                                                4) -
                                                                                            108 * a * m * Mathf.Pow(r,
                                                                                                4) +
                                                                                            108 * j * m * Mathf.Pow(r,
                                                                                                4) -
                                                                                            108 * b * n * Mathf.Pow(r,
                                                                                                4) +
                                                                                            108 * k * n * Mathf.Pow(r,
                                                                                                4) +
                                                                                            54 * n * Mathf.Pow(o, 2) *
                                                                                            Mathf.Pow(q, 3) +
                                                                                            54 * Mathf.Pow(m, 2) * n *
                                                                                            Mathf.Pow(q, 3) +
                                                                                            54 * Mathf.Pow(m, 2) * o *
                                                                                            Mathf.Pow(r, 3) +
                                                                                            54 * Mathf.Pow(n, 2) * o *
                                                                                            Mathf.Pow(r, 3) +
                                                                                            108 * c * n * q *
                                                                                            Mathf.Pow(r, 3) -
                                                                                            108 * l * n * q *
                                                                                            Mathf.Pow(r, 3) +
                                                                                            108 * b * o * q *
                                                                                            Mathf.Pow(r, 3) -
                                                                                            108 * k * o * q *
                                                                                            Mathf.Pow(r, 3) -
                                                                                            216 * a * m * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(r, 2) +
                                                                                            216 * j * m * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(r, 2) -
                                                                                            108 * b * n * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(r, 2) +
                                                                                            108 * k * n * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(r, 2) -
                                                                                            108 * c * o * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(r, 2) +
                                                                                            108 * l * o * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(r, 2) +
                                                                                            54 * Mathf.Pow(n, 3) * q *
                                                                                            Mathf.Pow(r, 2) -
                                                                                            108 * n * Mathf.Pow(o, 2) *
                                                                                            q *
                                                                                            Mathf.Pow(r, 2) +
                                                                                            54 * Mathf.Pow(m, 2) * n *
                                                                                            q *
                                                                                            Mathf.Pow(r, 2) +
                                                                                            108 * c * n *
                                                                                            Mathf.Pow(q, 3) * r -
                                                                                            108 * l * n *
                                                                                            Mathf.Pow(q, 3) * r +
                                                                                            108 * b * o *
                                                                                            Mathf.Pow(q, 3) * r -
                                                                                            108 * k * o *
                                                                                            Mathf.Pow(q, 3) * r +
                                                                                            54 * Mathf.Pow(o, 3) *
                                                                                            Mathf.Pow(q, 2) * r +
                                                                                            54 * Mathf.Pow(m, 2) * o *
                                                                                            Mathf.Pow(q, 2) * r -
                                                                                            108 * Mathf.Pow(n, 2) * o *
                                                                                            Mathf.Pow(q, 2) * r), 2))),
                                                                            (1f / 3)));

            // Solution 3
            float x3 = -(m * p + n * q + o * r) / (2 * (Mathf.Pow(p, 2) + Mathf.Pow(q, 2) + Mathf.Pow(r, 2))) -
                       ((1 + i * Mathf.Sqrt(3)) * Mathf.Pow(
                           (-108 * b * n * Mathf.Pow(p, 4) + 108 * k * n * Mathf.Pow(p, 4) -
                               108 * c * o * Mathf.Pow(p, 4) +
                               108 * l * o * Mathf.Pow(p, 4) + 54 * m * Mathf.Pow(n, 2) * Mathf.Pow(p, 3) +
                               54 * m * Mathf.Pow(o, 2) * Mathf.Pow(p, 3) + 108 * b * m * q * Mathf.Pow(p, 3) -
                               108 * k * m * q * Mathf.Pow(p, 3) + 108 * a * n * q * Mathf.Pow(p, 3) -
                               108 * j * n * q * Mathf.Pow(p, 3) + 108 * c * m * r * Mathf.Pow(p, 3) -
                               108 * l * m * r * Mathf.Pow(p, 3) + 108 * a * o * r * Mathf.Pow(p, 3) -
                               108 * j * o * r * Mathf.Pow(p, 3) - 108 * a * m * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                               108 * j * m * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                               108 * b * n * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                               108 * k * n * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                               216 * c * o * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                               216 * l * o * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                               108 * a * m * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                               108 * j * m * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) -
                               216 * b * n * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                               216 * k * n * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) -
                               108 * c * o * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                               108 * l * o * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                               54 * Mathf.Pow(n, 3) * q * Mathf.Pow(p, 2) +
                               54 * n * Mathf.Pow(o, 2) * q * Mathf.Pow(p, 2) -
                               108 * Mathf.Pow(m, 2) * n * q * Mathf.Pow(p, 2) +
                               54 * Mathf.Pow(o, 3) * r * Mathf.Pow(p, 2) -
                               108 * Mathf.Pow(m, 2) * o * r * Mathf.Pow(p, 2) +
                               54 * Mathf.Pow(n, 2) * o * r * Mathf.Pow(p, 2) + 108 * c * n * q * r * Mathf.Pow(p, 2) -
                               108 * l * n * q * r * Mathf.Pow(p, 2) + 108 * b * o * q * r * Mathf.Pow(p, 2) -
                               108 * k * o * q * r * Mathf.Pow(p, 2) + 108 * b * m * Mathf.Pow(q, 3) * p -
                               108 * k * m * Mathf.Pow(q, 3) * p + 108 * a * n * Mathf.Pow(q, 3) * p -
                               108 * j * n * Mathf.Pow(q, 3) * p + 108 * c * m * Mathf.Pow(r, 3) * p -
                               108 * l * m * Mathf.Pow(r, 3) * p + 108 * a * o * Mathf.Pow(r, 3) * p -
                               108 * j * o * Mathf.Pow(r, 3) * p + 54 * Mathf.Pow(m, 3) * Mathf.Pow(q, 2) * p -
                               108 * m * Mathf.Pow(n, 2) * Mathf.Pow(q, 2) * p +
                               54 * m * Mathf.Pow(o, 2) * Mathf.Pow(q, 2) * p +
                               54 * Mathf.Pow(m, 3) * Mathf.Pow(r, 2) * p +
                               54 * m * Mathf.Pow(n, 2) * Mathf.Pow(r, 2) * p -
                               108 * m * Mathf.Pow(o, 2) * Mathf.Pow(r, 2) * p + 108 * b * m * q * Mathf.Pow(r, 2) * p -
                               108 * k * m * q * Mathf.Pow(r, 2) * p + 108 * a * n * q * Mathf.Pow(r, 2) * p -
                               108 * j * n * q * Mathf.Pow(r, 2) * p + 108 * c * m * Mathf.Pow(q, 2) * r * p -
                               108 * l * m * Mathf.Pow(q, 2) * r * p + 108 * a * o * Mathf.Pow(q, 2) * r * p -
                               108 * j * o * Mathf.Pow(q, 2) * r * p - 324 * m * n * o * q * r * p -
                               108 * a * m * Mathf.Pow(q, 4) + 108 * j * m * Mathf.Pow(q, 4) -
                               108 * c * o * Mathf.Pow(q, 4) +
                               108 * l * o * Mathf.Pow(q, 4) - 108 * a * m * Mathf.Pow(r, 4) +
                               108 * j * m * Mathf.Pow(r, 4) -
                               108 * b * n * Mathf.Pow(r, 4) + 108 * k * n * Mathf.Pow(r, 4) +
                               54 * n * Mathf.Pow(o, 2) * Mathf.Pow(q, 3) + 54 * Mathf.Pow(m, 2) * n * Mathf.Pow(q, 3) +
                               54 * Mathf.Pow(m, 2) * o * Mathf.Pow(r, 3) + 54 * Mathf.Pow(n, 2) * o * Mathf.Pow(r, 3) +
                               108 * c * n * q * Mathf.Pow(r, 3) - 108 * l * n * q * Mathf.Pow(r, 3) +
                               108 * b * o * q * Mathf.Pow(r, 3) - 108 * k * o * q * Mathf.Pow(r, 3) -
                               216 * a * m * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                               216 * j * m * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) -
                               108 * b * n * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                               108 * k * n * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) -
                               108 * c * o * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                               108 * l * o * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                               54 * Mathf.Pow(n, 3) * q * Mathf.Pow(r, 2) -
                               108 * n * Mathf.Pow(o, 2) * q * Mathf.Pow(r, 2) +
                               54 * Mathf.Pow(m, 2) * n * q * Mathf.Pow(r, 2) + 108 * c * n * Mathf.Pow(q, 3) * r -
                               108 * l * n * Mathf.Pow(q, 3) * r + 108 * b * o * Mathf.Pow(q, 3) * r -
                               108 * k * o * Mathf.Pow(q, 3) * r + 54 * Mathf.Pow(o, 3) * Mathf.Pow(q, 2) * r +
                               54 * Mathf.Pow(m, 2) * o * Mathf.Pow(q, 2) * r -
                               108 * Mathf.Pow(n, 2) * o * Mathf.Pow(q, 2) * r + Mathf.Sqrt(
                                   4 * Mathf.Pow(
                                       (6 * (Mathf.Pow(m, 2) + Mathf.Pow(n, 2) + Mathf.Pow(o, 2) + 2 * a * p -
                                            2 * j * p +
                                            2 * b * q - 2 * k * q + 2 * c * r - 2 * l * r) *
                                        (Mathf.Pow(p, 2) + Mathf.Pow(q, 2) + Mathf.Pow(r, 2)) -
                                        9 * Mathf.Pow((m * p + n * q + o * r), 2)), 3) + Mathf.Pow(
                                       (-108 * b * n * Mathf.Pow(p, 4) + 108 * k * n * Mathf.Pow(p, 4) -
                                        108 * c * o * Mathf.Pow(p, 4) + 108 * l * o * Mathf.Pow(p, 4) +
                                        54 * m * Mathf.Pow(n, 2) * Mathf.Pow(p, 3) +
                                        54 * m * Mathf.Pow(o, 2) * Mathf.Pow(p, 3) + 108 * b * m * q * Mathf.Pow(p, 3) -
                                        108 * k * m * q * Mathf.Pow(p, 3) + 108 * a * n * q * Mathf.Pow(p, 3) -
                                        108 * j * n * q * Mathf.Pow(p, 3) + 108 * c * m * r * Mathf.Pow(p, 3) -
                                        108 * l * m * r * Mathf.Pow(p, 3) + 108 * a * o * r * Mathf.Pow(p, 3) -
                                        108 * j * o * r * Mathf.Pow(p, 3) -
                                        108 * a * m * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                                        108 * j * m * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                                        108 * b * n * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                                        108 * k * n * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                                        216 * c * o * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) +
                                        216 * l * o * Mathf.Pow(q, 2) * Mathf.Pow(p, 2) -
                                        108 * a * m * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                                        108 * j * m * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) -
                                        216 * b * n * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                                        216 * k * n * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) -
                                        108 * c * o * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                                        108 * l * o * Mathf.Pow(r, 2) * Mathf.Pow(p, 2) +
                                        54 * Mathf.Pow(n, 3) * q * Mathf.Pow(p, 2) +
                                        54 * n * Mathf.Pow(o, 2) * q * Mathf.Pow(p, 2) -
                                        108 * Mathf.Pow(m, 2) * n * q * Mathf.Pow(p, 2) +
                                        54 * Mathf.Pow(o, 3) * r * Mathf.Pow(p, 2) -
                                        108 * Mathf.Pow(m, 2) * o * r * Mathf.Pow(p, 2) +
                                        54 * Mathf.Pow(n, 2) * o * r * Mathf.Pow(p, 2) +
                                        108 * c * n * q * r * Mathf.Pow(p, 2) - 108 * l * n * q * r * Mathf.Pow(p, 2) +
                                        108 * b * o * q * r * Mathf.Pow(p, 2) - 108 * k * o * q * r * Mathf.Pow(p, 2) +
                                        108 * b * m * Mathf.Pow(q, 3) * p - 108 * k * m * Mathf.Pow(q, 3) * p +
                                        108 * a * n * Mathf.Pow(q, 3) * p - 108 * j * n * Mathf.Pow(q, 3) * p +
                                        108 * c * m * Mathf.Pow(r, 3) * p - 108 * l * m * Mathf.Pow(r, 3) * p +
                                        108 * a * o * Mathf.Pow(r, 3) * p - 108 * j * o * Mathf.Pow(r, 3) * p +
                                        54 * Mathf.Pow(m, 3) * Mathf.Pow(q, 2) * p -
                                        108 * m * Mathf.Pow(n, 2) * Mathf.Pow(q, 2) * p +
                                        54 * m * Mathf.Pow(o, 2) * Mathf.Pow(q, 2) * p +
                                        54 * Mathf.Pow(m, 3) * Mathf.Pow(r, 2) * p +
                                        54 * m * Mathf.Pow(n, 2) * Mathf.Pow(r, 2) * p -
                                        108 * m * Mathf.Pow(o, 2) * Mathf.Pow(r, 2) * p +
                                        108 * b * m * q * Mathf.Pow(r, 2) * p - 108 * k * m * q * Mathf.Pow(r, 2) * p +
                                        108 * a * n * q * Mathf.Pow(r, 2) * p - 108 * j * n * q * Mathf.Pow(r, 2) * p +
                                        108 * c * m * Mathf.Pow(q, 2) * r * p - 108 * l * m * Mathf.Pow(q, 2) * r * p +
                                        108 * a * o * Mathf.Pow(q, 2) * r * p - 108 * j * o * Mathf.Pow(q, 2) * r * p -
                                        324 * m * n * o * q * r * p - 108 * a * m * Mathf.Pow(q, 4) +
                                        108 * j * m * Mathf.Pow(q, 4) - 108 * c * o * Mathf.Pow(q, 4) +
                                        108 * l * o * Mathf.Pow(q, 4) - 108 * a * m * Mathf.Pow(r, 4) +
                                        108 * j * m * Mathf.Pow(r, 4) - 108 * b * n * Mathf.Pow(r, 4) +
                                        108 * k * n * Mathf.Pow(r, 4) + 54 * n * Mathf.Pow(o, 2) * Mathf.Pow(q, 3) +
                                        54 * Mathf.Pow(m, 2) * n * Mathf.Pow(q, 3) +
                                        54 * Mathf.Pow(m, 2) * o * Mathf.Pow(r, 3) +
                                        54 * Mathf.Pow(n, 2) * o * Mathf.Pow(r, 3) + 108 * c * n * q * Mathf.Pow(r, 3) -
                                        108 * l * n * q * Mathf.Pow(r, 3) + 108 * b * o * q * Mathf.Pow(r, 3) -
                                        108 * k * o * q * Mathf.Pow(r, 3) -
                                        216 * a * m * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                                        216 * j * m * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) -
                                        108 * b * n * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                                        108 * k * n * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) -
                                        108 * c * o * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                                        108 * l * o * Mathf.Pow(q, 2) * Mathf.Pow(r, 2) +
                                        54 * Mathf.Pow(n, 3) * q * Mathf.Pow(r, 2) -
                                        108 * n * Mathf.Pow(o, 2) * q * Mathf.Pow(r, 2) +
                                        54 * Mathf.Pow(m, 2) * n * q * Mathf.Pow(r, 2) +
                                        108 * c * n * Mathf.Pow(q, 3) * r -
                                        108 * l * n * Mathf.Pow(q, 3) * r + 108 * b * o * Mathf.Pow(q, 3) * r -
                                        108 * k * o * Mathf.Pow(q, 3) * r + 54 * Mathf.Pow(o, 3) * Mathf.Pow(q, 2) * r +
                                        54 * Mathf.Pow(m, 2) * o * Mathf.Pow(q, 2) * r -
                                        108 * Mathf.Pow(n, 2) * o * Mathf.Pow(q, 2) * r), 2))), (1f / 3))) /
                       (12 * Mathf.Pow(2, (1f / 3)) * (Mathf.Pow(p, 2) + Mathf.Pow(q, 2) + Mathf.Pow(r, 2))) +
                       ((1 - i * Mathf.Sqrt(3)) *
                        (6 * (Mathf.Pow(m, 2) + Mathf.Pow(n, 2) + Mathf.Pow(o, 2) + 2 * a * p - 2 * j * p + 2 * b * q -
                             2 * k * q + 2 * c * r - 2 * l * r) *
                         (Mathf.Pow(p, 2) + Mathf.Pow(q, 2) + Mathf.Pow(r, 2)) -
                         9 * Mathf.Pow((m * p + n * q + o * r), 2))) / (6 * Mathf.Pow(2, (2f / 3)) *
                                                                        (Mathf.Pow(p, 2) + Mathf.Pow(q, 2) +
                                                                         Mathf.Pow(r, 2)) *
                                                                        Mathf.Pow(
                                                                            (-108 * b * n * Mathf.Pow(p, 4) +
                                                                                108 * k * n * Mathf.Pow(p, 4) -
                                                                                108 * c * o * Mathf.Pow(p, 4) +
                                                                                108 * l * o * Mathf.Pow(p, 4) +
                                                                                54 * m * Mathf.Pow(n, 2) *
                                                                                Mathf.Pow(p, 3) +
                                                                                54 * m * Mathf.Pow(o, 2) *
                                                                                Mathf.Pow(p, 3) +
                                                                                108 * b * m * q * Mathf.Pow(p, 3) -
                                                                                108 * k * m * q * Mathf.Pow(p, 3) +
                                                                                108 * a * n * q * Mathf.Pow(p, 3) -
                                                                                108 * j * n * q * Mathf.Pow(p, 3) +
                                                                                108 * c * m * r * Mathf.Pow(p, 3) -
                                                                                108 * l * m * r * Mathf.Pow(p, 3) +
                                                                                108 * a * o * r * Mathf.Pow(p, 3) -
                                                                                108 * j * o * r * Mathf.Pow(p, 3) -
                                                                                108 * a * m * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(p, 2) +
                                                                                108 * j * m * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(p, 2) -
                                                                                108 * b * n * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(p, 2) +
                                                                                108 * k * n * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(p, 2) -
                                                                                216 * c * o * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(p, 2) +
                                                                                216 * l * o * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(p, 2) -
                                                                                108 * a * m * Mathf.Pow(r, 2) *
                                                                                Mathf.Pow(p, 2) +
                                                                                108 * j * m * Mathf.Pow(r, 2) *
                                                                                Mathf.Pow(p, 2) -
                                                                                216 * b * n * Mathf.Pow(r, 2) *
                                                                                Mathf.Pow(p, 2) +
                                                                                216 * k * n * Mathf.Pow(r, 2) *
                                                                                Mathf.Pow(p, 2) -
                                                                                108 * c * o * Mathf.Pow(r, 2) *
                                                                                Mathf.Pow(p, 2) +
                                                                                108 * l * o * Mathf.Pow(r, 2) *
                                                                                Mathf.Pow(p, 2) +
                                                                                54 * Mathf.Pow(n, 3) * q *
                                                                                Mathf.Pow(p, 2) +
                                                                                54 * n * Mathf.Pow(o, 2) * q *
                                                                                Mathf.Pow(p, 2) -
                                                                                108 * Mathf.Pow(m, 2) * n * q *
                                                                                Mathf.Pow(p, 2) +
                                                                                54 * Mathf.Pow(o, 3) * r *
                                                                                Mathf.Pow(p, 2) -
                                                                                108 * Mathf.Pow(m, 2) * o * r *
                                                                                Mathf.Pow(p, 2) +
                                                                                54 * Mathf.Pow(n, 2) * o * r *
                                                                                Mathf.Pow(p, 2) +
                                                                                108 * c * n * q * r * Mathf.Pow(p, 2) -
                                                                                108 * l * n * q * r * Mathf.Pow(p, 2) +
                                                                                108 * b * o * q * r * Mathf.Pow(p, 2) -
                                                                                108 * k * o * q * r * Mathf.Pow(p, 2) +
                                                                                108 * b * m * Mathf.Pow(q, 3) * p -
                                                                                108 * k * m * Mathf.Pow(q, 3) * p +
                                                                                108 * a * n * Mathf.Pow(q, 3) * p -
                                                                                108 * j * n * Mathf.Pow(q, 3) * p +
                                                                                108 * c * m * Mathf.Pow(r, 3) * p -
                                                                                108 * l * m * Mathf.Pow(r, 3) * p +
                                                                                108 * a * o * Mathf.Pow(r, 3) * p -
                                                                                108 * j * o * Mathf.Pow(r, 3) * p +
                                                                                54 * Mathf.Pow(m, 3) * Mathf.Pow(q, 2) *
                                                                                p -
                                                                                108 * m * Mathf.Pow(n, 2) *
                                                                                Mathf.Pow(q, 2) *
                                                                                p +
                                                                                54 * m * Mathf.Pow(o, 2) *
                                                                                Mathf.Pow(q, 2) * p +
                                                                                54 * Mathf.Pow(m, 3) * Mathf.Pow(r, 2) *
                                                                                p +
                                                                                54 * m * Mathf.Pow(n, 2) *
                                                                                Mathf.Pow(r, 2) * p -
                                                                                108 * m * Mathf.Pow(o, 2) *
                                                                                Mathf.Pow(r, 2) *
                                                                                p + 108 * b * m * q * Mathf.Pow(r, 2) *
                                                                                p -
                                                                                108 * k * m * q * Mathf.Pow(r, 2) * p +
                                                                                108 * a * n * q * Mathf.Pow(r, 2) * p -
                                                                                108 * j * n * q * Mathf.Pow(r, 2) * p +
                                                                                108 * c * m * Mathf.Pow(q, 2) * r * p -
                                                                                108 * l * m * Mathf.Pow(q, 2) * r * p +
                                                                                108 * a * o * Mathf.Pow(q, 2) * r * p -
                                                                                108 * j * o * Mathf.Pow(q, 2) * r * p -
                                                                                324 * m * n * o * q * r * p -
                                                                                108 * a * m * Mathf.Pow(q, 4) +
                                                                                108 * j * m * Mathf.Pow(q, 4) -
                                                                                108 * c * o * Mathf.Pow(q, 4) +
                                                                                108 * l * o * Mathf.Pow(q, 4) -
                                                                                108 * a * m * Mathf.Pow(r, 4) +
                                                                                108 * j * m * Mathf.Pow(r, 4) -
                                                                                108 * b * n * Mathf.Pow(r, 4) +
                                                                                108 * k * n * Mathf.Pow(r, 4) +
                                                                                54 * n * Mathf.Pow(o, 2) *
                                                                                Mathf.Pow(q, 3) +
                                                                                54 * Mathf.Pow(m, 2) * n *
                                                                                Mathf.Pow(q, 3) +
                                                                                54 * Mathf.Pow(m, 2) * o *
                                                                                Mathf.Pow(r, 3) +
                                                                                54 * Mathf.Pow(n, 2) * o *
                                                                                Mathf.Pow(r, 3) +
                                                                                108 * c * n * q * Mathf.Pow(r, 3) -
                                                                                108 * l * n * q * Mathf.Pow(r, 3) +
                                                                                108 * b * o * q * Mathf.Pow(r, 3) -
                                                                                108 * k * o * q * Mathf.Pow(r, 3) -
                                                                                216 * a * m * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(r, 2) +
                                                                                216 * j * m * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(r, 2) -
                                                                                108 * b * n * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(r, 2) +
                                                                                108 * k * n * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(r, 2) -
                                                                                108 * c * o * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(r, 2) +
                                                                                108 * l * o * Mathf.Pow(q, 2) *
                                                                                Mathf.Pow(r, 2) +
                                                                                54 * Mathf.Pow(n, 3) * q *
                                                                                Mathf.Pow(r, 2) -
                                                                                108 * n * Mathf.Pow(o, 2) * q *
                                                                                Mathf.Pow(r, 2) +
                                                                                54 * Mathf.Pow(m, 2) * n * q *
                                                                                Mathf.Pow(r, 2) +
                                                                                108 * c * n * Mathf.Pow(q, 3) * r -
                                                                                108 * l * n * Mathf.Pow(q, 3) * r +
                                                                                108 * b * o * Mathf.Pow(q, 3) * r -
                                                                                108 * k * o * Mathf.Pow(q, 3) * r +
                                                                                54 * Mathf.Pow(o, 3) * Mathf.Pow(q, 2) *
                                                                                r +
                                                                                54 * Mathf.Pow(m, 2) * o *
                                                                                Mathf.Pow(q, 2) * r -
                                                                                108 * Mathf.Pow(n, 2) * o *
                                                                                Mathf.Pow(q, 2) *
                                                                                r + Mathf.Sqrt(
                                                                                    4 * Mathf.Pow(
                                                                                        (6 * (Mathf.Pow(m, 2) +
                                                                                                Mathf.Pow(n, 2) +
                                                                                                Mathf.Pow(o, 2) +
                                                                                                2 * a * p - 2 * j * p +
                                                                                                2 * b * q -
                                                                                                2 * k * q + 2 * c * r -
                                                                                                2 * l * r) *
                                                                                            (Mathf.Pow(p, 2) +
                                                                                                Mathf.Pow(q, 2) +
                                                                                                Mathf.Pow(r, 2)) -
                                                                                            9 * Mathf.Pow(
                                                                                                (m * p + n * q + o * r),
                                                                                                2)), 3) + Mathf.Pow(
                                                                                        (-108 * b * n *
                                                                                            Mathf.Pow(p, 4) +
                                                                                            108 * k * n * Mathf.Pow(p,
                                                                                                4) -
                                                                                            108 * c * o * Mathf.Pow(p,
                                                                                                4) +
                                                                                            108 * l * o * Mathf.Pow(p,
                                                                                                4) +
                                                                                            54 * m * Mathf.Pow(n, 2) *
                                                                                            Mathf.Pow(p, 3) +
                                                                                            54 * m * Mathf.Pow(o, 2) *
                                                                                            Mathf.Pow(p, 3) +
                                                                                            108 * b * m * q *
                                                                                            Mathf.Pow(p, 3) -
                                                                                            108 * k * m * q *
                                                                                            Mathf.Pow(p, 3) +
                                                                                            108 * a * n * q *
                                                                                            Mathf.Pow(p, 3) -
                                                                                            108 * j * n * q *
                                                                                            Mathf.Pow(p, 3) +
                                                                                            108 * c * m * r *
                                                                                            Mathf.Pow(p, 3) -
                                                                                            108 * l * m * r *
                                                                                            Mathf.Pow(p, 3) +
                                                                                            108 * a * o * r *
                                                                                            Mathf.Pow(p, 3) -
                                                                                            108 * j * o * r *
                                                                                            Mathf.Pow(p, 3) -
                                                                                            108 * a * m * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            108 * j * m * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            108 * b * n * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            108 * k * n * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            216 * c * o * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            216 * l * o * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            108 * a * m * Mathf.Pow(r,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            108 * j * m * Mathf.Pow(r,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            216 * b * n * Mathf.Pow(r,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            216 * k * n * Mathf.Pow(r,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            108 * c * o * Mathf.Pow(r,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            108 * l * o * Mathf.Pow(r,
                                                                                                2) *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            54 * Mathf.Pow(n, 3) * q *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            54 * n * Mathf.Pow(o, 2) *
                                                                                            q *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            108 * Mathf.Pow(m, 2) * n *
                                                                                            q *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            54 * Mathf.Pow(o, 3) * r *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            108 * Mathf.Pow(m, 2) * o *
                                                                                            r *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            54 * Mathf.Pow(n, 2) * o *
                                                                                            r *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            108 * c * n * q * r *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            108 * l * n * q * r *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            108 * b * o * q * r *
                                                                                            Mathf.Pow(p, 2) -
                                                                                            108 * k * o * q * r *
                                                                                            Mathf.Pow(p, 2) +
                                                                                            108 * b * m *
                                                                                            Mathf.Pow(q, 3) * p -
                                                                                            108 * k * m *
                                                                                            Mathf.Pow(q, 3) * p +
                                                                                            108 * a * n *
                                                                                            Mathf.Pow(q, 3) * p -
                                                                                            108 * j * n *
                                                                                            Mathf.Pow(q, 3) * p +
                                                                                            108 * c * m *
                                                                                            Mathf.Pow(r, 3) * p -
                                                                                            108 * l * m *
                                                                                            Mathf.Pow(r, 3) * p +
                                                                                            108 * a * o *
                                                                                            Mathf.Pow(r, 3) * p -
                                                                                            108 * j * o *
                                                                                            Mathf.Pow(r, 3) * p +
                                                                                            54 * Mathf.Pow(m, 3) *
                                                                                            Mathf.Pow(q, 2) * p -
                                                                                            108 * m * Mathf.Pow(n, 2) *
                                                                                            Mathf.Pow(q, 2) * p +
                                                                                            54 * m * Mathf.Pow(o, 2) *
                                                                                            Mathf.Pow(q, 2) * p +
                                                                                            54 * Mathf.Pow(m, 3) *
                                                                                            Mathf.Pow(r, 2) * p +
                                                                                            54 * m * Mathf.Pow(n, 2) *
                                                                                            Mathf.Pow(r, 2) * p -
                                                                                            108 * m * Mathf.Pow(o, 2) *
                                                                                            Mathf.Pow(r, 2) * p +
                                                                                            108 * b * m * q *
                                                                                            Mathf.Pow(r, 2) *
                                                                                            p -
                                                                                            108 * k * m * q *
                                                                                            Mathf.Pow(r, 2) *
                                                                                            p +
                                                                                            108 * a * n * q *
                                                                                            Mathf.Pow(r, 2) *
                                                                                            p -
                                                                                            108 * j * n * q *
                                                                                            Mathf.Pow(r, 2) *
                                                                                            p +
                                                                                            108 * c * m *
                                                                                            Mathf.Pow(q, 2) * r *
                                                                                            p -
                                                                                            108 * l * m *
                                                                                            Mathf.Pow(q, 2) * r *
                                                                                            p +
                                                                                            108 * a * o *
                                                                                            Mathf.Pow(q, 2) * r *
                                                                                            p -
                                                                                            108 * j * o *
                                                                                            Mathf.Pow(q, 2) * r *
                                                                                            p - 324 * m * n * o * q *
                                                                                            r * p -
                                                                                            108 * a * m * Mathf.Pow(q,
                                                                                                4) +
                                                                                            108 * j * m * Mathf.Pow(q,
                                                                                                4) -
                                                                                            108 * c * o * Mathf.Pow(q,
                                                                                                4) +
                                                                                            108 * l * o * Mathf.Pow(q,
                                                                                                4) -
                                                                                            108 * a * m * Mathf.Pow(r,
                                                                                                4) +
                                                                                            108 * j * m * Mathf.Pow(r,
                                                                                                4) -
                                                                                            108 * b * n * Mathf.Pow(r,
                                                                                                4) +
                                                                                            108 * k * n * Mathf.Pow(r,
                                                                                                4) +
                                                                                            54 * n * Mathf.Pow(o, 2) *
                                                                                            Mathf.Pow(q, 3) +
                                                                                            54 * Mathf.Pow(m, 2) * n *
                                                                                            Mathf.Pow(q, 3) +
                                                                                            54 * Mathf.Pow(m, 2) * o *
                                                                                            Mathf.Pow(r, 3) +
                                                                                            54 * Mathf.Pow(n, 2) * o *
                                                                                            Mathf.Pow(r, 3) +
                                                                                            108 * c * n * q *
                                                                                            Mathf.Pow(r, 3) -
                                                                                            108 * l * n * q *
                                                                                            Mathf.Pow(r, 3) +
                                                                                            108 * b * o * q *
                                                                                            Mathf.Pow(r, 3) -
                                                                                            108 * k * o * q *
                                                                                            Mathf.Pow(r, 3) -
                                                                                            216 * a * m * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(r, 2) +
                                                                                            216 * j * m * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(r, 2) -
                                                                                            108 * b * n * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(r, 2) +
                                                                                            108 * k * n * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(r, 2) -
                                                                                            108 * c * o * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(r, 2) +
                                                                                            108 * l * o * Mathf.Pow(q,
                                                                                                2) *
                                                                                            Mathf.Pow(r, 2) +
                                                                                            54 * Mathf.Pow(n, 3) * q *
                                                                                            Mathf.Pow(r, 2) -
                                                                                            108 * n * Mathf.Pow(o, 2) *
                                                                                            q *
                                                                                            Mathf.Pow(r, 2) +
                                                                                            54 * Mathf.Pow(m, 2) * n *
                                                                                            q *
                                                                                            Mathf.Pow(r, 2) +
                                                                                            108 * c * n *
                                                                                            Mathf.Pow(q, 3) * r -
                                                                                            108 * l * n *
                                                                                            Mathf.Pow(q, 3) * r +
                                                                                            108 * b * o *
                                                                                            Mathf.Pow(q, 3) * r -
                                                                                            108 * k * o *
                                                                                            Mathf.Pow(q, 3) * r +
                                                                                            54 * Mathf.Pow(o, 3) *
                                                                                            Mathf.Pow(q, 2) * r +
                                                                                            54 * Mathf.Pow(m, 2) * o *
                                                                                            Mathf.Pow(q, 2) * r -
                                                                                            108 * Mathf.Pow(n, 2) * o *
                                                                                            Mathf.Pow(q, 2) * r), 2))),
                                                                            (1f / 3)));

            x1 = Mathf.Clamp(x1, 0, 1);
            x2 = Mathf.Clamp(x2, 0, 1);
            x3 = Mathf.Clamp(x3, 0, 1);
            Debug.Log(x1 + " " + x2  + " " + x3);
            
            sol1 = GetIntermediate(x1);
            sol2 = GetIntermediate(x2);
            sol3 = GetIntermediate(x3);
        }
    }
}