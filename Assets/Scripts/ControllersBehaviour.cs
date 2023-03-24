using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class ControllersBehaviour : MonoBehaviour
{
    private List<UnityEngine.XR.InputDevice> devices;
    private bool leftPrimaryTouch;
    private bool rightPrimaryTouch;

    public TextMeshProUGUI Pantalla_2;

    private bool botonPantalla_2 = true;
    public string textPantalla2;

    public GameObject Panel_Botones_1, Panel_Botones_2;

    void Start()
    {
        Panel_Botones_1.SetActive(true);
        Panel_Botones_2.SetActive(false);

        //Aqui cogemos los dipositivos y los guardamos en una lista
        devices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(devices);
    }

    public void MuestraDipositivos()
    {
        //Muestra todas las caracteristicas de todos los dipositivos
        foreach (var device in devices)
        {
             textPantalla2 += string.Format("\nDispositivo : w'{0}' \nRol : '{1}'", device.name, device.role.ToString());
        }

        StartCoroutine(EscribeTexto(textPantalla2, 0.05f));
    }

    private void detectTouchPrimaryButton()
    {
        //PrimaryTouch detecta si se ha tocado (no pulsado) los botones X/A
        devices[1].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryTouch, out leftPrimaryTouch);
        devices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryTouch, out rightPrimaryTouch);
    }

    public void showTouchPrimarys()
    {
        Debug.Log("Izquierdo:" + leftPrimaryTouch);
        Debug.Log("Derecho: " + rightPrimaryTouch);
    }

    public void MuestraCaracteristicasGafas()
    {
        Pantalla_2.text = "";
        botonPantalla_2 = true;

        var text = "";
        var inputFeatures = new List<UnityEngine.XR.InputFeatureUsage>();

        var device = devices[0];

        if (device.TryGetFeatureUsages(inputFeatures))
        {
            foreach (var feature in inputFeatures)
            {
                text += string.Format("Feature : '{0}'", feature.name);
            }
        }
        StartCoroutine(EscribeTexto(text, 0.05f));
    }

    public void Button_Tablet_2()
    {
        if (botonPantalla_2)
        {
            botonPantalla_2 = false;
            MuestraDipositivos();
        } 
    }

    public void Button_Tablet_2_Dispositivos()
    {
        Panel_Botones_1.SetActive(false);
        Panel_Botones_2.SetActive(true);
    }

    public void Button_Tablet_2_Home()
    {
        Panel_Botones_1.SetActive(true);
        Panel_Botones_2.SetActive(false);
    }

    private IEnumerator EscribeTexto(string frase, float tiempo)
    {
        Pantalla_2.text = "";

        foreach(char character in frase)
        {
            Pantalla_2.text = Pantalla_2.text + character;
            yield return new WaitForSeconds(tiempo);
        }
    }
}
