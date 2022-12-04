// LIGHT SCRIPT

using System.Collections;
using UnityEngine;

public class LightSwitch: Interactable 
{

    [SerializeField] private Light m_Light; // im using m_Light name since 'light' is already a variable used by unity
    [SerializeField] private GameObject obj;
    [SerializeField] private bool isOn;

    private void Start() {
        UpdateLight();
    }

    IEnumerator UpdateLight()
    {
        yield return new WaitForSeconds(.5f);
        m_Light.enabled = isOn;
        obj.SetActive(isOn);
    }

    public override string GetDescription() {
        if (isOn) return "Press [E] to turn <color=red>off</color> the light.";
        return "Press [E] to turn <color=green>on</color> the light.";
    }

    public override void Interact() {
        if(isOn)
            GameManager.Instance.AudioManager.play(SoundName.CandleBlow);
        isOn = !isOn;
        StartCoroutine(UpdateLight());
    }
}
