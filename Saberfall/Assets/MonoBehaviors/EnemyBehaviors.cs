using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Saberfall
{
    [RequireComponent(typeof(CharacterController2D))]
    [RequireComponent(typeof(Collider2D))]
    public class EnemyBehaviour : MonoBehaviour
    {
        static Collider2D[] s_ColliderCache = new Collider2D[16];

        public Vector3 moveVector { get { return m_MoveVector; } }
        public Transform Target { get { return m_Target; } }

        public bool spriteFaceLeft = false;

        [Header("Movement")]
        public float speed;
        public float gravity = 10.0f;

        [Header("Scanning settings")]
        [Range(0.0f, 360.0f)]
        public float viewDirection = 0.0f;
        [Range(0.0f, 360.0f)]
        public float viewFov;
        public float viewDistance;
        public float timeBeforeTargetLost = 3.0f;

        [Header("Melee Attack Data")]
        public float meleeRange = 3.0f;

        protected CharacterController2D m_CharacterController2D;
        protected Collider2D m_Collider;
        protected Vector3 m_MoveVector;
        protected Transform m_Target;
        protected Vector3 m_TargetShootPosition;
        protected float m_TimeSinceLastTargetView;

        protected Vector2 m_SpriteForward;
        protected ContactFilter2D m_Filter;

        protected bool m_Dead = false;

        private void Awake()
        {
            m_CharacterController2D = GetComponent<CharacterController2D>();
            m_Collider = GetComponent<Collider2D>();
            m_SpriteForward = spriteFaceLeft ? Vector2.left : Vector2.right;
        }

        private void OnEnable()
        {
            m_Dead = false;
            m_Collider.enabled = true;
        }

        void FixedUpdate()
        {
            if (m_Dead)
                return;

            m_MoveVector.y = Mathf.Max(m_MoveVector.y - gravity * Time.deltaTime, -gravity);

            //m_CharacterController2D.Move(m_MoveVector * Time.deltaTime);
            //m_CharacterController2D.CheckCapsuleEndCollisions();
            UpdateTimers();
        }

        void UpdateTimers()
        {
            if (m_TimeSinceLastTargetView > 0.0f)
                m_TimeSinceLastTargetView -= Time.deltaTime;
        }

        public void SetMoveVector(Vector2 newMoveVector)
        {
            m_MoveVector = newMoveVector;
        }

        public void UpdateFacing()
        {
            bool faceLeft = m_MoveVector.x < 0f;
            bool faceRight = m_MoveVector.x > 0f;

            if (faceLeft)
            {
                m_SpriteForward = spriteFaceLeft ? Vector2.right : Vector2.left;
            }
            else if (faceRight)
            {
                m_SpriteForward = spriteFaceLeft ? Vector2.left : Vector2.right;
            }
        }

        //public void ScanForPlayer()
        //{
        //    //Vector3 dir = PlayerCharacter.PlayerInstance.transform.position - transform.position;

        //    if (dir.sqrMagnitude > viewDistance * viewDistance)
        //    {
        //        return;
        //    }

        //    Vector3 testForward = Quaternion.Euler(0, 0, spriteFaceLeft ? Mathf.Sign(m_SpriteForward.x) * -viewDirection : Mathf.Sign(m_SpriteForward.x) * viewDirection) * m_SpriteForward;
        //    float angle = Vector3.Angle(testForward, dir);

        //    if (angle > viewFov * 0.5f)
        //    {
        //        return;
        //    }

        //    //m_Target = PlayerCharacter.PlayerInstance.transform;
        //    m_TimeSinceLastTargetView = timeBeforeTargetLost;
        //}

        public void CheckTargetStillVisible()
        {
            if (m_Target == null)
                return;

            Vector3 toTarget = m_Target.position - transform.position;

            if (toTarget.sqrMagnitude < viewDistance * viewDistance)
            {
                Vector3 testForward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirection : viewDirection) * m_SpriteForward;

                float angle = Vector3.Angle(testForward, toTarget);

                if (angle <= viewFov * 0.5f)
                {
                    m_TimeSinceLastTargetView = timeBeforeTargetLost;
                }
            }

            if (m_TimeSinceLastTargetView <= 0.0f)
            {
                m_Target = null;
            }
        }



    }
}