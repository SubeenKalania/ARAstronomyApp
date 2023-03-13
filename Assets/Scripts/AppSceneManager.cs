using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AppSceneManager : MonoBehaviour
{
    public const string mainMenuScene = "MainMenu_UI_Scene";
    public const string celestialObjectScene = "CelestialObject_UI_scene";
    public const string constellationScene = "Constellations_UI_scene";
    public const string planetPageScene = "PlanetsPage_UI_Scene";
    public const string solarSystemScene = "SolarSystemScene";
    public const string ConstellationARScene = "ConstellationARScene";
    public const string StarsinAR = "StarsInARScene";
    public const string MoonsinAR = "MoonsInARScene";

    public void LoadSolarSystemScene()
    {
        SceneManager.LoadScene(solarSystemScene);
    }
    public void LoadMoonsScene()
    {
        SceneManager.LoadScene(MoonsinAR);
    }
    public void LoadStarScene()
    {
        SceneManager.LoadScene(StarsinAR);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void LoadCelestialScene()
    {
        SceneManager.LoadScene(celestialObjectScene);
    }
    public void LoadConstellationsPage()
    {
        SceneManager.LoadScene(constellationScene);
    }

    public void LoadPlanetsPage()
    {
        SceneManager.LoadScene(planetPageScene);
    }
    public void ConstellationARPage(int indexChoice)
    {
        SceneManager.LoadScene(ConstellationARScene);

        //Save the choice!
        PlayerPrefs.SetInt("ConstellationChoice", indexChoice);
    }

    public void QuitApp()
    {
        Application.Quit();
    }


}
