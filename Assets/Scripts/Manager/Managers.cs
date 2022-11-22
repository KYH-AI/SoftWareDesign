using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers instance;
    public static Managers Instance { get { return instance; } }

    private ResourceManager resource = new ResourceManager(); // ��üȭ ����
    public static ResourceManager Resource { get { return Instance.resource; } }

    private SoundManager sound;
    public static SoundManager Sound { get { return Instance.sound; } }

    private ButtonManager button;
    public static ButtonManager Button { get { return Instance.button; } }

    private SkillEffectVolumeManager skillEffectVolume;
    public static SkillEffectVolumeManager SkillEffectVolume { get { return Instance.skillEffectVolume; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        MangersInit();
    }

    private void MangersInit()
    {
        sound = GetComponentInChildren<SoundManager>();
        button = GetComponentInChildren<ButtonManager>();
        skillEffectVolume = GetComponentInChildren<SkillEffectVolumeManager>();
    }
}
