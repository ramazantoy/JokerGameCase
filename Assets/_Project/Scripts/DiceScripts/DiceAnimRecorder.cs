using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.DiceScripts
{
    public class DiceAnimRecorder : MonoBehaviour
    {
#if UNITY_EDITOR

        [SerializeField] private Rigidbody _diceRigidBody;

        public float ThrowForce = 10f;

        public float TorqueForce = 5f;

        public Vector3 Direction;

        void Start()
        {
            _diceRigidBody.velocity = Vector3.zero;
            _diceRigidBody.angularVelocity = Vector3.zero;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ThrowDice();
            }
        }

        void ThrowDice()
        {
            _diceRigidBody.AddForce(Direction * ThrowForce, ForceMode.Impulse);

            Vector3 randomTorque = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ) * TorqueForce;

            _diceRigidBody.AddTorque(randomTorque, ForceMode.Impulse);
        }
#endif
    }
}