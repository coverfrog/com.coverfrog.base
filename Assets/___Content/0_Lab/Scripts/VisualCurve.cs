using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lab
{
    public class VisualCurve : MonoBehaviour
    {
        [Header("___ Curve ___")]
        [SerializeField] private bool showGizmo;
        [SerializeField] private AnimationCurve curve;

        [Header("___ Move Option ___")] 
        [SerializeField] private Vector2 moveStartPoint;
        [SerializeField] [Min(0.01f)] private float moveDuration = 2.0f;
        [SerializeField] private float moveDistance = 10.0f;
        [SerializeField] private float moveMaxHeight = 5.0f; 
        
        [Header("___ Point Draw Option ___")]
        [SerializeField] private Color pointDrawColor = new Color(0, 1, 1, 1);
        [SerializeField] [Min(0.01F)] private float pointDrawRadius = 1.0f;
        [SerializeField] [Min(3)] private int pointDrawCount = 3;

        [Header("___ Components ___")] 
        [SerializeField] private RectTransform rtMine;

        private Vector2 _gizmoReduce;
        
        #region > Action
        
        [Button]
        private void MovingStart()
        {
            if (_coMoving != null)
            {
                StopCoroutine(_coMoving);
                _coMoving = null;
            }
            
            MoveFirst();

            _coMoving = CoMoving();
            StartCoroutine(_coMoving);
        }

        private void MoveFirst()
        {
            rtMine.anchoredPosition = moveStartPoint;
        }
        
        private void MovePointListGet(out List<Vector2> movePointList)
        {
            movePointList = new List<Vector2>();
            
            for (var t = 0.0f; t <= moveDuration; t += Time.deltaTime)
            {
                var percent = t / moveDuration;
                var evaluate = curve.Evaluate(percent);
            
                var x = percent * moveDistance;
                var y = evaluate * moveMaxHeight;

                var point = moveStartPoint + new Vector2(x, y);
                movePointList.Add(point);
                
                _gizmoReduce = new Vector2(x, y);
            }
        }
        
        
        #endregion

        #region > Co

        private IEnumerator _coMoving;

        private IEnumerator CoMoving()
        {
            MovePointListGet(out var movePointList);

            var idx = 0;
            
            for (var t = 0.0f; t <= moveDuration; t += Time.deltaTime)
            {
                var point = movePointList[idx];

                rtMine.anchoredPosition = point;

                idx++;

                yield return null;
            }

            _coMoving = null;
        }

        #endregion

        #region > Draw

        private void OnDrawGizmos()
        {
            if (!showGizmo || !rtMine)
            {
                return;
            }

            DrawLines();
        }

        private void DrawGizmoCircle(Vector2 center, float radius, int segments = 30)
        {
            Gizmos.color = pointDrawColor;

            var angleStep = 360.0f / segments;
            var pointPrev = center + new Vector2(radius, 0);

            for (var idx = 0; idx <= segments; ++idx)
            {
                var radian = (angleStep * idx) * Mathf.Deg2Rad;
                var xDir = Mathf.Cos(radian);
                var yDir = Mathf.Sin(radian);
                var pointNow = center + new Vector2(xDir, yDir) * radius;
                
                Gizmos.DrawLine(pointPrev, pointNow);

                pointPrev = pointNow;
            }
        }

        private void DrawLines()
        {
            var addPoint = rtMine.position;
            var startPoint = rtMine.position;
            
            var len = curve.length;
            var seg = len / (float)pointDrawCount;
            
            for (var t = 0.0f; t <= len; t += seg)
            {
                var percent = t / len;
                var evaluate = curve.Evaluate(percent);
            
                var x = percent * moveDistance;
                var y = evaluate * moveMaxHeight;
                var add = new Vector3(x, y, 0);
                
                var point = new Vector3(addPoint.x, addPoint.y, 0);
                
                point += add;
                addPoint = add * percent;
                
                DrawGizmoCircle(startPoint + point, pointDrawRadius);
            }
        }
        
        #endregion
    }
}