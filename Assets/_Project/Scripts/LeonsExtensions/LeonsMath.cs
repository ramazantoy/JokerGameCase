using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace _Project.Scripts.LeonsExtensions
{
    public static class LeonsMath
    {
        
        public static Vector3[] GetParabolaPoints(Vector3 a, Vector3 c, float height = 1f, int quality = 100)
        {
            Vector3[] final = new Vector3[quality];

            Vector3 b = (a + c) / 2;
            b.y = height;
            Vector3 q;
            Vector3 r;
            Vector3 p;

            float interval = 1 / (float)quality;
            float t = 0;

            for (int i = 0; i < quality; i++)
            {
                t += interval;

                q = (1 - t) * a + t * b;
                r = (1 - t) * b + t * c;
                p = (1 - t) * q + t * r;
                final[i] = p;
            }

            return final;
        }

        public static List<Vector2> GenerateRandomPointsOnCircle(RectTransform center, float pointRadius, int count)
        {
            List<Vector2> points = new List<Vector2>();

            for (int i = 0; i < count; i++)
            {
                float angle = Random.Range(0f, 360f);
                Vector2 spawnPosition = GetPointOnCircle(center.position, pointRadius, angle);
                points.Add(spawnPosition);
            }

            return points;
        }

        private static Vector2 GetPointOnCircle(Vector2 center, float radius, float angle)
        {
            float radians = angle * Mathf.Deg2Rad;
            float x = center.x + radius * Mathf.Cos(radians);
            float y = center.y + radius * Mathf.Sin(radians);

            return new Vector2(x, y);
        }


        public static float InvoluteOfCircleX(float a, float theta)
        {
            return a * (Mathf.Cos(theta) + theta * Mathf.Sin(theta));
        }

        public static float InvoluteOfCircleY(float a, float theta)
        {
            return a * (Mathf.Sin(theta) - theta * Mathf.Cos(theta));
        }

        public static float InvoluteOfCircleZ(float b, float theta)
        {
            return b * theta;
        }


        //Spiral şekiller elde etmek için kullanılabilir
        public static Vector2 InvoluteOfCircle(float a, float theta)
        {
            return new Vector2(
                InvoluteOfCircleX(a, theta),
                InvoluteOfCircleY(a, theta)
            );
        }

        //Spiral şekiller elde etmek için kullanılabilir
        public static Vector3 InvoluteOfCircle(float a, float b, float theta)
        {
            return new Vector3(
                InvoluteOfCircleX(a, theta),
                InvoluteOfCircleY(a, theta),
                InvoluteOfCircleZ(b, theta)
            );
        }

        public static List<Vector3> GenerateRandomCirclePoints(Transform center, float radius, int numberOfPoints,Camera camera)
        {
            var points = new List<Vector3>();

            for (int i = 0; i < numberOfPoints; i++)
            {
                var angle = Random.Range(0, 2 * Mathf.PI);
                var distance = Random.Range(0, radius);
                var point = center.position +new Vector3(distance * Mathf.Cos(angle), 0, distance * Mathf.Sin(angle));

                points.Add(camera.WorldToScreenPoint(point));
            }

            return points;
        }

        public static Vector2Int GetVector2Diff(this Vector2Int current, Vector2Int target)
        {
            return target - current;
        }


        public static Vector2Int GetRandomVector2Element(ref Vector2Int current)
        {
            if (current == Vector2Int.zero) return Vector2Int.zero;
            
            var returnY = current.y == 0 ? 0 : -current.y / Mathf.Abs(current.y);
            var returnX = current.x == 0 ? 0 : -current.x / Mathf.Abs(current.x);
            
            var deleteX = Random.value < 0.5f;

            if (deleteX)
            {
                if (current.x == 0)
                {
                    current.y += returnY;
                    return new Vector2Int(0, returnY);
                }

                current.x += returnX;
                return new Vector2Int(returnX, 0);
            }

            if (current.y == 0)
            {
                current.x += returnX;
                return new Vector2Int(returnX, 0);
            }

            current.y += returnY;
            return new Vector2Int(0, returnY);
        }
    }
}