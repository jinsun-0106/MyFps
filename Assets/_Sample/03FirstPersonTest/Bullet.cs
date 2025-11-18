using UnityEngine;
using UnityEngine.Windows;

namespace MySample
{
    /// <summary>
    /// 총알을 관리하는 클래스,
    /// 리지디바디에 의해 움직임 구현
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        #region Variables
        //참조
        private Rigidbody rb;

        //이동
        [SerializeField]
        private float moveForce = 50f;
        #endregion

        #region Unity Event Method
        
        #endregion

        #region Custom Method
        public void MoveForce()
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }
            rb.AddForce(transform.forward * moveForce, ForceMode.Impulse);
        }
        #endregion
    }
}
