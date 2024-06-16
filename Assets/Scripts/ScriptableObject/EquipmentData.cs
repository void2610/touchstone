using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentData", menuName = "Scriptable Objects/EquipmentData")]
public class EquipmentData : ScriptableObject
{
    public string equipmentName;
    public string equipmentDescription;
    public Sprite equipmentIcon;
}
