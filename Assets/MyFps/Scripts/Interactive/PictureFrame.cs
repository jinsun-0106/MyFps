using System.Collections;
using TMPro;
using UnityEngine;

namespace MyFps
{
    public class PictureFrame : Interactive
    {
        #region Variables
        //도어스위치
        public GameObject doorSwitch;
        //눈 퍼즐 조각
        public GameObject leftEye;
        public GameObject rightEye;

        //시퀀스 텍스트
        public TextMeshProUGUI squenceText;

        private bool isDone = false;
        #endregion

        #region Unity Event Method

        #endregion

        #region Custom Method
        protected override void DoAction()
        {
            StartCoroutine(MakePicture());
        }

        IEnumerator MakePicture()
        {
            bool isLeft = PlayerStats.Instance.HavePuzzleItem(PuzzleItem.LeftEye);
            bool isRight = PlayerStats.Instance.HavePuzzleItem(PuzzleItem.RightEye);

            //퍼즐 조각 맞추기
            if (isLeft)
            {
                LeftEyePiece();

                yield return new WaitForSeconds(0.1f);

                //충돌체 복구
                collider.enabled = true;

                if (isRight)
                {
                    RighteyePiece();
                    isDone = true;
                    //충돌체 제거
                    collider.enabled = false;

                }
                else
                {
                    squenceText.text = "You need more pieces";
                    yield return new WaitForSeconds(2f);
                    squenceText.text = "";
                }

            }
            if(isRight)
            {
                RighteyePiece();

                yield return new WaitForSeconds(0.1f);

                //충돌체 복구
                collider.enabled = true;

                if (isLeft)
                {
                    LeftEyePiece();
                    isDone = true;
                    //충돌체 제거
                    collider.enabled = false;
                }
                else
                {
                    squenceText.text = "You need more pieces";
                    yield return new WaitForSeconds(2f);
                    squenceText.text = "";
                }
            }
            else
            {
                squenceText.text = "You need pieces";
                yield return new WaitForSeconds(2f);
                squenceText.text = "";
                //충돌체 복구
                collider.enabled = true;
            }

            //모든 조각을 다 맞추었는지 체크
            if (isDone)
            {
                doorSwitch.SetActive(true);
            }

        }


        //왼쪽 눈 활성화
        void LeftEyePiece()
        {
            leftEye.SetActive(true);
        }

        //오른쪽 눈 활성화
        void RighteyePiece()
        {
            rightEye.SetActive(true);
        }
        #endregion

    }
}
