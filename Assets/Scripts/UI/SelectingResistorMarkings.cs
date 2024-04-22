using Obi;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class SelectingResistorMarkings : MonoBehaviour
{
    [Header("Resistor")]
    [SerializeField] PrefabRisistNominalSetting prefabResist;
    [SerializeField] TMP_Text resistNominalText;
    [SerializeField] List<Color> colorDigitalList;
    [SerializeField] List<UnityEngine.UI.Image> imageColorRisist;

    [Header("ChoiceResist Window")]
    [SerializeField] GameObject choiceWindow;

    [Header("Point spawn")]
    [SerializeField] Transform pointSpawnResist;

    private float firstDigital = 0;
    private float secondDigital = 0;
    private float thirdDigital = 0;
    private double multiplicate = 1;
    private float admittance = 1;
    private string multiplicateString = "ŒÏ";

    private float firstNominal;
    private double nominal;
    private double endNominal;
    private int divisor = 1;
    private string decimalMultiString = "1";

    public void FirstDigitalValue(int value)
    {
        prefabResist.SetFirstColor(value);
        firstDigital = value;
        switch (value)
        {
            case 0:
                imageColorRisist[0].color = colorDigitalList[2];
                break;
            case 1:
                imageColorRisist[0].color = colorDigitalList[3];
                break;
            case 2:
                imageColorRisist[0].color = colorDigitalList[4];
                break;
            case 3:
                imageColorRisist[0].color = colorDigitalList[5];
                break;
            case 4:
                imageColorRisist[0].color = colorDigitalList[6];
                break;
            case 5:
                imageColorRisist[0].color = colorDigitalList[7];
                break;
            case 6:
                imageColorRisist[0].color = colorDigitalList[8];
                break;
            case 7:
                imageColorRisist[0].color = colorDigitalList[9];
                break;
            case 8:
                imageColorRisist[0].color = colorDigitalList[10];
                break;
            case 9:
                imageColorRisist[0].color = colorDigitalList[11];
                break;
        }
        NominalTextOut(decimalMultiString);
    }

    public void SecondDigitalValue(int value) 
    {
        prefabResist.SetSecondColor(value);
        secondDigital = value;
        switch (value)
        {
            case 0:
                imageColorRisist[1].color = colorDigitalList[2];
                break;
            case 1:
                imageColorRisist[1].color = colorDigitalList[3];
                break;
            case 2:
                imageColorRisist[1].color = colorDigitalList[4];
                break;
            case 3:
                imageColorRisist[1].color = colorDigitalList[5];
                break;
            case 4:
                imageColorRisist[1].color = colorDigitalList[6];
                break;
            case 5:
                imageColorRisist[1].color = colorDigitalList[7];
                break;
            case 6:
                imageColorRisist[1].color = colorDigitalList[8];
                break;
            case 7:
                imageColorRisist[1].color = colorDigitalList[9];
                break;
            case 8:
                imageColorRisist[1].color = colorDigitalList[10];
                break;
            case 9:
                imageColorRisist[1].color = colorDigitalList[11];
                break;
        }
        NominalTextOut(decimalMultiString);
    }

    public void ThirdDigitalValue(int value)
    {
        prefabResist.SetThirdColor(value);
        thirdDigital = value;
        switch (value)
        {
            case 0:
                imageColorRisist[2].color = colorDigitalList[2];
                break;
            case 1:
                imageColorRisist[2].color = colorDigitalList[3];
                break;
            case 2:
                imageColorRisist[2].color = colorDigitalList[4];
                break;
            case 3:
                imageColorRisist[2].color = colorDigitalList[5];
                break;
            case 4:
                imageColorRisist[2].color = colorDigitalList[6];
                break;
            case 5:
                imageColorRisist[2].color = colorDigitalList[7];
                break;
            case 6:
                imageColorRisist[2].color = colorDigitalList[8];
                break;
            case 7:
                imageColorRisist[2].color = colorDigitalList[9];
                break;
            case 8:
                imageColorRisist[2].color = colorDigitalList[10];
                break;
            case 9:
                imageColorRisist[2].color = colorDigitalList[11];
                break;
        }
        NominalTextOut(decimalMultiString);
    }

    public void DecimalMultiplier(string multiplier)
    {
        prefabResist.SetDecimalColor(multiplier);
        switch (multiplier)
        {
            case "0,01":
                imageColorRisist[3].color = colorDigitalList[0];
                multiplicate = 0.01f;

                break;
            case "0,1":
                imageColorRisist[3].color = colorDigitalList[1];
                multiplicate = 0.1f;

                break;
            case "1":
                imageColorRisist[3].color = colorDigitalList[2];
                multiplicate = 1f;
                divisor = 1;
                break;
            case "10":
                imageColorRisist[3].color = colorDigitalList[3];
                multiplicate = 10;
                divisor = 1000;

                break;
            case "100":
                imageColorRisist[3].color = colorDigitalList[4];
                multiplicate = 100;
                divisor = 1000;
                break;

            case "1000":
                imageColorRisist[3].color = colorDigitalList[5];
                multiplicate = 1000;
                divisor = 1000;

                break;
            case "10000":
                imageColorRisist[3].color = colorDigitalList[6];
                multiplicate = 10000;
                divisor = 1000000;
                break;

            case "100000":
                imageColorRisist[3].color = colorDigitalList[7];
                multiplicate = 100000;
                divisor = 1000000;
                break;

            case "1000000":
                imageColorRisist[3].color = colorDigitalList[8];
                multiplicate = 1000000;
                divisor = 1000000;
                break;

            case "10000000":
                imageColorRisist[3].color = colorDigitalList[9];
                multiplicate = 10000000;
                divisor = 1000000000;
                break;

            case "100000000":
                imageColorRisist[3].color = colorDigitalList[10];
                multiplicate = 100000000;
                divisor = 1000000000;
                break;

            case "1000000000":
                imageColorRisist[3].color = colorDigitalList[11];
                multiplicate = 1000000000;
                divisor = 1000000000;
                break;
        }
        decimalMultiString = multiplicate.ToString();
        NominalTextOut(decimalMultiString);
    }

    public void Admittance(int value)
    {
        prefabResist.SetAdmittanceColor(value);
        switch (value)
        {
            case 0:
                imageColorRisist[4].color = colorDigitalList[0];
                admittance = 10;
                break;
            case 1:
                imageColorRisist[4].color = colorDigitalList[1];
                admittance = 5;
                break;
            case 2:
                imageColorRisist[4].color = colorDigitalList[3];
                admittance = 1;
                break;
            case 3:
                imageColorRisist[4].color = colorDigitalList[4];
                admittance = 2;
                break;
            case 4:
                imageColorRisist[4].color = colorDigitalList[7];
                admittance = 0.5f;
                break;
            case 5:
                imageColorRisist[4].color = colorDigitalList[8];
                admittance = 0.25f;
                break;
            case 6:
                imageColorRisist[4].color = colorDigitalList[9];
                admittance = 0.10f;
                break;
            case 7:
                imageColorRisist[4].color = colorDigitalList[10];
                admittance = 0.05f;
                break;
        }
        NominalTextOut(decimalMultiString);
    }
    
    private void NominalTextOut(string multi)
    {
        if(firstDigital == 0 && secondDigital == 0)
        {
            firstNominal = thirdDigital;
        }
        else if (firstDigital == 0)
        {
            string nominalString = $"{secondDigital}" + $"{thirdDigital}";
            firstNominal = float.Parse(nominalString);
        }
        else
        {
            string nominalString = $"{firstDigital}" + $"{secondDigital}" + $"{thirdDigital}";
            firstNominal = float.Parse(nominalString);
        }
        //resistNominalText.text = $"{firstDigital}{secondDigital}{thirdDigital} {multiplicateString} ± {admittance}%";

        nominal = firstNominal * multiplicate;
        endNominal = nominal;

        switch (multi)
        {
            case "0,01":
                multiplicateString = "ŒÏ";
                break;
            case "0,1":
                multiplicateString = "ŒÏ";
                break;
            case "1":
                multiplicateString = "ŒÏ";
                break;
            case "10":
                if (nominal>=1000)
                {
                    nominal = nominal / 1000;
                    multiplicateString = "ÍŒÏ";
                }
                else
                {
                    multiplicateString = "ŒÏ";
                }
                break;
            case "100":
                if (nominal>=1000)
                {
                    nominal = nominal / 1000;
                    multiplicateString = "ÍŒÏ";
                }
                else
                {
                    multiplicateString = "ŒÏ";
                }
                break;
            case "1000":
                if (nominal >= 1000)
                {
                    nominal = nominal / 1000;
                    multiplicateString = "ÍŒÏ";
                }
                else
                {
                    multiplicateString = "ŒÏ";
                }
                break;
            case "10000":
                if (nominal >= 1000000)
                {
                    nominal = nominal / 1000000;
                    multiplicateString = "ÃŒÏ";
                }
                else
                {
                    nominal = nominal / 1000;
                    multiplicateString = "ÍŒÏ";
                }
                break;
            case "100000":
                if (nominal >= 1000000)
                {
                    nominal = nominal / 1000000;
                    multiplicateString = "ÃŒÏ";
                }
                else
                {
                    nominal = nominal / 1000;
                    multiplicateString = "ÍŒÏ";
                }
                break;
            case "1000000":
                if (nominal >= 1000000)
                {
                    nominal = nominal / 1000000;
                    multiplicateString = "ÃŒÏ";
                }
                else
                {
                    nominal = nominal / 1000;
                    multiplicateString = "ÍŒÏ";
                }
                break;

            case "10000000":
                if (nominal >= 1000000000)
                {
                    nominal = nominal / 1000000000;
                    multiplicateString = "√ŒÏ";
                }
                else
                {
                    nominal = nominal / 1000000;
                    multiplicateString = "ÃŒÏ";
                }
                break;
            case "100000000":
                if (nominal >= 1000000000)
                {
                    nominal = nominal / 1000000000;
                    multiplicateString = "√ŒÏ";
                }
                else
                {
                    nominal = nominal / 1000000;
                    multiplicateString = "ÃŒÏ";
                }
                break;
            case "1000000000":
                if (nominal >= 1000000000)
                {
                    nominal = nominal / 1000000000;
                    multiplicateString = "√ŒÏ";
                }
                else
                {
                    nominal = nominal / 1000000;
                    multiplicateString = "ÃŒÏ";
                }
                break;
        }
        prefabResist.resistNominal = endNominal;
        resistNominalText.text = nominal.ToString() + $" {multiplicateString} ± {admittance}%";
    }

    public void SetPrefabAndStartSetting(int firstValue,int secondValue, int thirdValue,string multiplicateValue,int admittanceValue,PrefabRisistNominalSetting prefab)
    {
        prefabResist = prefab;
        FirstDigitalValue(firstValue);
        SecondDigitalValue(secondValue);
        ThirdDigitalValue(thirdValue);
        DecimalMultiplier(multiplicateValue);
        Admittance(admittanceValue);
    }


    public void BackCloseMarkingWindow()
    {
        choiceWindow.SetActive(true);
        transform.gameObject.SetActive(false);
    }

    public void ApplayMarking()
    {
        Instantiate(prefabResist.transform.gameObject,pointSpawnResist.position,pointSpawnResist.rotation);
        transform.gameObject.SetActive(false);
    }
}
