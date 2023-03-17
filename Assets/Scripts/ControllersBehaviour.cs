using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControllersBehaviour : MonoBehaviour
{
    private List<UnityEngine.XR.InputDevice> devices;
    private bool leftPrimaryTouch;
    private bool rightPrimaryTouch;

    public TextMeshProUGUI Pantalla_2;
    
    void Start()
    {
        //Aqui cogemos los dipositivos y los guardamos en una lista
        devices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(devices);
    }

    public void MuestraDipositivos()
    {
        //Muestra todas las caracteristicas de todos los dipositivos
        foreach (var device in devices)
        {
            Debug.Log(string.Format("Device name w'{0}' has characteristics '{1}'", device.name, device.characteristics.ToString()));
        }
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


    public void Button_Tablet_2(string texto)
    {
        StartCoroutine(EscribeTexto(texto));
    }

    private IEnumerator EscribeTexto(string frase)
    {
        Pantalla_2.text = "";

        foreach(char character in frase)
        {
            Pantalla_2.text = Pantalla_2.text + character;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
