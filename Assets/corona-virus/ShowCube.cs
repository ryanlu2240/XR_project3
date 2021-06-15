using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using UnityEngine;
using UnityEngine.Events;

public class ShowCube : MonoBehaviour
{
	[Header("References")]

	[SerializeField]
	private MeshRenderer mesh;
    public int show;

	[SerializeField]
	private SolverHandler solverHandler;

	private bool wasTracked = false;
	private OrbState state = OrbState.Idle;
	private IMixedRealityController trackedController;
	public bool IsTracking => solverHandler.TransformTarget != null;

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
    // Start is called before the first frame update
    void Start()
    {
        show=1;
    }

    // Update is called once per frame
    void Update()
    {
	    bool isTracked = IsTracking;
	    if (wasTracked != IsTracking)
	    {
	        trackedController = isTracked ? GetTrackedController(solverHandler.CurrentTrackedHandedness) : null;
	        wasTracked = isTracked;
	    }

	    if (isTracked)
	    {
	        // If we are now tracking a controller and we were idle, then switch to source tracking state
	        if (CurrentState == OrbState.Idle)
	        {
	            CurrentState = OrbState.SourceTracked;
	        }
	    }
	    else
	    {
	        CurrentState = OrbState.Idle;
	    }
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
    private void OrbStateUpdate()
    {
    	solverHandler.UpdateSolvers = CurrentState != OrbState.PhysicsTracked;
        if(show==1){
            mesh.enabled = CurrentState != OrbState.Idle;
        }
        else{
            mesh.enabled = false;
        }
    }

}
