using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentData", menuName = "Scriptable Objects/EquipmentData")]
public class EquipmentData : ScriptableObject
{
    public int equipmentID;
    public string equipmentName;
    public string equipmentDescription;
    public int equipmentPrice;
    public Sprite equipmentIcon;
}
