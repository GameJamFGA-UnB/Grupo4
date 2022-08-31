using UnityEngine;
using Cinemachine;
 
/// <summary>
/// An add-on module for Cinemachine Virtual Camera that locks the camera's Z co-ordinate
/// </summary>
[ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
public class LockCameraAxis : CinemachineExtension
{
    [Tooltip("Lock the camera's X position to this value")]
    public bool lockX = false;
    public float m_XPosition = 10;

    [Tooltip("Lock the camera's Y position to this value")]
    public bool lockY = false;
    public float m_YPosition = 10;

    [Tooltip("Lock the camera's Z position to this value")]
    public bool lockZ = false;
    public float m_ZPosition = 10;
 
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            if(lockX) pos.x = m_XPosition;
            if(lockY) pos.y = m_YPosition;
            if(lockZ) pos.z = m_ZPosition;
            state.RawPosition = pos;
        }
    }
}