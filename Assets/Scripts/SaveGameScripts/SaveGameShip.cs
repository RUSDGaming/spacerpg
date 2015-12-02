using System;
using System.Collections;

[Serializable]
public class SaveGameShip : SaveGame {


    public int shipId;
    public SaveGameItem[] WeaponSlots;
    public SaveGameItem[] cargo;
	
}
