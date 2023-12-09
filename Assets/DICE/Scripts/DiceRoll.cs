using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DICE.Scripts
{
    public class DiceRoll: MonoBehaviour
    {
        [SerializeField] private float maxRandomForce;
        [SerializeField] private float maxRollingForce;
        [SerializeField] private Rigidbody m_rigidbody;
        
        private float forceX;
        private float forceY;
        private float forceZ;
        
        private int diceNum;

        private float m_startMass;
        private float m_startDrag;

        private void Awake()
        {
            m_startMass = m_rigidbody.mass;
            m_startDrag = m_rigidbody.drag;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                RollDice();
            }
        }

        private void RollDice()
        {
            m_rigidbody.isKinematic = false;

            var minForce = maxRandomForce * 0.1f;
            forceX = Random.Range(minForce, maxRandomForce);
            forceY = Random.Range(minForce, maxRandomForce);
            forceZ = Random.Range(minForce, maxRandomForce);
            
            Debug.Log($"forceX - {forceX}\n forceY - {forceY}\n forceZ - {forceZ}\n");

            var randomMassPlus = Random.Range(-1, 1);
            m_rigidbody.mass = m_startMass + randomMassPlus * m_startMass * 0.3f;
            
            var randomDragPlus = Random.Range(0, 0.2f);
            m_rigidbody.drag = 0 + randomDragPlus;

            Vector3 randomForce = new Vector3(Random.Range(-maxRandomForce, maxRandomForce), Random.Range(-maxRandomForce, maxRandomForce), Random.Range(-maxRandomForce, maxRandomForce)).normalized;
            m_rigidbody.AddTorque(randomForce, ForceMode.Impulse);
            m_rigidbody.AddTorque(forceX, forceY, forceZ, ForceMode.Impulse);
            m_rigidbody.AddForceAtPosition(Vector3.up * maxRollingForce, new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)));

        }
        
        
    }
}