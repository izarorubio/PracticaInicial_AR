using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using UnityEngine.UI;

namespace UnityEngine.XR.ARFoundation.Samples
{
    [RequireComponent(typeof(ARRaycastManager))]
    public class PlaceOnPlane : PressInputBase
    {
        [SerializeField]
        GameObject bluePrefab;

        [SerializeField]
        GameObject redPrefab;

        [SerializeField]
        TMP_Text planeCountText;

        [SerializeField]
        TMP_Dropdown prefabSelector;

        [SerializeField]
        Button clearPrefabsButton;

        ARRaycastManager m_RaycastManager;
        List<GameObject> spawnedObjects = new List<GameObject>();
        bool m_Pressed;

        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

        protected override void Awake()
        {
            base.Awake();
            m_RaycastManager = GetComponent<ARRaycastManager>();
            clearPrefabsButton.onClick.AddListener(ClearPrefabs);
        }

        void Update()
        {
            // Update plane count
            var planeCount = FindObjectsOfType<ARPlane>().Length;
            planeCountText.text = $"Planes detectados: {planeCount}";

            if (Pointer.current == null || !m_Pressed)
                return;

            var touchPosition = Pointer.current.position.ReadValue();

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = s_Hits[0].pose;
                GameObject selectedPrefab = prefabSelector.value == 0 ? bluePrefab : redPrefab;
                var spawnedObject = Instantiate(selectedPrefab, hitPose.position, hitPose.rotation);
                spawnedObjects.Add(spawnedObject);
            }
        }

        protected override void OnPress(Vector3 position) => m_Pressed = true;
        protected override void OnPressCancel() => m_Pressed = false;

        void ClearPrefabs()
        {
            foreach (var obj in spawnedObjects)
            {
                Destroy(obj);
            }
            spawnedObjects.Clear();
        }
    }
}
