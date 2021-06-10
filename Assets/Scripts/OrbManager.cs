using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using UnityEngine;
using UnityEngine.Events;

namespace XR.Break
{
    /// <summary>
    /// This class manages the state for the game orb entity and orchestrates the manipulation of related references
    /// in response to state and input changes.
    /// </summary>
    public class OrbManager : MonoBehaviour, IMixedRealityInputHandler
    {
        [Header("References")]

        [SerializeField]
        private Animator animator;
        public ParticleSystem gas;
        public GameObject barCtrl=null;
        public GameObject can;

        [SerializeField]
        private Rigidbody rigidBody;

        [SerializeField]
        private MeshRenderer mesh;

        [SerializeField]
        private SolverHandler solverHandler;

    	public float sprayRange = 5f;


        [Header("Power Up")]
        [SerializeField]
        private ParabolaPhysicalLineDataProvider parabolicLineData;

        [SerializeField]
        private float PowerUpMax = 1.15f;

        [SerializeField]
        private float PowerUpForceMultiplier = 3.0f;

        [Header("Events")]
        public UnityEvent OnFire = new UnityEvent();
        public UnityEvent OnRetrieve = new UnityEvent();

        public bool IsTracking => solverHandler.TransformTarget != null;

        private bool IsPoweringUp
        {
            get => poweringUp;
            set
            {
                if (poweringUp != value)
                {
                    poweringUp = value;
                    PowerUpUpdate();
                }
            }
        }

        private enum OrbState
        {
            Idle,
            SourceTracked,
            PhysicsTracked,
        };

        private OrbState CurrentState
        {
            get => state;
            set
            {
                if (state != value)
                {
                    state = value;
                    OrbStateUpdate();
                }
            }
        }
        private float PowerUpForce => Mathf.Clamp(powerUpTimer, 0.0f, PowerUpMax) * PowerUpForceMultiplier;
        private float PowerUp01 => Mathf.Clamp01(powerUpTimer / PowerUpMax);

        private Vector3 TrackedPointerDirection =>
            trackedLinePointer != null ? trackedLinePointer.Rotation * Vector3.forward : Vector3.zero;

        private OrbState state = OrbState.Idle;
        private bool wasTracked = false;
        private IMixedRealityController trackedController;
        private IMixedRealityPointer trackedLinePointer;

        private float powerUpTimer;

        private bool poweringUp = false;

        private void Awake()
        {
            Debug.Assert(animator != null);
            Debug.Assert(rigidBody != null);
            Debug.Assert(solverHandler != null);
            Debug.Assert(mesh != null);

            // Default initialize values
            PowerUpUpdate();
            OrbStateUpdate();
        }

        private void PowerUpUpdate()
        {
            powerUpTimer = 0.0f;

            animator.SetBool("PowerUp", poweringUp);
            animator.SetFloat("PowerUpSpeed", 1.0f / PowerUpMax);

            parabolicLineData.gameObject.SetActive(poweringUp);
        }

        private void OrbStateUpdate()
        {
            IsPoweringUp = false;

            solverHandler.UpdateSolvers = CurrentState != OrbState.PhysicsTracked;

            mesh.enabled = CurrentState != OrbState.Idle;

            rigidBody.isKinematic = CurrentState != OrbState.PhysicsTracked;
        }

        private void OnEnable()
        {
            CoreServices.InputSystem?.RegisterHandler<IMixedRealityInputHandler>(this);
        }

        private void OnDisable()
        {
            CoreServices.InputSystem?.UnregisterHandler<IMixedRealityInputHandler>(this);
        }

        private void Update()
        {
            bool isTracked = IsTracking;
            if (wasTracked != IsTracking)
            {
                trackedController = isTracked ? GetTrackedController(solverHandler.CurrentTrackedHandedness) : null;
                trackedLinePointer = isTracked ? GetLinePointer(trackedController) : null;
                wasTracked = isTracked;
            }

            can.active = isTracked;
            if (isTracked)
            {
                // If we are now tracking a controller and we were idle, then switch to source tracking state
                if (CurrentState == OrbState.Idle)
                {
                    CurrentState = OrbState.SourceTracked;
                }
                else if (CurrentState == OrbState.SourceTracked && IsPoweringUp)
                {
                    powerUpTimer += Time.deltaTime;
                    UpdatePowerUpVisuals();
                }
            }
            else
            {
                CurrentState = OrbState.Idle;
            }
        }

