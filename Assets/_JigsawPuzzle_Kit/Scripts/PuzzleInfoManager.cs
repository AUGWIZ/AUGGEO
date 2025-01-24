using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleInfoManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private AugWiz_Core _gameCore;
    [SerializeField] private AnimalDetails animalDetails;

    [Header("Image")]
    [SerializeField] private GameObject infoCanvas;
    [SerializeField] private GameObject _mainImageCanvas;
    [SerializeField] private Image _mainImageDisplay;
    [SerializeField] private Image _ARTourBG;
    [SerializeField] private Sprite _defaultImage;
    [SerializeField] private Animator _animator;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI animalName;
    [SerializeField] private TextMeshProUGUI _stickyNote1;
    [SerializeField] private TextMeshProUGUI _stickyNote2;

    private void OnEnable()
    {
        PuzzleButton.OnButtonClicked += OnPuzzleItemButton;
        infoCanvas.SetActive(false);
    }

    private void OnDisable()
    {
        PuzzleButton.OnButtonClicked -= OnPuzzleItemButton;
    }

    private void OnPuzzleItemButton(string buttonName)
    {
        int index = FindIndexByName(buttonName);
        if (index > -1)
        {
           // AssignAnimalNames(index);
        }

        if (CheckNameExistsInGameCore(buttonName))
        {
            Sprite sprite = GetImageFromGameCore(buttonName);
            DisplayImage(sprite);

            StartCoroutine(InvokeTextDisplayDelayAfter(0.4f, true));
        }
    }

    private IEnumerator InvokeTextDisplayDelayAfter(float delay, bool flag)
    {
        yield return new WaitForSeconds(delay);
        _stickyNote1.enabled = flag;
        _stickyNote2.enabled = flag;
    }

    private void DisplayImage(Sprite sprite)
    {
        if (_mainImageDisplay != null)
        {
            _mainImageDisplay.sprite = sprite != null ? sprite : _defaultImage;
        }
    }

    private void AssignAnimalNames(int index)
    {
        var animal = animalDetails.animalInfoList.animals[index];
        animalName.text = animal.name;

        _stickyNote1.text = $"{animal.stickyNote1.intro}\n\n<b>Appearance:</b> {animal.stickyNote1.appearance}\n\n<b>Habitat:</b> {animal.stickyNote1.habitat}\n\n<b>Movement:</b> {animal.stickyNote1.movement}\n\n<b>Diet:</b> {animal.stickyNote1.diet}";

        _stickyNote2.text = $"<b>Reproduction:</b> {animal.stickyNote2.reproduction}\n\n<b>Social Structure:</b> {animal.stickyNote2.social_structure}\n\n<b>Communication Skills:</b> {animal.stickyNote2.communication}\n\n<b>Survival Skill:</b> {animal.stickyNote2.survival_skill}";

        infoCanvas.SetActive(true);
        _animator.Play("Info BG");
    }
    
    private void AssignBuildingNames(int index)
    {
        var buildings = animalDetails.buildingInfoList.buildings[index];
        animalName.text = buildings.name;

        _stickyNote1.text = $"{buildings.stickyNote1.intro}\n\n<b>Appearance:</b> {buildings.stickyNote1.appearance}\n\n<b>Location:</b> {buildings.stickyNote1.location}\n\n<b>Events:</b> {buildings.stickyNote1.events}\n";

        _stickyNote2.text = $"<b>Construction:</b> {buildings.stickyNote2.construction}\n\n<b>Maintenance:</b> {buildings.stickyNote2.maintenance}\n\n<b>Sustainablity:</b> {buildings.stickyNote2.sustainability}\n";

        infoCanvas.SetActive(true);
        _animator.Play("Info BG");
    }

    private int FindIndexByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return -1; // early exit for invalid input
        }

        string processedName = RemovePrefix(name);

        if (animalDetails?.animalInfoList?.animals == null || animalDetails?.buildingInfoList?.buildings == null)
        {
            return -1; // early exit if lists are not available
        }

        // Search in animal list
        for (int i = 0; i < animalDetails.animalInfoList.animals.Count; i++)
        {
            string animalName = animalDetails.animalInfoList.animals[i].name.Replace(" ", "");

            if (animalName.Equals(processedName, StringComparison.OrdinalIgnoreCase))
            {
                AssignAnimalNames(i);
                return i;
            }
        }

        // Search in building list
        for (int i = 0; i < animalDetails.buildingInfoList.buildings.Count; i++)
        {
            string buildingName = animalDetails.buildingInfoList.buildings[i].name.Replace(" ", "");

            if (buildingName.Equals(processedName, StringComparison.OrdinalIgnoreCase))
            {
                AssignBuildingNames(i);
                return i;
            }
        }

        return -1;
    }


    private string RemovePrefix(string name)
    {
        string[] prefixes = { "Africa", "Antarctica", "Asia", "NA", "SA", "Australia", "Europe" };

        foreach (string prefix in prefixes)
        {
            if (name.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                AssignBGImageFromName(prefix + ("BG"));
                return name.Substring(prefix.Length);
            }
        }

        return name;
    }

    private bool CheckNameExistsInGameCore(string flagName)
    {
        flagName = flagName.Replace(" ", "");

        foreach (var image in _gameCore.arTourImages)
        {
            if (image.name.Contains(flagName))
            {
                AssignBGImageFromName(image.name);
                return true;
            }
        }

        return false;
    }

    private Sprite GetImageFromGameCore(string flagName)
    {
        flagName = flagName.Replace(" ", "");

        foreach (var image in _gameCore.arTourImages)
        {
            if (image.name.Contains(flagName))
            {
                AssignBGImageFromName(image.name);
                return image;
            }
        }

        Debug.Log($"No match found for flagName: {flagName}");
        return null;
    }

    private void AssignBGImageFromName(string imageName)
    {
        foreach (var bgImage in _gameCore.arTourBG)
        {
            if (bgImage.name.Contains(imageName))
            {
                DisplayBGImage(bgImage);
                return;
            }
        }

        Debug.Log($"No matching background image found for imageName: {imageName}");
    }

    private void DisplayBGImage(Sprite bgImage)
    {
        if (_ARTourBG != null)
        {
            _ARTourBG.sprite = bgImage != null ? bgImage : _defaultImage;
        }
        else
        {
            Debug.LogWarning("ARTourBG Image component is not assigned!");
        }
    }

    public void CloseInfoCanvas()
    {
        infoCanvas.SetActive(!infoCanvas.activeInHierarchy);
        StartCoroutine(InvokeTextDisplayDelayAfter(0f, false));
    }
}
