using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 퍼즐 아이템 줍기
    /// </summary>
    public class PickupPuzzleItem : PickupItem
    {
        #region Variables
        //획득할 퍼증 아이템
        [SerializeField]
        private PuzzleItem puzzleItem = PuzzleItem.None;
        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            //퍼즐 아이템 획득
            bool isGain = PlayerStats.Instance.GainPuzzleItem(puzzleItem);

            //아이템 킬
            if (isGain)
            {
                Destroy(gameObject);
            }
        }
        #endregion
    }
}
