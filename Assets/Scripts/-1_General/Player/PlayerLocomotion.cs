using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectD
{
    public class PlayerLocomotion : MonoBehaviour
    {
        InputHandler m_inputHandler;
        AnimatorHandler m_animatorHandler;

        Transform m_cameraObject;
        Vector3 m_moveDirection;
        Transform m_myTransform;

        GameObject m_normalCamera;

        // WHY?
            // For some reason if i try to get the reference from script it does not get it
        [SerializeField] Rigidbody m_rigidbody;

        [Header("Stats")]
        [SerializeField] float m_movementSpeed = 5;
        [SerializeField] float m_sprintSpeed = 7;
        [SerializeField] float m_rotationSpeed = 10;
        [SerializeField] float m_fallSpeed = 45;

        bool m_isSprinting;
        #region Movement Flags
        bool m_rollFlag;
        bool m_sprintFlag;
        bool m_isInAir;
        bool m_isGrounded;
        #endregion

        [Header("Ground & Air Detection")]
        [SerializeField] float m_groundDetectionRayStart = 0.5f;
        [SerializeField] float m_minimumDistanceNeededToBeginFall = 1.0f;
        [SerializeField] float m_groundDirectionRayDistance = 0.2f;
        [SerializeField] LayerMask m_ignoreForGroundCheck;
        public float m_inAirTimer;

        #region Events
        UnityEvent m_sprintStart;
        UnityEvent m_sprintEnd;
        public void AddListenerToSprintStart(UnityAction p_function) { m_sprintStart.AddListener(p_function); }
        public void AddListenerToSprintEnd(UnityAction p_function) { m_sprintEnd.AddListener(p_function); }
        #endregion

        private void Awake()
        {
            m_sprintStart = new UnityEvent();
            m_sprintEnd = new UnityEvent();
        }

        private void Start()
        {
            m_rigidbody = m_rigidbody.GetComponent<Rigidbody>();
            m_inputHandler = GetComponent<InputHandler>();
            m_animatorHandler = GetComponentInChildren<AnimatorHandler>();
            m_cameraObject = Camera.main.transform;
            m_myTransform = transform;
            m_animatorHandler.Initialize();

            m_isGrounded = true;
            //m_isInAir = true;
        }

        public void Update()
        {
            float delta = Time.deltaTime;

            HandleMovement(delta);
            HandleRolling(delta);

            if (m_isInAir)
            {
                m_inAirTimer += delta;
            }
            HandleFalling(delta);
        }

        #region Movement

        Vector3 m_normalVector;
        Vector3 m_targetPosition;

        private void HandleRotation(float p_delta)
        {
            Vector3 targetDir = Vector3.zero;
            float moveOverride = m_inputHandler.MoveAmount;

            targetDir = m_cameraObject.forward * m_inputHandler.VerticalInput;
            targetDir += m_cameraObject.right * m_inputHandler.HorizontalInput;

            targetDir.Normalize();
            targetDir.y = 0;
            if(targetDir == Vector3.zero) { targetDir = m_myTransform.forward; }

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(m_myTransform.rotation, tr, m_rotationSpeed * p_delta);
            m_myTransform.rotation = targetRotation;
        }

        private void HandleMovement(float p_delta)
        {
            if (m_animatorHandler.IsInteracting) { return; }

            m_moveDirection = m_cameraObject.forward * m_inputHandler.VerticalInput;
            m_moveDirection += m_cameraObject.right * m_inputHandler.HorizontalInput;
            m_moveDirection.Normalize();
            m_moveDirection.y = 0;

            float speed = 0;
            if (m_sprintFlag)
            {
                speed = m_sprintSpeed;
                m_isSprinting = true;
                m_sprintFlag = false;
            }
            else {
                if(m_isSprinting && !m_inputHandler.SprintInput)
                {
                    m_isSprinting = false;
                }
                speed = m_movementSpeed;
            }

            Vector3 projectedVelocityUnitary = Vector3.ProjectOnPlane(m_moveDirection, m_normalVector);
            m_rigidbody.velocity = speed * projectedVelocityUnitary;

            m_animatorHandler.UpdateAnimatorValues(m_inputHandler.MoveAmount, 0, m_isSprinting);
            if (m_animatorHandler.CanRotate)
            {
                HandleRotation(p_delta);
            }

        }

        private void HandleRolling(float p_delta)
        {
            if (m_animatorHandler.IsInteracting) { return; }

            if (m_rollFlag)
            {
                m_moveDirection = m_cameraObject.forward * m_inputHandler.VerticalInput;
                m_moveDirection += m_cameraObject.right * m_inputHandler.HorizontalInput;

                m_animatorHandler.PlayTargetAnimation(ANIMATIONS.ROLLING, true);
                m_moveDirection.y = 0;
                // rotate into the direction you are rolling
                if(m_moveDirection == Vector3.zero) { return; }
                Quaternion rollRotation = Quaternion.LookRotation(m_moveDirection);
                m_myTransform.rotation = rollRotation;
            }
        }

        private void HandleFalling(float p_delta)
        {
            m_isGrounded = false;
            Vector3 origin = m_myTransform.position;
            origin.y += m_groundDetectionRayStart;
            RaycastHit hit;
            if (Physics.Raycast(origin, m_myTransform.forward, out hit, 0.4f))
            {
                m_moveDirection = Vector3.zero;
                return;
            }
            if (m_isInAir)
            {
                m_rigidbody.AddForce(-Vector3.up * m_fallSpeed);
                m_rigidbody.AddForce(m_moveDirection * m_fallSpeed / 5f);
            }

            Vector3 dir = m_moveDirection.normalized;
            origin += dir * m_groundDirectionRayDistance;
            m_targetPosition = m_myTransform.position;

            Debug.DrawLine(origin, origin - m_minimumDistanceNeededToBeginFall * Vector3.up, Color.red);
            if(Physics.Raycast(origin, -Vector3.up, out hit, m_minimumDistanceNeededToBeginFall, ~m_ignoreForGroundCheck))
            {
                Debug.Log("Grounded");
                m_normalVector = hit.normal;
                Vector3 tp = hit.point;
                m_isGrounded = true;
                m_targetPosition.y = tp.y;


                if (m_isInAir)
                {
                    if (m_inAirTimer > 0.5f)
                    {
                        Debug.Log("You were in the air for " + m_inAirTimer);
                        m_animatorHandler.PlayTargetAnimation(ANIMATIONS.LAND, true);
                    }
                    else
                    {
                        m_animatorHandler.PlayTargetAnimation(ANIMATIONS.LOCOMOTION, true);
                        m_inAirTimer = 0;
                    }
                }
                
                m_isInAir = false;
                m_isGrounded = true;
                m_moveDirection.y = 0;
                m_inAirTimer = 0;
            }
            else
            {
                Debug.Log("Air borne");
                if (m_isGrounded)
                {
                    m_isGrounded = false;
                    if (!m_isInAir)
                    {
                        if (!m_animatorHandler.IsInteracting)
                        {
                            m_animatorHandler.PlayTargetAnimation(ANIMATIONS.FALLING, true);
                        }
                        Vector3 vel = m_rigidbody.velocity;
                        vel.Normalize();
                        m_rigidbody.velocity = vel * m_movementSpeed * 0.5f;
                        m_isInAir = true;
                    }
                }
                else { m_isInAir = true; }

                if (m_isGrounded)
                {
                    if(m_animatorHandler.IsInteracting || m_inputHandler.MoveAmount > 0)
                    {
                        m_myTransform.position = Vector3.Lerp(m_myTransform.position, m_targetPosition, p_delta);
                    }
                    else
                    {
                        m_myTransform.position = m_targetPosition;
                    }
                }

            }
        }

        #endregion

        #region Accesors
        public Rigidbody Rigidbody { get { return m_rigidbody; } }
        public bool RollFlag {
            get { return m_rollFlag; }
            set { m_rollFlag = value; }
        }
        public bool SprintFlag {
            get { return m_sprintFlag; }
            set {
                m_sprintFlag = value;
            }
        }
        #endregion

    }
}
