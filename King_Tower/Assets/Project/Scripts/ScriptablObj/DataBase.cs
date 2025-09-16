
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataBase", menuName = "Data/DataBase")]
public class DataBase : ScriptableObject
{
    public Player player;

    public List<Room> Rooms;

}
