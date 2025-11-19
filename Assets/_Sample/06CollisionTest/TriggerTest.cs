using UnityEngine;

namespace MySample
{
    /// <summary>
    /// 충돌체의 Trigger 충돌 체크
    /// </summary>
    public class TriggerTest : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"OnTriggerEnter: {other.name}");
            //오른쪽으로 힘(200f), 컬러를 빨간색
            MoveObject moveObject = other.GetComponent<MoveObject>();
            if (moveObject)
            {
                moveObject.MoveRight();
                moveObject.ChangeMoveColor();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log($"OnTriggerStay: {other.name}");
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log($"OnTriggerExit: {other.name}");
            //오른쪽으로 힘(200f), 컬러를 오리지널
            MoveObject moveObject = other.GetComponent<MoveObject>();
            if (moveObject)
            {
                moveObject.MoveRight();
                moveObject.ResetMoveColor();
            }
        }
    }
}
