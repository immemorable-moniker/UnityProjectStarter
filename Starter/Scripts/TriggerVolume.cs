using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// When the player enters this trigger, we will fire an event.
/// </summary>
public class TriggerVolume : MonoBehaviour
{
    public bool isOneShot = true;
    public UnityEvent triggerEntered;
    bool m_Fired = false;

    private void Start()
    {
        Renderer[] Renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < Renderers.Length; i++)
            Renderers[i].enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool ShouldFire = (!isOneShot || (isOneShot && !m_Fired));
        if (ShouldFire && other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            triggerEntered?.Invoke();
            Destroy(gameObject);
            m_Fired = true;
        }
    }
}