        private void UpdatePowerUpVisuals()
        {
            parabolicLineData.LineTransform.rotation = Quaternion.identity;
            parabolicLineData.Direction = TrackedPointerDirection;

            // Increase the modifier multipliers the higher we point.
            //float velocity = Mathf.Lerp(minParabolaVelocity, maxParabolaVelocity, PowerUp01);
            float velocity = PowerUpForce;
            //float distance = Mathf.Lerp(minDistanceModifier, maxDistanceModifier, PowerUp01);

            parabolicLineData.Velocity = velocity;
            //parabolicLineData.DistanceMultiplier = distance;
        }

        private void Fire()
        {
            if (trackedLinePointer != null)
            {
                Debug.Log("Fire");
                // var forceVec = TrackedPointerDirection * PowerUpForce;
                //
                // CurrentState = OrbState.PhysicsTracked;
                //
                // rigidBody.AddForce(forceVec, ForceMode.Impulse);

                OnFire?.Invoke();
                gas.Play();
                if (barCtrl != null)
                {
                    barCtrl.GetComponent<bar>().DecreaseCleaner();
                }

                GameObject[] virus = GameObject.FindGameObjectsWithTag("Virus");
                float shortestDistance = Mathf.Infinity;
                GameObject nearestVirus = null;
                foreach (GameObject v in virus)
                {
                    float distanceToVirus = Vector3.Distance(transform.position, v.transform.position);
                    if (distanceToVirus < shortestDistance)
                    {
                        shortestDistance = distanceToVirus;
                        nearestVirus = v;
                    }
                }

                Debug.Log(shortestDistance);
                if (nearestVirus != null && shortestDistance <= sprayRange)
        		{
        			Destroy(nearestVirus);
        		}
            }
        }

        #region IMixedRealityInputHandler implementation

        public void OnInputUp(InputEventData eventData)
        {
            if (IsTrackingSource(eventData.SourceId)
                && eventData.MixedRealityInputAction.Description == "Select")
            {

                if(barCtrl.GetComponent<bar>().currentCleaner > 0)
                {
                    Fire();
                    CurrentState = OrbState.SourceTracked;
                    OnRetrieve?.Invoke();
                }

                // if (CurrentState == OrbState.SourceTracked)
                // {
                //     Fire();
                // }
                // else
                // {
                //     CurrentState = OrbState.SourceTracked;
                //     OnRetrieve?.Invoke();
                // }
            }
        }

        public void OnInputDown(InputEventData eventData)
        {
            // if (IsTrackingSource(eventData.SourceId)
            //     && eventData.MixedRealityInputAction.Description == "Select")
            // {
            //     if (CurrentState == OrbState.SourceTracked)
            //     {
            //         IsPoweringUp = true;
            //         powerUpTimer = 0.0f;
            //     }
            //     // if sourceTracked state, then power up
            // }
        }

        #endregion

        private bool IsTrackingSource(uint sourceId)
        {
            return trackedController?.InputSource.SourceId == sourceId;
        }

        private static IMixedRealityController GetTrackedController(Handedness handedness)
        {
            foreach (IMixedRealityController c in CoreServices.InputSystem?.DetectedControllers)
            {
                if (c.ControllerHandedness.IsMatch(handedness))
                {
                    return c;
                }
            }

            return null;
        }

        private static IMixedRealityPointer GetLinePointer(IMixedRealityController controller)
        {
            foreach (var pointer in controller?.InputSource?.Pointers)
            {
                if (pointer is LinePointer linePointer)
                {
                    return linePointer;
                }
            }

            return null;
        }
    }
}
