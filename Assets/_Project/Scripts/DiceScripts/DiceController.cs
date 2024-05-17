using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.LeonsExtensions.Clamp;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.DiceScripts
{
    public class DiceController : MonoBehaviour
    {
        private readonly Vector3 _startPos = new Vector3(4.4f, 10f, -2f);

        [SerializeField] private Rigidbody _diceRigidbody;

        [SerializeField] private List<Rigidbody> _dices2;


 

        private Vector3 _lastVelocity;
        private Vector3 _lastAngularVelocity;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RollDice( 60f);
            }
        }

        private void RollDice( float rollForce)
        {
         
            DestroyImmediate(transform.GetComponent<FixedJoint>());
            _dices2[0].isKinematic = true;
            _dices2[1].isKinematic = true;
            
            _diceRigidbody.velocity = Vector3.zero;
            _diceRigidbody.angularVelocity = Vector3.zero;

            transform.position = _startPos;
            transform.rotation = Random.rotation;

            
            var randomVariance = Random.Range(-1f, 1f);
         

            var rndX = Random.Range(-1f, 1f);
            var rndY = Random.Range(-1f, 1f);
            var rndZ = Random.Range(-1f, 1f);
       
            
            var rnd = Random.Range(0, 1f);
       
            if (rnd < .5f)
            {
                Debug.LogError("Dice : 5");
                _dices2[1].isKinematic = false;
                _dices2[1].velocity=Vector3.zero;
                _dices2[1].angularVelocity=Vector3.zero;
                transform.AddComponent<FixedJoint>().connectedBody = _dices2[1];
            }
            else
            {
                Debug.LogError("Dice : 3");
                _dices2[0].isKinematic = false;
                _dices2[0].velocity=Vector3.zero;
                _dices2[0].angularVelocity=Vector3.zero;
                transform.AddComponent<FixedJoint>().connectedBody = _dices2[0];

            }
            
            _diceRigidbody.AddForce(Vector3.down*50f,ForceMode.Impulse);
            _diceRigidbody.AddTorque(new Vector3(rndX, rndY, rndZ) * (rollForce * randomVariance), ForceMode.Impulse);
        }

    
    }
}