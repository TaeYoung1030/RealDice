using UnityEngine;

public class EquipCamera : MonoBehaviour
{
    [SerializeField] Transform camDirection;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            TakePhoto();
        }
    }

    void TakePhoto()
    {
        Debug.Log("카메라 클릭");
        RaycastHit hit;
        if(Physics.Raycast(camDirection.position,camDirection.forward, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }
}
