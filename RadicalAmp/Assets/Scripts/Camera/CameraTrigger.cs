using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    GameObject mainCamera;
    CameraDrive mainCameraScript;

    [SerializeField] float angleX;
    [SerializeField] float height;
    [SerializeField] float posZminus;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        mainCameraScript = GameObject.FindWithTag("MainCamera").GetComponent<CameraDrive>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            mainCamera.transform.eulerAngles = new Vector3(angleX,0,0);
            mainCameraScript.height = height;
            mainCameraScript.posZminus = posZminus;
        }
    }
}
