using UnityEngine;

namespace MyFps
{
    //손에 든 무기 타입 enum 정의
    public enum WeaponType
    {
        None = 0,   //무기가 없을때
        Pistol,
        Healmatic,
    }

    /// <summary>
    /// 플레이어의 데이터를 관리하는 싱글톤 클래스
    /// 모든 씬에서 계속 데이터를 유지 관리
    /// </summary>
    public class PlayerStats : PersistantSingleton<PlayerStats>
    {
        #region Variables
        //탄환 갯수
        private int ammoCount;

        //소지 무기 타입
        private WeaponType weaponType;
        #endregion

        #region Property
        public int AmmoCount { get { return ammoCount; } }
        public WeaponType WeaponType { get { return weaponType; } }
        #endregion

        #region Unity Event Method
        protected override void Awake()
        {
            base.Awake();

            //플레이어 데이터 초기화
            ammoCount = 0;
            weaponType = WeaponType.None;
        }
        #endregion

        #region Custom Method
        // ammo 추가하기
        public void AddAmmo(int amount)
        {
            ammoCount += amount;
            Debug.Log($"ammoCont: {ammoCount}");
        }

        // ammo 사용하기
        public bool UseAmmo(int amount = 1)
        {
            if (ammoCount < amount)
            {
                Debug.Log("You need to reload");
                return false;
            }

            ammoCount -= amount;
            Debug.Log($"ammoCont: {ammoCount}");
            return true;
        }

        //무기 획득(교체)
        public void SetWeaponType(WeaponType type)
        {
            weaponType = type;
        }
        #endregion

    }
}
