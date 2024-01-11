using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Klasa przechowująca informacje o obrazie.
public class ImageInfo
{
    // Unikalny identyfikator obrazu.
    public int id = -1;
    // Tekstura używana w obrazie.
    public Texture usedTexture;
    // Lista identyfikatorów wyświetlań, w których pojawił się obraz.
    public List<int> displayId = new List<int>();
    // Lista czasów pojawienia się obrazu w poszczególnych wyświetlaniach.
    public List<float> timeOfAppearence = new List<float>();
    // Lista czasów zniknięcia obrazu z poszczególnych wyświetlaniach.
    public List<float> timeOfDisappearence = new List<float>();


    // Metoda statyczna do pobierania informacji o obrazie na podstawie id.
    public ImageInfo GetImageInfoById(List<ImageInfo> list, int id)
    {
        ImageInfo info = new ImageInfo();

        foreach (ImageInfo item in list)
        {
            // Sprawdzenie, czy identyfikator obrazu zgadza się z żądanym id.
            if (item.id == id)
            {
                return item;
            }

        }
        // Zwrócenie pustej instancji ImageInfo, jeśli nie znaleziono pasującego id.
        return info;
    }

    // Metoda statyczna do pobierania informacji o obrazie na podstawie displayId.
    public ImageInfo GetImageInfoByDisplayId(List<ImageInfo> list, int displayId)
    {
        ImageInfo info = new ImageInfo();

        foreach (ImageInfo item in list)
        {
            foreach (int id in item.displayId)
            {
                // Sprawdzenie, czy lista DisplayIds zawiera żądane displayId.
                if (id == displayId)
                {
                    return item;
                }
            }
        }
        // Zwrócenie pustej instancji ImageInfo, jeśli nie znaleziono pasującego displayId.
        return info;
    }

}
