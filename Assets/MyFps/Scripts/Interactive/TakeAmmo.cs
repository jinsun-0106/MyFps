using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 탄환 아이템 획득하기
    /// </summary>
    public class TakeAmmo : Interactive
    {
        #region Variables
        [SerializeField]
        private int giveAmmo = 7;       //ammo 지급 갯수
        #endregion

        

        #region Custom Method


        protected override void DoAction()
        {
            //UI 감추기
            HideActionUI();

            PlayerStats.Instance.AddAmmo(giveAmmo);

            //오브젝트 제거
            //this.gameObject.SetActive(false);

            //아이템 킬
            Destroy(gameObject);


        }

        #endregion
    }
}
