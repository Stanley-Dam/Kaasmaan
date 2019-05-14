using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingTypes {

    public static readonly BuildingTypes NONE = new BuildingTypes(0, "", "");
    public static readonly BuildingTypes FACTORY = new BuildingTypes(1, "Sprites/Buildings/Factory/", "factory_level_");
    public static readonly BuildingTypes HOUSING = new BuildingTypes(2, "Sprites/Buildings/Housing/", "housing_level_");
    public static readonly BuildingTypes MEADOW = new BuildingTypes(3, "Sprites/Buildings/Meadow/", "meadow_level_");
    public static readonly BuildingTypes EXPIRIMENTAL = new BuildingTypes(4, "Sprites/Buildings/Expirimental/", "expirimental_level_");

    private int typeID;
    private string spritesPath;
    private string spritesName;

    BuildingTypes(int typeID, string spritesPath, string spritesName) => (this.typeID, this.spritesPath, this.spritesName) = (typeID, spritesPath, spritesName);

    public static IEnumerable<BuildingTypes> Values {
        get {
            yield return NONE;
            yield return FACTORY;
            yield return HOUSING;
            yield return MEADOW;
            yield return EXPIRIMENTAL;
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

}
