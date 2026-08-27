using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource chaseBgmSource;

    [SerializeField] AudioClip normalBgm;
    [SerializeField] AudioClip chaseBgm;

    private void Awake()
    {
        Instance = this;
    }


}
