using UnityEngine;

namespace MySample
{
    public class CameraController : MonoBehaviour
    {
        public Transform player;
        public Vector3 offset;

        private void LateUpdate()
        {
            this.transform.position = player.position + offset;
        }
    }
}
