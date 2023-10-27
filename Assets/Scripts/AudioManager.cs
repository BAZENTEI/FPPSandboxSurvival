using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 音源データ管理
/// </summary>
/// 
public enum ClipName
{
    BoarAttack,
}
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;    //Singleton パターン

    private AudioClip[] audioClip;
    private Dictionary<string, AudioClip> audioClipDic;
    void Awake()
    {
        Instance = this;
        audioClipDic = new Dictionary<string, AudioClip>();
        audioClip = Resources.LoadAll<AudioClip>("Audio/All");
        Debug.Log("audio:" + audioClip.Length);
        //音リソースをロード
        for (int i = 0; i < audioClip.Length; i++)
        {
            audioClipDic.Add(audioClip[i].name, audioClip[i]);
            //  ClipName.
        }
        //テスト
        //foreach(var item in audioClipDic.Keys) {
        //Debug.Log(item , audioClipDic[item]);
        //}
    }

    public AudioClip GetAudioClipByName(ClipName clipName)
    {
        AudioClip tempClip;
        audioClipDic.TryGetValue(clipName.ToString(), out tempClip);
        return tempClip;
    }

    public void PlayAudioClipByName(ClipName clipName, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(GetAudioClipByName(clipName), pos);
    }

    public void AttachAudioSourceComponent(GameObject gameObject, ClipName clipName, bool isPlayNow = true, bool isLoop = true)
    {
        AudioSource tempAudioSource = gameObject.AddComponent<AudioSource>();
        tempAudioSource.clip = GetAudioClipByName(clipName);
        //再生
        if (isPlayNow) tempAudioSource.Play();
        tempAudioSource.loop = isLoop;
    }
}
