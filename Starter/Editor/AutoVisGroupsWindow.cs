using UnityEngine;
using UnityEditor;

/// <summary>
/// Naively apply rules to all GameObjects to toggle visibility of groups.
/// </summary>
public class AutoVisGroupsWindow : EditorWindow
{
    internal const string k_WindowTitle = "Auto Vis Groups";
    bool m_ShowTriggerVolumes = true;
    bool m_ShowPickups = true;
    bool m_ShowEnemy = true;

    GameObject[] m_AllObjects = null;

    /// <summary>
    /// Create a window if necessary, or focus a currently open window.
    /// The MenuItem attribute inserts this as a function in the editor menu bars.
    /// </summary>
    [MenuItem("Tools/" + k_WindowTitle)]
    public static void ShowWindow()
    {
        GetWindow<AutoVisGroupsWindow>(k_WindowTitle);
    }

    /// <summary>
    /// Specify what is drawn inside the editor window
    /// </summary>
    void OnGUI()
    {
        m_AllObjects = null;

        bool newShowTriggerVolumes = GUILayout.Toggle(m_ShowTriggerVolumes, "Trigger Volumes");
        bool newShowPickups = GUILayout.Toggle(m_ShowPickups, "Pickups");
        bool newShowEnemy = GUILayout.Toggle(m_ShowEnemy, "Enemies");

        // Has a toggle changed?
        if (newShowTriggerVolumes != m_ShowTriggerVolumes ||
            newShowPickups != m_ShowPickups ||
            newShowEnemy != m_ShowEnemy)
        {
            RefreshGameObjectArray();

            // Iterate all GameObjects.
            for (int i = 0; i < m_AllObjects.Length; i++)
            {
                if (!m_AllObjects[i].activeInHierarchy)
                    continue;

                // Apply rule to find trigger volumes
                if ((newShowTriggerVolumes != m_ShowTriggerVolumes) &&
                    (m_AllObjects[i].GetComponent<TriggerVolume>() != null)
                    )
                {
                    Collider[] colliders = m_AllObjects[i].GetComponentsInChildren<Collider>();

                    for (int j = 0; j < colliders.Length; j++)
                        SetVisibility(colliders[j].gameObject, newShowTriggerVolumes);
                }

                // Apply rule to find Pickups
                if (newShowPickups != m_ShowPickups &&
                    m_AllObjects[i].tag == "Pickup"
                    )
                {
                    SetVisibility(m_AllObjects[i], newShowPickups);
                }

                // Apply rule to find Enemies
                if (newShowEnemy != m_ShowEnemy &&
                    m_AllObjects[i].tag == "Enemy"
                    )
                {
                    SetVisibility(m_AllObjects[i], newShowEnemy);
                }
            }
        }

        // Update checkbox values to new values.
        m_ShowTriggerVolumes = newShowTriggerVolumes;
        m_ShowPickups = newShowPickups;
        m_ShowEnemy = newShowEnemy;
    }

    void RefreshGameObjectArray()
    {
        m_AllObjects = FindObjectsOfType<GameObject>();
    }

    void SetVisibility(GameObject go, bool visibility)
    {
        if (visibility)
            SceneVisibilityManager.instance.Show(go, false);
        else
            SceneVisibilityManager.instance.Hide(go, false);
    }
}
