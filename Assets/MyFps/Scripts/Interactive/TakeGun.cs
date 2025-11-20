using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyFps
{
    /// <summary>
    /// 피스톨 아이템 획득하기
    /// </summary>
    public class TakeGun : Interactive
    {
        #region Variables
        
        [Header("Interactive Action")]
        //총
        public GameObject realPistol;
        //화살표
        public GameObject theMarker;

        #endregion

        #region Custom Method


        protected override void DoAction()
        {
            //UI 감추기
            HideActionUI();

            //오브젝트 제거
            this.gameObject.SetActive(false);
            theMarker.SetActive(false);

            //진짜 총 나옴
            realPistol.SetActive(true);

        }

        #endregion
    }
}
