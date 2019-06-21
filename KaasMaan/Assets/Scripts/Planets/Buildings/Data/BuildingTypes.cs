using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTypes {

    public static readonly BuildingTypes NONE = new BuildingTypes(0, "", "", 0, 0, 0);
    public static readonly BuildingTypes FACTORY = new BuildingTypes(1, "Sprites/Buildings/Factory/", "factory_level_", 1, 150, 8);
    public static readonly BuildingTypes HOUSING = new BuildingTypes(2, "Sprites/Buildings/Housing/", "housing_level_", 1, 50, 8);
    public static readonly BuildingTypes MEADOW = new BuildingTypes(3, "Sprites/Buildings/Meadow/", "meadow_level_", 1, 100, 8);
    public static readonly BuildingTypes RESEARCH = new BuildingTypes(4, "Sprites/Buildings/Research/", "research_level_", 1, 300, 8);
    public static readonly BuildingTypes MARKET = new BuildingTypes(5, "Sprites/Buildings/MarketPlace/", "marketplace_level_", 1, 100, 8);

    private int typeID;
    private string spritesPath;
    private string spritesName;
    private int spriteUpgradeDiv;
    private float buildingCost;
    private int maxLevel;

    BuildingTypes
        (int typeID, string spritesPath, string spritesName, int spriteUpgradeDiv, float buildingCost, int maxLevel) 
        => 
        (this.typeID, this.spritesPath, this.spritesName, this.spriteUpgradeDiv, this.buildingCost, this.maxLevel) 
        = 
        (typeID, spritesPath, spritesName, spriteUpgradeDiv, buildingCost, maxLevel);

    public static IEnumerable<BuildingTypes> Values {
        get {
            yield return NONE;
            yield return FACTORY;
            yield return HOUSING;
            yield return MEADOW;
            yield return RESEARCH;
            yield return MARKET;
        }
    }

    public int getTypeId() {
        return this.typeID;
    }

    public string getSpritePath() {
        return this.spritesPath;
    }

    public string getSpriteName() {
        return this.spritesName;
    }

    public int getSpriteUpgradeDiv() {
        return this.spriteUpgradeDiv;
    }

    public float GetBuildingCost() {
        return this.buildingCost;
    }

    public int GetMaxLevel() {
        return this.maxLevel;
    }

    public static BuildingTypes getBuildingFromTypeID(int typeID) {
        foreach(BuildingTypes currentType in BuildingTypes.Values) {
            if (currentType.getTypeId() == typeID) return currentType;
        }

        return null;
    }

    public static string LevelToStringTranslator(int level) {
        string levelString = "";

        if (level < 10)
            levelString = "0" + level;
        else {
            levelString = "" + level;
        }

        return levelString;
    }

}
