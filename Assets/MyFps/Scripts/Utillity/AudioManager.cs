using UnityEngine;

namespace MyFps
{
    /// <summary>
    /// 사운드 데이터를 관리하는 싱글톤 클래스
    /// </summary>
    public class AudioManager : Singleton<AudioManager>
    {
        #region Variables
        public Sound[] sounds;              //관리하는 사운드 목록

        private string bgmSound = "";        //현재 플레이 되고 있는 배경음
        #endregion

        #region Unity Event Method
        protected override void Awake()
        {
            base.Awake();

            //사운드 목록 데이터 셋팅
            foreach (var sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();

                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
                sound.source.playOnAwake = sound.playOnAwake;
            }
        }
        #endregion

        #region Custom Method
        //사운드 플래이 시작
        public void Play(string name)
        {
            //플레이할 사운드
            Sound sound = null;

            foreach(var s in sounds)
            {
                if(s.name == name)
                {
                    sound = s;
                    break;                  //찾았으면 반복문 정지
                }
            }

            if(sound == null)
            {
                Debug.Log($"Cannot Find {name} Play Sound");
                return;
            }

            sound.source.Play();

        }

        //사운드 플레이 정기
        public void Stop(string name)
        {
            //정지할 사운드
            Sound sound = null;

            foreach (var s in sounds)
            {
                if (s.name == name)
                {
                    sound = s;
                    break;                  //찾았으면 반복문 정지
                }
            }

            if (sound == null)
            {
                Debug.Log($"Cannot Find {name} Stop Sound");
                return;
            }

            sound.source.Stop();
        }

        //배경음 플레이
        public void PlayBGM(string name)
        {
            //배경음 이름 체크
            if(bgmSound == name)
            {
                return;
            }

            //기존 배경음 정지 - 정지할 배경음
            foreach (var s in sounds)
            {
                if (s.name == bgmSound)
                {
                    //찾았으면 찾은 오디오소스 플레이 정지
                    s.source.Stop();
                    break;
                }
            }

            //새로운 배경음 플레이
            Sound sound = null;

            foreach (var s in sounds)
            {
                if (s.name == name)
                {
                    sound = s;
                    bgmSound = s.name;
                    break;                  //찾았으면 반복문 정지
                }
            }

            //못찾으면
            if (sound == null)
            {
                Debug.Log($"Cannot Find {name} BGM Sound");
                return;
            }

            sound.source.Play();
        }

        #endregion
    }
}
