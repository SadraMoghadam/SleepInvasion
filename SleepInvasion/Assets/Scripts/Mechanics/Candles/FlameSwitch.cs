// Flame SCRIPT
using UnityEngine;

public class FlameSwitch: Interactable {

    [SerializeField] private GameObject obj;
    [SerializeField] private bool isOn;

    private void Start() {
        isOn = true;
        UpdateFlame();
    }

    void UpdateFlame() {
        obj.SetActive(isOn);
    }

    public override string GetDescription() {
        if (isOn) return "Press [E] to turn <color=red>off</color> the light.";
        return "Press [E] to turn <color=green>on</color> the light.";
    }

    public override void Interact() {
        isOn = !isOn;
        UpdateFlame();
    }
}
