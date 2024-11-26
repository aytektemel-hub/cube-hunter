using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public UIController _UIController;
    public PlayerControl _PlayerControl;
    public Text LevelTextRun;
    public Text LevelTextFire;
    public Text PriceRunText;
    public Text PriceFireText;
    public Button UpgradeRunButton;
    public Button UpgradeFireButton;

    public int[] UpgradePrices = new int[9];

    private void Awake()
    {
        SetUpgrades(PriceRunText, "RunLevel", UpgradeRunButton, LevelTextRun);
        SetUpgrades(PriceFireText, "FireLevel", UpgradeFireButton, LevelTextFire);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetUpgrades(Text PriceText, string PlayerPrefsName, Button UpgradeButton, Text LevelText)
    {
        int level = PlayerPrefs.GetInt(PlayerPrefsName, 1);
        LevelText.text = "LVL " + PlayerPrefs.GetInt(PlayerPrefsName, 1).ToString();

        if (level < 10)
        {
            PriceText.text = UpgradePrices[level - 1].ToString();

        }
        else
        {
            PriceText.text = "MAX";
            UpgradeButton.interactable = false;
        }

    }

    public void UpgradeRunButtonClick()
    {
        int level = PlayerPrefs.GetInt("RunLevel", 1);

        if (PlayerPrefs.GetInt("Coin", 0) >= UpgradePrices[level - 1])
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) - UpgradePrices[level - 1]);
            _UIController.CoinText.text = PlayerPrefs.GetInt("Coin", 0).ToString();
            PlayerPrefs.SetInt("RunLevel", level + 1);
            LevelTextRun.text = "LVL " + PlayerPrefs.GetInt("RunLevel", 1).ToString();
            _PlayerControl.PlayerSpeed = _PlayerControl.PlayerSpeeds[level];
            if (level == 9)
            {
                PriceRunText.text = "MAX";
                UpgradeRunButton.interactable = false;
            }
            else
            {
                PriceRunText.text = UpgradePrices[level].ToString();
            }


        }

    }

    public void UpgradeFireButtonClick()
    {
        int level = PlayerPrefs.GetInt("FireLevel", 1);

        if (PlayerPrefs.GetInt("Coin", 0) >= UpgradePrices[level - 1])
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin", 0) - UpgradePrices[level - 1]);
            _UIController.CoinText.text = PlayerPrefs.GetInt("Coin", 0).ToString();
            PlayerPrefs.SetInt("FireLevel", level + 1);
            LevelTextFire.text = "LVL " + PlayerPrefs.GetInt("FireLevel", 1).ToString();
            _PlayerControl.fireTime = _PlayerControl.FireSpeeds[level];

            if (level == 9)
            {
                PriceFireText.text = "MAX";
                UpgradeFireButton.interactable = false;
            }
            else
            {
                PriceFireText.text = UpgradePrices[level].ToString();
            }
        }

    }

}
