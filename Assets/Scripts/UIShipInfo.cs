using UnityEngine;
using System.Collections;
using UnityEngine.UI;




public class UIShipInfo : MonoBehaviour {

    #region textFields
    [SerializeField]    Image shipImage;
    [SerializeField]    Text healthText;
    [SerializeField]    Text armorText;
    [SerializeField]    Text sheildText;
    [SerializeField]    Text energyText;
    [SerializeField]    Text energyRegenText;
    [SerializeField]    Text speedText;
    [SerializeField]    Text thrustText;
    [SerializeField]    Text turnRateText;
    [SerializeField]    Text massText;
    public Button setShipButton;
    #endregion

    #region getters/setters
    public Sprite ShipImage
    {
        get { return shipImage.sprite; }
        set { shipImage.sprite = value; }

     }

    public string HealthText
    {
        get{return healthText.text;}
        set{ healthText.text = value;}
    }
    public string ArmorText
    {
        get { return armorText.text; }
        set { armorText.text = value; }
    }
    public string SheildText
    {
        get { return sheildText.text; }
        set { sheildText.text = value; }
    }
    public string EnergyText
    {
        get { return energyText.text; }
        set { energyText.text = value; }
    }
    public string EnergyRegenText
    {
        get { return energyRegenText.text; }
        set { energyRegenText.text = value; }
    }
    public string SpeedText
    {
        get { return speedText.text; }
        set { thrustText.text = value; }
    }
    public string ThrustText
    {
        get { return thrustText.text; }
        set { thrustText.text = value; }
    }
    public string TurnRateText
    {
        get { return turnRateText.text; }
        set { turnRateText.text = value; }
    }
    public string MassText
    {
        get { return massText.text; }
        set { massText.text = value; }
    }
    #endregion

    public void setUpButton(GameObject shipPrefab, ControlSwitcher switcher)
    {
        setShipButton.onClick.AddListener(() => {
            switcher.SetMainShip(shipPrefab);            
        });
    }
}
