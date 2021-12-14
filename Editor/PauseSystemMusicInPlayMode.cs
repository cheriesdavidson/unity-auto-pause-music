#define DO_AUTO_PAUSE //comment out this line to disable this script

#if UNITY_EDITOR_WIN && DO_AUTO_PAUSE
using System;
using System.Runtime.InteropServices;
using UnityEditor;

namespace AutoPauseMusic
{
    [InitializeOnLoadAttribute]
    public class PauseSystemMusicInPlayMode
    {
        public const int KEYEVENTF_EXTENTEDKEY = 1;
        public const int KEYEVENTF_KEYUP = 0;
        public const int VK_MEDIA_PLAY_PAUSE = 0xB3;

        //key press lib
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);

        // listen for playModeStateChanged when the class is initialized
        static PauseSystemMusicInPlayMode()
        {
            EditorApplication.playModeStateChanged += PlayModeStateChanged;
        }

        private static void PlayModeStateChanged(PlayModeStateChange state)
        {
            //keep those bangin' tunes if playmode is muted
            if (EditorUtility.audioMasterMute)
                return;

            //play/pause as we change state
            if (state == PlayModeStateChange.EnteredPlayMode || state == PlayModeStateChange.ExitingPlayMode)
            {
                SimulateSystemPlayPauseButton();
            }
        }
        
        private static void SimulateSystemPlayPauseButton()
        {
            keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
        }
    }
}
#endif