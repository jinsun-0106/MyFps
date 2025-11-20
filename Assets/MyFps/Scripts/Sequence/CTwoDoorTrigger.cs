using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 
    /// </summary>
    public class CTwoDoorTrigger : MonoBehaviour
    {
        #region Variables
        //충돌체
        private BoxCollider collider;

        //시퀀스
        public Door door;

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            collider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            SequencePlay();

            //충돌체 비활성화(또는 킬)
            collider.enabled = false;
        }
        #endregion

        #region Custom Method
        private void SequencePlay()
        {
            door.Activate();
        }
        #endregion
    }
}
