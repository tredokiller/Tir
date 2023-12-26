using UnityEngine;

namespace Dynamic_Floating_Text.Scripts
{
    public class DynamicTextManager : MonoBehaviour
    {

        public static DynamicTextData defaultData;
        public static GameObject canvasPrefab;
        public static Transform mainCamera;

        public static float distanceFromCameraToSpawn;

        [SerializeField] private DynamicTextData _defaultData;
        [SerializeField] private GameObject _canvasPrefab;
        [SerializeField] private Transform _mainCamera;

        [SerializeField] private float _distanceFromCameraToSpawn;
        
        private void Awake()
        {
            defaultData = _defaultData;
            mainCamera = _mainCamera;
            canvasPrefab = _canvasPrefab;
            distanceFromCameraToSpawn = _distanceFromCameraToSpawn;
        }

        public static void CreateText2D(Vector2 position, string text, DynamicTextData data)
        {
            GameObject newText = Instantiate(canvasPrefab, position, Quaternion.identity);
            newText.transform.GetComponent<DynamicText2D>().Initialise(text, data);
        }

        public static void CreateText(Vector3 position, string text, DynamicTextData data = null)
        {
            GameObject newText = Instantiate(canvasPrefab, position, Quaternion.identity);
            
            float distance = Vector3.Distance(newText.transform.position, mainCamera.transform.position);

            if (data == null)
            {
                newText.transform.GetComponent<DynamicText>().Initialise(text, defaultData); 
            }
            else
            {
                newText.transform.GetComponent<DynamicText>().Initialise(text, data);
            }
        }

    }
}
