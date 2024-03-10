using UnityEngine;

public class SFX : MonoBehaviour
{
    [SerializeField] private AudioClip[] footsteps;
    [SerializeField] private AudioClip[] oneshots;
    private AudioSource gunshotSource;
    private AudioSource foootstepSource;
    // Start is called before the first frame update
    void Start()
    {
        gunshotSource = makeSource();
        foootstepSource = makeSource();
    }

    void Oneshot(int clipID){
        gunshotSource.PlayOneShot(oneshots[clipID]);
    }
    void Footsteps(){
        foootstepSource.PlayOneShot(footsteps[Random.Range(0,footsteps.Length)]);
    }
    private AudioSource makeSource(){
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.spatialBlend = 1f;
        source.rolloffMode = AudioRolloffMode.Linear;
        return source;
    }
}
