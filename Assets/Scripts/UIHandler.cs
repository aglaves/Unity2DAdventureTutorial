using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIHandler : MonoBehaviour
{
    public float currentHealth = .5f;
    private VisualElement m_Healthbar;

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIDocument uIDocument = GetComponent<UIDocument>();
        m_Healthbar = uIDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1f);
    }

    public void SetHealthValue(float healthPercentage) {
        m_Healthbar.style.width = Length.Percent(100 * healthPercentage);
    }

    public static UIHandler Instance { get; private set;}
}
