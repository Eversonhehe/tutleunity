using UnityEngine;

public class PlayerCollision : MonoBehaviour{
    void OnCollisionEnter (Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "obstacle")
        {
            Debug.Log("obstacle was hit");
        }
    }
}
