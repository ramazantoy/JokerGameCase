using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.LeonsExtensions.Clamp;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.DiceScripts
{
    public class DiceController : MonoBehaviour
    {
        private readonly Vector3 _startPos = new Vector3(0, 10f, -10f);

        [SerializeField] private ClampVal _randomX;
        [SerializeField] private ClampVal _randomZ;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RandomJump();
            }
        }

        private void RandomJump()
        {
            transform.DOKill();
            var startPos = new Vector3(Random.Range(-1f, 1f), 0, 0) + _startPos;

            transform.position = startPos;
            var randomRotation = new Vector3(Random.Range(-360f, 360f), Random.Range(-360f, 360f),
                Random.Range(-360f, 360f));
            transform.rotation = Quaternion.Euler(randomRotation);
            var randomEndTarget = new Vector3(_randomX.RandomValue, 0, _randomZ.RandomValue);
            var pathList = LeonsExtensions.LeonsMath.ParabolaPoints(transform.position, randomEndTarget, 5f);

            transform.DOPath(pathList.ToArray(), .75f);

            transform.DORotate(Vector3.zero, 1f);
        }
    }
}