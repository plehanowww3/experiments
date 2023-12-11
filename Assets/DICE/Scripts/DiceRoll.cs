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
        [SerializeField] private BoxCollider m_collider;
        
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
        
        private int RandomSign()
        {
            return Random.Range(0, 2) * 2 - 1; // Возвращает 1 или -1
        }
        
        private void RollDice()
        {
            m_collider.isTrigger = true;
            
            forceX = Random.Range(-maxRandomForce, maxRandomForce) * 10;
            forceY = Random.Range(-maxRandomForce, maxRandomForce) * 10;
            forceZ = Random.Range(-maxRandomForce, maxRandomForce) * 10;
            
            switch (Random.Range(0, 3))
            {
                case 0:
                    forceX = maxRandomForce * RandomSign();
                    break;
                case 1: 
                    forceY = maxRandomForce * RandomSign();
                    break;
                case 2: 
                    forceZ = maxRandomForce * RandomSign();
                    break;
            }
            
            m_rigidbody.AddTorque(forceX, forceY, forceZ, ForceMode.Impulse);
            
            // Случайная точка приложения силы вблизи центра кубика
            Vector3 forcePoint = transform.position + Random.insideUnitSphere * 1f;

            m_rigidbody.AddForceAtPosition(Vector3.up * maxRollingForce, forcePoint, ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            m_collider.isTrigger = false;
        }
    }
}