using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

/// <summary>
/// Manages the change of images during the test and captures user interactions.
/// </summary>
public class ChangeImagesAtTime : MonoBehaviour
{
    [SerializeField] public static List<Texture2D> textures;// List of textures used for images.
    [SerializeField] private RawImage imageSlot; // Slot for displaying images.
    [SerializeField] private Image imageFrame; // Frame around the displayed image.
    [SerializeField] private static int numberOfImages = 10; // Number of images to be displayed.
    [SerializeField] private double partRepeatedPercent = 0.4; // Percentage of repeated images.

    private static ChangeImagesAtTime obj; // Reference to the instance of the class.
    //private ImagesShuffle imagShuff = obj.AddComponent<ImagesShuffle>();

    private KeywordRecognizer keywordRecognizer; // Recognizer for voice commands.
    private Dictionary<string, Action> dictionary = new Dictionary<string, Action>(); // Dictionary for voice commands.

    private static ImagesShuffle imagShuff = new ImagesShuffle(); // Instance of the image shuffling class.
    private AnalyseCollectedData ACD = new AnalyseCollectedData(); // Instance of data analysis class.
    public static List<ImageInfo> shownImagesInfo = new List<ImageInfo>(); // List of information about displayed images.
    public static List<Click> clicks = new List<Click>(); // List of user clicks.
    public static List<int> imagesOrder = new List<int>(); // Order of displayed images.
    bool whiteToYellow, nextPictureShown = false;
    bool found = false;
    bool firstClick = false;
    public static bool finished = false; // Indicates whether the test is finished.
    public static bool noClicks = true; // Indicates whether there are no user clicks.
    private SettingsMenager sM; // Instance of settings manager.


    float lerpValue = 0f; // Value used for color interpolation.
    float lerpDuration = 0.5f; // Duration of color interpolation.
    int displayedId = 0;

