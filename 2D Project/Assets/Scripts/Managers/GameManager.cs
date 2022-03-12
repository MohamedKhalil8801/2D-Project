using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("General Settings")]
    [Range(1, 6)]
    public float GameSpeed = 1;
    [Header("Randomize Audio Pitch")]
    [Range(0, 1)]
    public float MinPitch = 0.9f;
    [Range(1, 2)]
    public float MaxPitch = 1.1f;
    [Header("References")]
    public Transform BulletsPool;

    private AudioSource audioSource;
    
    private void Awake() {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public int RandomRangeExcept (int min, int max, int except) {
        int number = 0;

        do {
        number = Random.Range(min, max);
        } while (number == except);

        return number;
    }
    public float GetRelativeGameSpeed(float max) {
        float multiplier = (max - 1) / 5.0f;
        return 1 + ((GameSpeed - 1) * multiplier);
    }

    public void PlayAudio(AudioClip audioClip, float volume, bool randomizePitch){
        if(audioClip != null){
            audioSource.pitch = (randomizePitch) ? Random.Range(MinPitch, MaxPitch) : 1;
            audioSource.PlayOneShot(audioClip, volume);
        }
    }
}
