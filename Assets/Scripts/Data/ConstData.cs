using UnityEngine;

namespace Platformer
{
    [CreateAssetMenu(menuName = "Platformer/ConstData", fileName = "ConstData")]
    public class ConstData : ScriptableObject
    {
        public float playerJumpForce;
        public float playerSpeed;
        public float cameraFollowSmoothness;
        public float speedBuffDuration;
        public float jumpBuffDuration;
    }
}