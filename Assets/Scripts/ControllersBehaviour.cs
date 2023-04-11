using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class ControllersBehaviour : MonoBehaviour
{
    #region Variables
    private List<UnityEngine.XR.InputDevice> devices;
    private bool leftPrimaryTouch;
    private bool rightPrimaryTouch;

    public TextMeshProUGUI Pantalla_2, Pantalla_1;

    private bool botonPantalla_2 = true;
    public string textPantalla2;

    public GameObject Panel_Botones_1, Panel_Botones_2;

    public bool MandoIActivate;
    public bool MandoDActivate;

    [Header("Primary Touch")]
    public bool MandoIPrimaryTouch;
    public bool MandoDPrimaryTouch;

    [Header("Gatillo")]
    public bool MandoIGatillo;
    public bool MandoDGatillo;

    [Header("Joystick")]
    public Vector2 MandoIJoystick;
    public Vector2 MandoDJoystick;

    [Header("Velocidad")]
    public Vector3 VelocidadMandoI;
    public Vector3 VelocidadMandoD;

    [Header("Aceleracion")]
    public Vector3 AceleracionMandoI;
    public Vector3 AceleracionMandoD;
    #endregion

    #region Metodos Unity
    void Start()
    {
        Panel_Botones_1.SetActive(true);
        Panel_Botones_2.SetActive(false);

        //Aqui cogemos los dipositivos y los guardamos en una lista
        devices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(devices);
    }

    private void Update()
    {
        detectTouchPrimaryButton();
        DetectarGatillo();
        DetectarJoystick();
        DetectarVelocidad();
        DetectarAceleracion();

        if (MandoIActivate)
        {
            MostrarGetFeaturesMandoI();
        }
        
        if(MandoDActivate)
        {
            MostrarGetFeaturesMandoD();
        }
        
    }
    #endregion

    #region Metodos Propios
    

    #region Caracteristicas Dispositivo
    //Primary Touch
    private void detectTouchPrimaryButton()
    {
        //PrimaryTouch detecta si se ha tocado (no pulsado) los botones X/A
        MandoIPrimaryTouch =  devices[1].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryTouch, out leftPrimaryTouch);
        MandoDPrimaryTouch = devices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryTouch, out rightPrimaryTouch);
    }

    //Gatillo
    private void DetectarGatillo()
    {
        MandoIGatillo = devices[1].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out MandoIGatillo);
        MandoDGatillo = devices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out MandoDGatillo);

    }

    //Joystick
    private void DetectarJoystick()
    {
        devices[1].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out MandoIJoystick);
        devices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out MandoDJoystick);
    }

    //Velocidad
    private void DetectarVelocidad()
    {
        devices[1].TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out VelocidadMandoI);
        devices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out VelocidadMandoD);
    }

    //Aceleracion
    private void DetectarAceleracion()
    {
        devices[1].TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceAcceleration, out AceleracionMandoI);
        devices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceAcceleration, out AceleracionMandoD);
    }
    #endregion

    #region Muestra las Caracteristicas de dispositivos
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

    public void MuestraCaracteristicasMandoD()
    {
        Pantalla_2.text = "";
        botonPantalla_2 = true;

        var text = "";
        var inputFeatures = new List<UnityEngine.XR.InputFeatureUsage>();

        var device = devices[2];

        if (device.TryGetFeatureUsages(inputFeatures))
        {
            foreach (var feature in inputFeatures)
            {
                text += string.Format("Feature : '{0}'", feature.name);
            }
        }
        StartCoroutine(EscribeTexto(text, 0.05f));
    }

    public void MuestraCaracteristicasMandoI()
    {
        Pantalla_2.text = "";
        botonPantalla_2 = true;

        var text = "";
        var inputFeatures = new List<UnityEngine.XR.InputFeatureUsage>();

        var device = devices[1];

        if (device.TryGetFeatureUsages(inputFeatures))
        {
            foreach (var feature in inputFeatures)
            {
                text += string.Format("Feature : '{0}'", feature.name);
            }
        }
        StartCoroutine(EscribeTexto(text, 0.05f));
    }
    #endregion

    #region Botones de la UI
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
    #endregion


    #region Metodos Que tocan las pantallas
    public void MostrarGetFeaturesMandoI()
    {
        Pantalla_1.text = "Mando Izquierdo";
        Pantalla_1.text += "\nPrimary Touch : " + MandoIPrimaryTouch.ToString();
        Pantalla_1.text += "\nGatillo : " + MandoIGatillo.ToString();
        Pantalla_1.text += "\nJoystick : " + MandoIJoystick.ToString();
        Pantalla_1.text += "\nVelocidad : " + VelocidadMandoI.ToString();
        Pantalla_1.text += "\nAceleracion : " + AceleracionMandoI.ToString();
    }
    
    public void MostrarGetFeaturesMandoD()
    {
        Pantalla_1.text = "Mando Derecho";
        Pantalla_1.text += "\nPrimary Touch : " + MandoDPrimaryTouch.ToString();
        Pantalla_1.text += "\nGatillo : " + MandoDGatillo.ToString();
        Pantalla_1.text += "\nJoystick : " + MandoDJoystick.ToString();
        Pantalla_1.text += "\nVelocidad : " + VelocidadMandoD.ToString();
        Pantalla_1.text += "\nAceleracion : " + AceleracionMandoD.ToString();
    }

    private IEnumerator EscribeTexto(string frase, float tiempo)
    {
        Pantalla_2.text = "";

        foreach (char character in frase)
        {
            Pantalla_2.text = Pantalla_2.text + character;
            yield return new WaitForSeconds(tiempo);
        }
    }
    #endregion

    public void ActivarMandoI()
    {
        MandoIActivate = true;
        MandoDActivate= false;
    }
    
    public void ActivarMandoD()
    {
        MandoIActivate = false;
        MandoDActivate= true;
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

    #endregion
}
