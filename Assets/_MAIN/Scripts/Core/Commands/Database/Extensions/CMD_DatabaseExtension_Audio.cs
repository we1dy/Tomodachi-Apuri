using System;
using UnityEngine;

namespace COMMANDS
{
    public class CMD_DatabaseExtension_Audio : CMD_DatabaseExtension
    {
        private static string[] PARAM_SFX = new string[] { "-s", "-sfx" };
        private static string[] PARAM_VOLUME = new string[] { "-v", "-vol", "-volume" };
        private static string[] PARAM_PITCH = new string[] { "-p", "-pitch" };
        private static string[] PARAM_LOOP = new string[] { "-l", "-loop" };
        new public static void Extend(CommandDatabase database)
        {
            //SFX
            database.AddCommand("playsfx", new Action<string[]>(PlaySFX));
            database.AddCommand("stopsfx", new Action<string>(StopSFX));

            //voices
            database.AddCommand("playvoice", new Action<string[]>(PlayVoice));
            database.AddCommand("stopvoice", new Action<string>(StopVoice));
        }

        private static void PlaySFX(string[] data)
        {
            string filePath;
            float volume, pitch;
            bool loop;

            var parameters = ConvertDataToParameters(data, startingIndex: 0);

            //try get the name of path of sfx
            parameters.TryGetValue(PARAM_SFX, out filePath);

            //try get volume of sound
            parameters.TryGetValue(PARAM_VOLUME, out volume, defaultValue: 1f);

            //try get pitch of sound
            parameters.TryGetValue(PARAM_PITCH, out pitch, defaultValue: 1f);

            //try get of this sound loops
            parameters.TryGetValue(PARAM_LOOP, out loop, defaultValue: false);

            AudioClip sound = Resources.Load<AudioClip>(FilePaths.GetPathToResource(FilePaths.resources_sfx, filePath));

            if (sound == null)
                return;

            AudioManager.instance.PlaySoundEffect(sound, volume: volume, pitch: pitch, loop: loop);
        }

        private static void StopSFX(string data)
        {
            AudioManager.instance.StopSoundEffect(data);
        }

        private static void PlayVoice(string[] data)
        {
            string filePath;
            float volume, pitch;
            bool loop;

            var parameters = ConvertDataToParameters(data, startingIndex: 0);

            //try get the name of path of sfx
            parameters.TryGetValue(PARAM_SFX, out filePath);

            //try get volume of sound
            parameters.TryGetValue(PARAM_VOLUME, out volume, defaultValue: 1f);

            //try get pitch of sound
            parameters.TryGetValue(PARAM_PITCH, out pitch, defaultValue: 1f);

            //try get of this sound loops
            parameters.TryGetValue(PARAM_LOOP, out loop, defaultValue: false);

            AudioClip sound = Resources.Load<AudioClip>(FilePaths.GetPathToResource(FilePaths.resources_voices, filePath));

            if (sound == null)
            {
                Debug.Log($"Was not abble to load voice '{filePath}'");
                return;
            }         

            AudioManager.instance.PlayVoice(sound, volume: volume, pitch: pitch, loop: loop);
        }

        private static void StopVoice(string[] data)
        {
            string filePath;
            float volume, pitch;
            bool loop;

            var parameters = ConvertDataToParameters(data, startingIndex: 0);

            //try get the name of path of sfx
            parameters.TryGetValue(PARAM_SFX, out filePath);

            //try get volume of sound
            parameters.TryGetValue(PARAM_VOLUME, out volume, defaultValue: 1f);

            //try get pitch of sound
            parameters.TryGetValue(PARAM_PITCH, out pitch, defaultValue: 1f);

            //try get of this sound loops
            parameters.TryGetValue(PARAM_LOOP, out loop, defaultValue: false);

            AudioClip sound = Resources.Load<AudioClip>(FilePaths.GetPathToResource(FilePaths.resources_voices, filePath));

            if (sound == null)
            {
                Debug.Log($"Was not abble to load voice '{filePath}'");
                return;
            }

            AudioManager.instance.StopVoice(sound, volume: volume, pitch: pitch, loop: loop);
        }
    }
}