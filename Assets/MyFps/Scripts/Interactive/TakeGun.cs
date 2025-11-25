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

        //탄환 UI
        public GameObject ammoCountUI;

        public WeaponType weaponType = WeaponType.Pistol;

        #endregion

        #region Custom Method


        protected override void DoAction()
        {
            //UI 감추기
            HideActionUI();

            //오브젝트 제거
            //this.gameObject.SetActive(false);
            theMarker.SetActive(false);

            //현재 소지무기 세팅
            PlayerStats.Instance.SetWeaponType(weaponType);

            //진짜 총 나옴
            realPistol.SetActive(true);

            //AmmoCountUI 나옴
            ammoCountUI.SetActive(true);

            //아이템 킬
            Destroy(gameObject);

        }

        #endregion
    }
}
