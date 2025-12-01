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

        //눈
        public GameObject leftEye;
        public GameObject rightEye;

        private PuzzleItem left = PuzzleItem.LeftEye;
        private PuzzleItem right = PuzzleItem.RightEye;

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
            
            if(PlayerStats.Instance.HavePuzzleItem(left))
            {
                LeftEyePiece();

                yield return new WaitForSeconds(0.1f);

                //충돌체 복구
                collider.enabled = true;

                if (PlayerStats.Instance.HavePuzzleItem(right))
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
            else if(PlayerStats.Instance.HavePuzzleItem(right))
            {
                RighteyePiece();

                yield return new WaitForSeconds(0.1f);

                //충돌체 복구
                collider.enabled = true;

                if (PlayerStats.Instance.HavePuzzleItem(left))
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

            if(isDone)
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
