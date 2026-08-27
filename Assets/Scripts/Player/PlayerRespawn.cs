using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    public void Respawn()
    {
        CharacterController controller =  GetComponent<CharacterController>();

        if(controller != null) controller.enabled = false;

        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        if(controller != null) controller.enabled = true;
       
    }
    
}
