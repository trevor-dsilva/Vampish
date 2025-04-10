using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform player;
    public Transform playercamera;

    [Range(0.0f, 50.0f)] public float height = 45f;


    // Update is called once per frame
    void LateUpdate()
    {
        if (player!= null)
        {
            transform.position = new Vector3(player.position.x, player.position.y + height, player.position.z);
            transform.rotation = Quaternion.Euler(90f, playercamera.eulerAngles.y, 0f);
        }
    }
}
