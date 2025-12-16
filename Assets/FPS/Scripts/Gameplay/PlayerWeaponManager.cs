using UnityEngine;
using Unity.FPS.Game;
using System.Collections.Generic;

namespace Unity.FPS.Gameplay
{
    public class PlayerWeaponManager : MonoBehaviour
    {
        #region Variables
        //무기 장착
        //처음 지급되는 무기(WeaponController가 붙어있는 프리팹) 리스트 - 인벤토리 개념
        public List<WeaponController> startingWeapons = new List<WeaponController>();

        //무기가 장착될 오브젝트
        public Transform weaponParentSocket;

        //플레이어가 게임 중에 들고다니는 무기 리스트 - 슬롯 개념
        private WeaponController[] weaponSlots = new WeaponController[9];

        #endregion

        #region Property
        //무기 슬롯(weaponSlots)을 관리하는 인덱스
        public int ActiveWeaponIndex {  get; private set; }
        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method

        #endregion
    }
}
