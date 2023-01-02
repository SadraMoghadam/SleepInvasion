// LIGHT SCRIPT

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LightSwitch: Interactable 
{

    [SerializeField] private Light m_Light; // im using m_Light name since 'light' is already a variable used by unity
    [SerializeField] private GameObject obj;
    [SerializeField] private bool isOn;

    public UnityEvent onCandleSwitch;

    private void Start() {
        UpdateLight();
    }

    IEnumerator UpdateLight()
    {
        yield return new WaitForSeconds(1f);
        onCandleSwitch.Invoke();
        m_Light.enabled = isOn;
        obj.SetActive(isOn);
    }

    public override string GetDescription() {
        if (isOn) return "Press [E] to turn <color=red>off</color> the light.";
        return "Press [E] to turn <color=green>on</color> the light.";
    }

    public override void Interact() {
        if(isOn)
            GameManager.Instance.AudioManager.Instantplay(SoundName.CandleBlow, transform.position);
        isOn = !isOn;
        StartCoroutine(UpdateLight());
    }

    public bool IsOn()
    {
        return isOn;
    }
}
