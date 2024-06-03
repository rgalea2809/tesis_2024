using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameEndManager : MonoBehaviour
{
    // Properties
    public int imageWidth = 2550;
    public int imageHeight = 2550;

    // References
    [SerializeField] private UIXRControler uiXRController;

    [SerializeField] private SpacingValidation logicHelper;

    // Main Camera
    public Camera mainCamera = null;

    // Bedroom sockets
    public XRSocketInteractor bedSocket = null;
    public XRSocketInteractor deskSocket = null;
    public XRSocketInteractor closetSocket = null;
    public XRSocketInteractor nightTableSocket = null;
    public XRSocketInteractor chairSocket = null;
    // Living room sockets
    public XRSocketInteractor oneSeatSofaSocket = null;
    public XRSocketInteractor twoSeatSofaSocket = null;
    public XRSocketInteractor threeSeatSofaSocket = null;
    public XRSocketInteractor tvBaseSocket = null;
    public XRSocketInteractor bookShelfSocket = null;
    public XRSocketInteractor tableSocket = null;

    // Bedroom confetti
    public ParticleSystem bedroomConfetti = null;

    // Living room confetti
    public ParticleSystem livingRoomConfetti = null;

    // Bedroom camera
    public Camera bedroomCamera = null;

    // Living room camera
    public Camera livingRoomCamera = null;

    bool canEndGame = false;


    // Start is called before the first frame update
    void Start()
    {
        bedroomConfetti.gameObject.SetActive(false);
        livingRoomConfetti.gameObject.SetActive(false);

        bedroomCamera.enabled = false;
        livingRoomCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Handlers
    public void RequestGameEnd()
    {
        if (uiXRController.didSelectFreeMode)
        {
            if (logicHelper.getIsOnValidDistance())
                HandleGameEnd();
            else
                ShowSocketsNotFilledError();
        }
        else
        {
            // First check if it can end (Has placed all of the sockets)
            if (HasFilledSockets())
            {
                HandleGameEnd();
            }
            else
            {
                ShowSocketsNotFilledError();
            }


        }
    }

    private void HandleGameEnd()
    {
        if (uiXRController.didSelectLivingRoomType)
        {
            // Living room
            // Show living room confetti
            livingRoomConfetti.gameObject.SetActive(true);
        }
        else
        {
            // Bedroom
            // Show Bedroom confetti
            bedroomConfetti.gameObject.SetActive(true);
        }

        // Show photo prompt
        ShowPhotoPrompt();
    }

    private bool HasFilledSockets()
    {
        if (uiXRController.didSelectLivingRoomType)
        {
            // Living room
            return oneSeatSofaSocket.hasSelection &&
            twoSeatSofaSocket.hasSelection &&
            threeSeatSofaSocket.hasSelection &&
            tableSocket.hasSelection &&
            bookShelfSocket.hasSelection &&
            tvBaseSocket.hasSelection;
        }
        else
        {
            // Bedroom
            return bedSocket.hasSelection &&
            nightTableSocket.hasSelection &&
            chairSocket.hasSelection &&
            deskSocket.hasSelection &&
            closetSocket.hasSelection;
        }
    }

    private void ShowSocketsNotFilledError()
    {
        uiXRController.ToggleSocketsNotFilled(true);
    }

    private void ShowPhotoPrompt()
    {
        uiXRController.ToggleGameEndMenu(true);
    }

    public void TakePhoto()
    {
        if (uiXRController.didSelectLivingRoomType)
        {
            // Living room
            // Show living room confetti
            livingRoomConfetti.gameObject.SetActive(false);
            PhotoProcess(livingRoomCamera);
        }
        else
        {
            // Bedroom
            // Show Bedroom confetti
            bedroomConfetti.gameObject.SetActive(false);
            PhotoProcess(bedroomCamera);
        }
    }

    private void PhotoProcess(Camera camera)
    {
        camera.enabled = true;

        RenderTexture rt = new RenderTexture(imageWidth, imageHeight, 24);
        camera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(imageWidth, imageHeight, TextureFormat.RGB24, false);
        camera.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, imageWidth, imageHeight), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = BuildScreenShotName(imageWidth, imageHeight);
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("Took screenshot to: {0}", filename));

        camera.enabled = false;
    }


    public string BuildScreenShotName(int width, int height)
    {
        String folder = Application.dataPath;

        if (Application.isEditor)
        {
            // put screenshots in folder above asset path so unity doesn't index the files
            var stringPath = folder + "/..";
            folder = Path.GetFullPath(stringPath);
        }

        folder += "/screenshots";

        // make sure directoroy exists
        System.IO.Directory.CreateDirectory(folder);


        return string.Format("{0}/screen_{1}x{2}_{3}.png",
                             folder,
                             width,
                             height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    public void TriggerExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void stopConfettie(){
        livingRoomConfetti.gameObject.SetActive(false);
        bedroomConfetti.gameObject.SetActive(false);
    }
}