    private void Awake()
    {
        UpdateTextureList();
        imagShuff = new ImagesShuffle();
        ACD = new AnalyseCollectedData();
        shownImagesInfo = new List<ImageInfo>();
        clicks = new List<Click>();
        imagesOrder = new List<int>();
        whiteToYellow = false;
        nextPictureShown = false;
        found = false;
        firstClick = false;
        finished = false;
        noClicks = true;
        sM = new();
    }
    void Start()
    {
        if (sM.GetBoolPref("voice"))
        {
            dictionary.Add("ok", Ok);
            dictionary.Add("talk", Ok);
            dictionary.Add("yes", Ok);

            keywordRecognizer = new KeywordRecognizer(dictionary.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += OnVoiceRecognized;
            keywordRecognizer.Start();
        }

        if (textures.Count < numberOfImages || textures.Count == 0)
        {
            Debug.Log("The ammount of given images is too small");
        }
        else
        {
            imagesOrder = imagShuff.CreateNewImageList(numberOfImages, partRepeatedPercent);

            string mess = "";
            for (int i = 0; i < imagesOrder.Count; i++)
            {
                mess += " " + imagesOrder[i];
            }
            Debug.Log(mess);

            StartCoroutine(ShowNewImage());
        }
           
    }

    private void OnVoiceRecognized(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        dictionary[speech.text].Invoke();
    }


    private void Ok()
    {
        addNewClick(Time.time);
    }

    void Update()
    {
        //===========================ADD new photo info to the list============================

        found = false;
        if (nextPictureShown)
        {
            addNewImageInfo(Time.time);
        }

        //===========================capture click============================
        if ((Input.GetMouseButtonDown(0)&& sM.GetBoolPref("mouse")) ||(Input.GetKeyDown(KeyCode.Space)&& sM.GetBoolPref("keyboard")))
        {
            whiteToYellow = true;
            addNewClick(Time.time);
        }

        //===========================frame colour whiteToYellowt============================
        if (whiteToYellow)
        { 
            if (lerpValue < 1)
            {
                lerpValue += Time.deltaTime / lerpDuration;
                imageFrame.color = Color.Lerp(Color.yellow, Color.white, lerpValue) ;
            }
            else
            {
                whiteToYellow = false;
                lerpValue = 0f;
            }
        }
    }

    IEnumerator ShowNewImage()
    {
        while (displayedId < imagesOrder.Count)
        {
            //float randTime = UnityEngine.Random.Range(imageChangeMinSpeed, imageChangeMaxSpeed);
            float randTime = 3;
            if (PlayerPrefs.GetFloat("min-time-between-images")<=1 || PlayerPrefs.GetFloat("max-time-between-images")<=1) {
                randTime = UnityEngine.Random.Range(4,8);
            }
            else
                randTime = UnityEngine.Random.Range(PlayerPrefs.GetFloat("min-time-between-images"), PlayerPrefs.GetFloat("max-time-between-images"));
                
            randTime = UnityEngine.Random.Range(2, 3);


            nextPictureShown = true;
            //Debug.Log("display picture: " + imagesOrder[displayedId]);
            imageSlot.texture = textures[imagesOrder[displayedId]];

            yield return new WaitForSeconds(randTime);
           
            displayedId++;
            firstClick = true;
        }

        nextPictureShown = true;
        yield return new WaitForSeconds(1);
        ACD.AnalyseData(shownImagesInfo,imagesOrder,clicks);
        yield return new WaitForSeconds(1);
        if (clicks.Count==0)
        {
            noClicks = true;
        }
        finished = true;
    }

    void addNewImageInfo(float time)
    {
        ImageInfo newImage = new ImageInfo();
        
        if (displayedId == 0)
        {
            newImage.id = imagesOrder[displayedId];
            newImage.timeOfAppearence.Add(time);
            newImage.usedTexture = textures[imagesOrder[displayedId]];
            newImage.displayId.Add(0);

            shownImagesInfo.Add(newImage);
        }

        else if (displayedId == imagesOrder.Count)
        {
            foreach (ImageInfo info in shownImagesInfo.ToList())
            {
                
                if (info.id == imagesOrder[displayedId - 1])
                {
                    info.timeOfDisappearence.Add(time);
                }
            }
        }

        else
        {
            found = false;
            foreach (ImageInfo info in shownImagesInfo.ToList())
            {
                //Add past image time change
                if (info.id == imagesOrder[displayedId - 1])
                {
                    info.timeOfDisappearence.Add(time);
                }

                if (info.id == imagesOrder[displayedId])
                {
                    info.timeOfAppearence.Add(time);
                    info.displayId.Add(displayedId);
                    found = true;
                }
            }
            if(!found)
            {
                newImage.id = imagesOrder[displayedId];
                newImage.timeOfAppearence.Add(time);
                newImage.usedTexture = textures[imagesOrder[displayedId]];
                newImage.displayId.Add(displayedId);

                shownImagesInfo.Add(newImage);
            }
        }
        nextPictureShown = false;
    }

    void addNewClick(float time)
    {
        Click click = new();
        ImageInfo info = new ImageInfo();
        if (displayedId < imagesOrder.Count)
        {
            if (firstClick)
            {
                click.firstClick = true;
                firstClick = false;
            }
            else
                click.firstClick = false;

            click.connectedImageInfo = info.GetImageInfoById(shownImagesInfo, imagesOrder[displayedId]);

            click.orderInAppearence = click.connectedImageInfo.timeOfAppearence.Count-1;

            click.timestamp = time;
            
            click.displayedId = displayedId;

            for(int i=0; i<displayedId; i++)
            {
                if (imagesOrder[i] == imagesOrder[displayedId])
                {
                    click.wasCorrect = true;
                    break;
                }
                else
                    click.wasCorrect = false;
            }
            
            clicks.Add(click);
        }
    }
    public void UpdateTextureList()
    {
        FileDownloader fd = new();
        string folderName = PlayerPrefs.GetString("images-folder-name");

        textures = fd.LoadTexturesFromFolder(folderName);

        numberOfImages = PlayerPrefs.GetInt("images-number");
        //imageChangeMaxSpeed = 2*PlayerPrefs.GetFloat("max-time-between-images");
        //imageChangeMaxSpeed = 4;
        //Debug.Log("Displayed max time: " + imageChangeMaxSpeed);
        //imageChangeMinSpeed = 2*PlayerPrefs.GetFloat("min-time-between-images");
        //imageChangeMaxSpeed = 3;
        //Debug.Log("Displayed min time: " + imageChangeMinSpeed);
        partRepeatedPercent = PlayerPrefs.GetFloat("part-repeated-percent");

    }
}
