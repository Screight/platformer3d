using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ProjectD.BackInTime
{
    public struct Target
    {
        public Transform transform;
        public Rigidbody rigidbody;
        public AnimatorHandler animtorHandler;

        public Target(GameObject p_targetGameObject)
        {
            transform = p_targetGameObject.transform;
            rigidbody = p_targetGameObject.GetComponent<Rigidbody>();
            animtorHandler = p_targetGameObject.GetComponentInChildren<AnimatorHandler>();
        }
    }

    public class StateController : MonoBehaviour
    {
        List<State> m_states;

        const int MAX_NUMBER_OF_STATES = 500;

        bool m_isRecording;
        bool m_undoMovement;

        [SerializeField] GameObject m_target;
        Target m_targetInfo;

        private void Awake()
        {
            m_states = new List<State>();
        }

        private void Start()
        {
            m_targetInfo = new Target(m_target);
        }

        private void Update()
        {
            if (m_isRecording) {
                AddState(m_targetInfo);
            }
            else if (m_undoMovement)
            {
                if(m_states.Count == 0)
                {
                    m_undoMovement = false;
                    m_targetInfo.rigidbody.velocity = Vector3.zero;
                    Debug.Log("No more states left. End of 'back in time'.");
                    return;
                }
                State newState = GetState();
                newState.ApplyStateTo(m_targetInfo);
            }
        }

        public bool AddState(Target p_target) {
            if(m_states.Count == MAX_NUMBER_OF_STATES) {
                m_states.RemoveAt(0);
            }
            m_states.Add(new State(p_target));
            return true;
        }

        public State GetState() {
            int index = m_states.Count - 1;
            try
            {
                State state = m_states[index];
                m_states.RemoveAt(index);
                return state;
            }
            catch (ArgumentOutOfRangeException e)
            {
                return null;
            }
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Start Recording"))
            {
                StartRecording();
            }
            else if (GUILayout.Button("Start 'Back in Time'"))
            {
                StartBackInTime();
            }
            else if (GUILayout.Button("Stop 'Back in Time'"))
            {
                StopBackInTime();
            }
        }

        public void StartRecording() {
            m_isRecording = true;
            Debug.Log("Start recording movements");
        }

        public void StopRecording()
        {
            m_isRecording = false;
            Debug.Log("Stop recording movements");
        }

        public void StartBackInTime()
        {
            m_isRecording = false;
            m_undoMovement = true;
            Debug.Log("Start 'Back in Time'");
        }

        public void StopBackInTime()
        {
            m_undoMovement = false;
            Debug.Log("Stop 'Back in Time'");
        }

        public void UndoMovement()
        {

        }

    }
}
