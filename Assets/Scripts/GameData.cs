using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class generalData
{
    public const int numPlanets = 8;
}

public class firstPlanetData {
    public const double taxRate = 0.0001;

    //Planet Population Constants 
    public const int startingPopulation = 1000000;
    public const long populationCapacity = 1000000000000;

    //modifier for pop gain when population increase button is added
    public const double popIncreaseModifier = 100000;
    //money cost for pop gain when population increase button is pushed
    public const double popIncreaseCostBase = 1000;
    //threshold for pop base necessary to press gain pop button
    public const double popIncreaseThreshold = 100000;
    //modifier for pop growth on updates
    public const double popGrowthRate = 0.0003;
    //money cost for colonization of new planet
    public const double colonizeMoneyCost = 10000;
    //pop cost for colonization of new planet
    public const double colonizePopCost = 10000;
}


public class planetData {
    public const double taxRate = 0.000;

    //Planet Population Constants 
    public const int startingPopulation = 0;
    public const long populationCapacity = 6000000000000;
    
    //population addition for each planet click
    public const double popClick = 100;
    //modifier for pop gain when population increase button is added
    public const double popIncreaseModifier = 100000;
    //money cost for pop gain when population increase button is pushed
    public const double popIncreaseCostBase = 1000;
    //threshold for pop base necessary to press gain pop button
    public const double popIncreaseThreshold = 100;
    //modifier for pop growth on updates
    public const double popGrowthRate = 0.0003;
    //money cost for colonization of new planet
    public const double colonizeMoneyCost = 1000;
    //pop cost for colonization of new planet
    public const double colonizePopCost = 100;
    //Scale by which popINcreaseCost increases at each colonize
    public const double popIncreaseCostScale = 1.05;
    //Scale by which the colonization cost ncreases
    public const double colonizeMoneyCostScale = 1.1;
    
}
