using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class firstPlanetData {
    public const int numPlanets = 1;

    public const double taxRate = 0.0001;

    //Planet Population Constants 
    public const int startingPopulation = 1000000;
    public const long populationCapacity = 1000000000000;

    //modifier for pop gain when population increase button is added
    public const double PopIncreaseModifier = 100000;
    //money cost for pop gain when population increase button is pushed
    public const double PopIncreaseCostBase = 1000;
    //threshold for pop base necessary to press gain pop button
    public const double PopIncreaseThreshold = 100000;
    //modifier for pop growth on updates
    public const double PopGrowthRate = 0.0003;
    //money cost for colonization of new planet
    public const double ColonizeMoneyCost = 10000;
    //pop cost for colonization of new planet
    public const double ColonizePopCost = 10000;
}


public static class planetData {
    public const int numPlanets = 7;

    public const double taxRate = 0.000;

    //Planet Population Constants 
    public const int startingPopulation = 1000000;
    public const long populationCapacity = 6000000000000;

    //modifier for pop gain when population increase button is added
    public const double PopIncreaseModifier = 100000;
    //money cost for pop gain when population increase button is pushed
    public const double PopIncreaseCostBase = 1000;
    //threshold for pop base necessary to press gain pop button
    public const double PopIncreaseThreshold = 100000;
    //modifier for pop growth on updates
    public const double PopGrowthRate = 0.0003;
    //money cost for colonization of new planet
    public const double ColonizeMoneyCost = 10000;
    //pop cost for colonization of new planet
    public const double ColonizePopCost = 10000;
}
