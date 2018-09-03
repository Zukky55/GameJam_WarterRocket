using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//効果音を管理・再生するスクリプト
public class SoundEffectsManager : MonoBehaviour {

    private static SoundEffectsManager _soundEffectsManager = null;
    public static SoundEffectsManager GetSoundEffectsManager() {
        return _soundEffectsManager;
    }

    //ロケットの噴射音用
    [HideInInspector] public AudioSource fireAudio;

    //衝突時の効果音
    [HideInInspector] public AudioSource collideAudio;

    //墜落時の効果音
    [HideInInspector] public AudioSource downAudio;

    //敵のビームの効果音
    [HideInInspector] public AudioSource beamAudio;

    [SerializeField]
    AudioClip[] soundEffects = new AudioClip[4];

	void Start () {
        _soundEffectsManager = this;

        fireAudio = GetComponent<AudioSource>();
        fireAudio.clip = soundEffects[0];
        fireAudio.loop = true;

        collideAudio = gameObject.AddComponent<AudioSource>();
        collideAudio.clip = soundEffects[1];
        collideAudio.playOnAwake = false;
        collideAudio.loop = true;

        downAudio = gameObject.AddComponent<AudioSource>();
        downAudio.clip = soundEffects[1];
        downAudio.playOnAwake = false;
        downAudio.loop = false;

        beamAudio = gameObject.AddComponent<AudioSource>();
        beamAudio.clip = soundEffects[1];
        beamAudio.playOnAwake = false;
        beamAudio.loop = false;
	}
}
