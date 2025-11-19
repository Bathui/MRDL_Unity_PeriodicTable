using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

namespace MRDL_PeriodicTable
{
    /// <summary>
    /// Configures the GameObject to behave as a Head-Up Display (HUD) that stays in front of the user.
    /// This script automatically adds and configures the necessary MRTK Solver components.
    /// </summary>
    [RequireComponent(typeof(SolverHandler))]
    [RequireComponent(typeof(RadialView))]
    public class HeadUpDisplay : MonoBehaviour
    {
        [Tooltip("Minimum distance from the head (meters).")]
        [SerializeField]
        private float minDistance = 0.5f;

        [Tooltip("Maximum distance from the head (meters).")]
        [SerializeField]
        private float maxDistance = 0.8f;

        [Tooltip("How much the element is allowed to drift from the center of view (degrees).")]
        [SerializeField]
        private float maxViewDegrees = 15f;

        [Tooltip("Time to move to the target position (seconds). Lower is faster.")]
        [SerializeField]
        private float moveLerpTime = 0.1f;

        [Tooltip("Time to rotate to the target rotation (seconds). Lower is faster.")]
        [SerializeField]
        private float rotateLerpTime = 0.1f;

        private void Start()
        {
            ConfigureSolvers();
        }

        private void OnValidate()
        {
            // Update configurations in Editor when values change
            if (Application.isEditor)
            {
                ConfigureSolvers();
            }
        }

        private void ConfigureSolvers()
        {
            // Configure SolverHandler
            SolverHandler solverHandler = GetComponent<SolverHandler>();
            if (solverHandler != null)
            {
                solverHandler.TrackedTargetType = TrackedObjectType.Head;
            }

            // Configure RadialView
            RadialView radialView = GetComponent<RadialView>();
            if (radialView != null)
            {
                radialView.ReferenceDirection = RadialViewReferenceDirection.FacingWorldUp;
                
                radialView.MinDistance = minDistance;
                radialView.MaxDistance = maxDistance;
                
                radialView.MinViewDegrees = 0f; // Always keep it potentially in center
                radialView.MaxViewDegrees = maxViewDegrees;
                
                radialView.MoveLerpTime = moveLerpTime;
                radialView.RotateLerpTime = rotateLerpTime;
                
                // Ensure smoothing is on
                radialView.Smoothing = true;
            }
        }
    }
}

