using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 Each Planet has negative growth rate
 [Planet upgrades will be purely productivity upgrades and autoclick upgrades]
 The point of spawning new planets is to increase population, but also get more science points.
 After a certain point one planet will not be able to give more science points
 
 Money will be local to each planet.
 
 Upgrades tied to planet, showing max of 3 at a time.
 
 // All planets spawn with More Click Effect, Autoclicker, and Productivity
 
 Planet 0 = [Autoclicker, Productivity, SciencePoints]

 Planet 1 = [Autoclicker, Productivity, SciencePoints]
 
 Planet 2 = [Autoclicker, Productivity, SciencePoints]

 Planet 3 = [Autoclicker, Productivity, SciencePoints]

 Planet 4 = [Autoclicker, Productivity, SciencePoints]
 
 GENERAL UPGRADES:
 Stats: [Science Points] - doesn't increment automatically, needs upgrades from planets
 
 Upgrades: [ReproductionBoosts, SpawnPlanet, DiscoverCloning, Immortality]
 
 // Kinds of upgrades
 PlanetUpgrades - have X world population (ex: SpawnPlanet)
 PopulationUpgrades- Have X science points (ex: ReproductionBoosts, DiscoverCloning)
 Productivity/Decline/Upgrades - Have X science points (ex: Immortality)
 SciencePoints - (ex: invest in science research) - have X world population on Y planets each (gives incentive to click different planets) - gives incentive to increase pop
 	on certain planet

 // Kinds of constraints:
- have X world population on Y planets each (gives incentive to click different planets)
- Have X science points (gives different stat to maximize)


 // ReproductionBoosts will be Quicker Reproduction Time, More Babies Each Birth, etc. => Greater pop growth
 // Immortality is so there is no population decline
 // DiscoverCloning is so each click has *2 population instead of +1. It can then increase to *5, *10, *100.
*/



public class UpgradesManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
