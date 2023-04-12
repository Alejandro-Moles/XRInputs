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

    public TextMeshProUGUI Pantalla_2, Pantalla_1, Pantalla_Ordenador_1, Pantalla_Ordenador_2, Pantalla_Ordenador_3;

    private bool botonPantalla_2 = true;
    public string textPantalla2;

    public GameObject Panel_Botones_1, Panel_Botones_2;

    //Mandos
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

    [Header("Secondary")]
    public bool SecondaryTouch;
    public bool secondaryButton;

    [Header("GripButton")]
    public bool GripButton;

    [Header("Gip Sensibilidad")]
    public float GripSensi;

    //Gafas
    public bool GafasActivate;

    [Header("Gafa Puestas")]
    public bool GafasPuestas;

    [Header("PosicionOjo")]
    public Vector3 PosicionOjoI;
    public Vector3 PosicionOjoD;

    [Header("VelocidadOjo")]
    public Vector3 VelocidadOjoI;
    public Vector3 VelocidadOjoD;

    [Header("Rotacion Ojos")]
    public Quaternion RotacionOjoI;
    public Quaternion RotacionOjoD;

    [Header("Rotacion Gafas")]
    public Quaternion RotacionGafas;

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
        //Mando
        detectTouchPrimaryButton();
        DetectarGatillo();
        DetectarJoystick();
        DetectarVelocidad();
        DetectarAceleracion();

        //Gafas
        DetectarGafasPuestas();
        DetectarPosicionOjos();
        DetectarVelocidadOjos();

        if (MandoIActivate)
        {
            MostrarGetFeaturesMandoI();
        }
        
        if(MandoDActivate)
        {
            MostrarGetFeaturesMandoD();
        }

        if (GafasActivate)
        {
            MostrarGetFeaturesGafas();
        }

        MuestraPantallaOrdenador_1();
        MuestraPantallaOrdenador_2();
        MuestraPantallaOrdenador_3();

    }
    #endregion

    #region Metodos Propios
    

    #region Caracteristicas Dispositivos
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

    //Gafas Puestas
    private void DetectarGafasPuestas()
    {
        devices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.userPresence, out GafasPuestas);
    }

    //Posicion de los Ojos
    private void DetectarPosicionOjos()
    {
        devices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.leftEyePosition, out PosicionOjoI);
        devices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.rightEyePosition, out PosicionOjoD);
    }

    //Velocidad de los Ojos
    private void DetectarVelocidadOjos()
    {
        devices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.leftEyeVelocity, out VelocidadOjoI);
        devices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.rightEyeVelocity, out VelocidadOjoD);
    }

    //Rotacion de los ojos
    public void DetectarRotacionOjos()
    {
        devices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.leftEyeRotation, out RotacionOjoI);
        devices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.rightEyeRotation, out RotacionOjoD);
    }

    //Rotacion
    public void DetectarRotacion()
    {
        devices[0].TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out RotacionGafas);
    }

    //GripButton
    public void DetectarGripButton()
    {
        devices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.gripButton, out GripButton);
    }

    //Sensibilidad Grip
    public void DetectarGripSensi()
    {
        devices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out GripSensi);
    }

    //Secondary Touch
    public void DetectarSecondaryTouch()
    {
        devices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryTouch, out SecondaryTouch);
    }

    //Secondaty button
    public void DetectarSecondatuButton()
    {
        devices[2].TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out secondaryButton);
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

    public void MostrarGetFeaturesGafas()
    {
        Pantalla_1.text = "Gafas";
        Pantalla_1.text += "\nGafas Puestas : " + GafasPuestas.ToString();
        Pantalla_1.text += "\nPosicion Ojo Derecho : " + PosicionOjoD.ToString();
        Pantalla_1.text += "\nPosicion Ojo Izquierdo : " + PosicionOjoI.ToString();
        Pantalla_1.text += "\nVelocidad Ojo Derecho : " + VelocidadOjoD.ToString();
        Pantalla_1.text += "\nVelocidad Ojo Izquierdo : " + VelocidadOjoI.ToString();
    }

    public void MuestraPantallaOrdenador_1()
    {
        Pantalla_Ordenador_1.text = "Gafas";
        Pantalla_Ordenador_1.text += "\nRotacion Ojo Derecho" + RotacionOjoD.ToString();
        Pantalla_Ordenador_1.text += "\nRotacion Ojo Izquierdo" + RotacionOjoI.ToString();
        Pantalla_Ordenador_1.text += "\n Rotacion Gafas : " + RotacionGafas.ToString();
    }

    public void MuestraPantallaOrdenador_2()
    {
        Pantalla_Ordenador_2.text = "Mandos";
        Pantalla_Ordenador_2.text += "\nGrip Button" + GripButton.ToString();
        Pantalla_Ordenador_2.text += "\nGrip Sensi" + GripSensi.ToString();
    }

    public void MuestraPantallaOrdenador_3()
    {
        Pantalla_Ordenador_3.text = "Mandos";
        Pantalla_Ordenador_3.text += "\nSecondary Touch" + SecondaryTouch.ToString();
        Pantalla_Ordenador_3.text += "\nSecondaty Button" + secondaryButton.ToString();
    }

    #endregion

    public void ActivarMandoI()
    {
        MandoIActivate = true;
        MandoDActivate= false;
        GafasActivate= false;
    }
    
    public void ActivarMandoD()
    {
        MandoIActivate = false;
        MandoDActivate= true;
        GafasActivate= false;
    }

    public void ActivarGafas()
    {
        MandoIActivate = false;
        MandoDActivate = false;
        GafasActivate = true;
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
}
