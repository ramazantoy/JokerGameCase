using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.LeonsExtensions.Clamp;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.DiceScripts
{
    public class DiceController : MonoBehaviour
    {
        private readonly Vector3 _startPos = new Vector3(10, 1f, -7.5f);
        
        [SerializeField] private Rigidbody _diceRigidbody;


      [SerializeField] private List<GameObject> _dices;


        private Vector3 _lastVelocity;
        private Vector3 _lastAngularVelocity;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RollDice(10f, 15f);
            }
        }

        private void RollDice(float throwForce, float rollForce)
        {
            _dices[0].SetActive(false);
            _dices[1].SetActive(false);
            _diceRigidbody.isKinematic = false;
            StopAllCoroutines();
            _diceRigidbody.velocity = Vector3.zero;
            _diceRigidbody.angularVelocity = Vector3.zero;

            transform.position = _startPos;
            transform.rotation = Random.rotation;


            var targetPos = new Vector3(Random.Range(-1f, 1f), Random.Range(4f, 5f), Random.Range(-1f, 1f));
            var randomVariance = Random.Range(-1f, 1f);
            _diceRigidbody.AddForce((targetPos - transform.position).normalized * (throwForce + randomVariance),
                ForceMode.Impulse);

            var rndX = Random.Range(-1f, 1f);
            var rndY = Random.Range(-1f, 1f);
            var rndZ = Random.Range(-1f, 1f);
            _diceRigidbody.AddTorque(new Vector3(rndX, rndY, rndZ) * (rollForce * randomVariance), ForceMode.Impulse);

            StartCoroutine(Cheat());
        }

        private IEnumerator Cheat()
        {
            yield return new WaitUntil((() => (Vector3.Distance(_diceRigidbody.velocity, Vector3.zero) <= .1f) || Vector3.Distance(_diceRigidbody.angularVelocity,Vector3.zero)<=.1f));
            var rnd = Random.Range(0, 1f);
            if (rnd < .5f)
            {
                Debug.LogError("Dice : 5");
                _dices[0].SetActive(false);
                _dices[1].SetActive(true);
            }
            else
            {
                Debug.LogError("Dice : 3");
                _dices[0].SetActive(true);
                _dices[1].SetActive(false);
            }
        }
    }
}