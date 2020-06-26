using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider))]
public class DrawGizmoBoxColliderExtents : MonoBehaviour
{
    BoxCollider m_BoxCollider;
    Transform m_Transform;

    private void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider>();
        m_Transform = transform;
    }

    // Draw lines around the cube with gizmos so that we can easily see its extents at all times.
    private void OnDrawGizmos()
    {
        if (m_BoxCollider == null)
            m_BoxCollider = GetComponent<BoxCollider>();

        if (m_Transform == null)
            m_Transform = transform;

        var CenterPosition = transform.position + m_BoxCollider.center;
        var XHalfSize = (transform.lossyScale.x * m_BoxCollider.size.x) / 2;
        var YHalfSize = (transform.lossyScale.y * m_BoxCollider.size.y) / 2;
        var ZHalfSize = (transform.lossyScale.z * m_BoxCollider.size.z) / 2;
        var Corner1 = CenterPosition + transform.rotation * (new Vector3(XHalfSize, YHalfSize, ZHalfSize));
        var Corner2 = CenterPosition + transform.rotation * (new Vector3(XHalfSize, YHalfSize, -ZHalfSize));
        var Corner3 = CenterPosition + transform.rotation * (new Vector3(XHalfSize, -YHalfSize, ZHalfSize));
        var Corner4 = CenterPosition + transform.rotation * (new Vector3(XHalfSize, -YHalfSize, -ZHalfSize));
        var Corner5 = CenterPosition + transform.rotation * (new Vector3(-XHalfSize, YHalfSize, ZHalfSize));
        var Corner6 = CenterPosition + transform.rotation * (new Vector3(-XHalfSize, YHalfSize, -ZHalfSize));
        var Corner7 = CenterPosition + transform.rotation * (new Vector3(-XHalfSize, -YHalfSize, ZHalfSize));
        var Corner8 = CenterPosition + transform.rotation * (new Vector3(-XHalfSize, -YHalfSize, -ZHalfSize));

        Gizmos.DrawLine(Corner1, Corner2);
        Gizmos.DrawLine(Corner2, Corner4);
        Gizmos.DrawLine(Corner4, Corner3);
        Gizmos.DrawLine(Corner3, Corner1);

        Gizmos.DrawLine(Corner5, Corner6);
        Gizmos.DrawLine(Corner6, Corner8);
        Gizmos.DrawLine(Corner8, Corner7);
        Gizmos.DrawLine(Corner7, Corner5);

        Gizmos.DrawLine(Corner1, Corner5);
        Gizmos.DrawLine(Corner2, Corner6);
        Gizmos.DrawLine(Corner3, Corner7);
        Gizmos.DrawLine(Corner4, Corner8);
    }
}
