using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class generalData
{
    public const int numPlanetsMax = 5;
    
    //population number when new planet can be spawned
    public const double planetSpawnThreshold = 100;
    //scale for newPlanetPopThreshold
    public const double planetSpawnThresholdScale = 10;

    public const double upgradeClickCostScale = 2;
    public const double popClickScale = 10;
}

public class firstPlanetData {
    public const double productivity = 0.5;

    //Planet Population Constants 
    public const int startingPopulation = 0;
    public const long populationCapacity = 1000000000000;

    //modifier for pop gain when population increase button is added
    public const double popIncreaseModifier = 100000;
    //money cost for pop gain when population increase button is pushed
    public const double popIncreaseCostBase = 1000;
    //threshold for pop base necessary to press gain pop button
    public const double popIncreaseThreshold = 100000;
    //modifier for pop growth on updates
    public const double popGrowthRate = 0;
    //money cost for colonization of new planet
    public const double colonizeMoneyCost = 10000;
    //pop cost for colonization of new planet
    public const double colonizePopCost = 10000;
}


public class planetData
{
    public const double productivity = 0.5;
    
    //Planet Population Constants 
    public const int startingPopulation = 0;
    public const long populationCapacity = 6000000000000;
    
    //population addition for each planet click
    //public const double popClick = 1;
    //population number when new planet can be spawned
    public const double newPlanetPopThreshold = 100;
    //scale for newPlanetPopThreshold
    public const double newPlanetPopScale = 5;
    
    // Scale by which popIncreaseCost increases
    public const double popIncreaseCostScale = 1.05;
    
    //modifier for pop gain when population increase button is added
    public const double popIncreaseModifier = 100000;
    //money cost for pop gain when population increase button is pushed
    public const double popIncreaseCostBase = 1000;
    //threshold for pop base necessary to press gain pop button
    public const double popIncreaseThreshold = 100;
    //modifier for pop growth on updates
    public const double popGrowthRate = 0;
    //money cost for colonization of new planet
    public const double colonizeMoneyCost = 1000;
    //pop cost for colonization of new planet
    public const double colonizePopCost = 100;
    //Scale by which the colonization cost ncreases
    public const double colonizeMoneyCostScale = 1.1;



    //Planet Scaling on Hover 
    public const float scalePlanetHover = 1.4f;
    public const float scalePlanetClick = 1.2f;

    //Label stuff
    public static float[] labelOffset = new float[2] {0f, .5f};
    //Badge stuff 
    public static float[] prodBadgeOffset = new float[2] {.3f, .3f};

    public const double popAutoGrowthCostBase = 2;
    public const double popAutoGrowthCostScale = 2;

    public const double productivityGrowthCost = 2;
    public const double productivityGrowthCostScale = 4;
    public const double productivityUpgradeScale = 2;
    public const double planetStartCryptoScale = 10;


    public const double planetSciencePopCost = 150;
    public const double planetSciencePopCostScale = 5;
}
