using UnityEngine;

namespace MyFps
{
    public class PickupLeftEye : PickupItem
    {
        protected override void DoAction()
        {
            Debug.Log("완쪽을 획득하였습니다");
            PlayerStats.Instance.GainPuzzleItem(PuzzleItem.LeftEye);

            //아이템 킬
            Destroy(gameObject);
        }
    }
}
