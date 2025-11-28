using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 아이템 줍기: 탄환 10개 지급
    /// </summary>
    public class PickupAmmo : Pickup
    {
        #region Variables
        //탄환 지급 갯수
        [SerializeField]
        private int giveAmmo = 10;
        #endregion

        protected override bool OnPickup()
        {
            PlayerStats.Instance.AddAmmo(giveAmmo);
            return true;
        }
    }
}
