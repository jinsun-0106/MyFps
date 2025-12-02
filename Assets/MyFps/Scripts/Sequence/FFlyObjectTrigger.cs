using UnityEngine;
using System.Collections;

namespace MyFps
{
    /// <summary>
    /// 트리거에 걸리면 액티브 오브젝트를 이용하여 컵을 날린다
    /// </summary>
    public class FFlyObjectTrigger : MonoBehaviour
    {
        #region Variables
        //참조: 충돌체
        private BoxCollider boxCollider;

        //액티브 오브젝트
        public GameObject activateObject;

        public GameObject thePlayer;

        #endregion

        #region Unity Event Method
        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(SequencePlay());

            //충돌체 비활성화
            boxCollider.enabled = false;
        }
        #endregion

        #region Custom Method
        IEnumerator SequencePlay()
        {
            thePlayer.SetActive(false);

            activateObject.SetActive(true);

            yield return new WaitForSeconds(1f);

            thePlayer.SetActive(true);
            activateObject.SetActive(false);
        }
        #endregion
    }
}
