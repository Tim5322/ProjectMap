using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace ProjectMap.WebApi.Models;

public class Object2D
{
    public Guid Id { get; set; }
    public string? PrefabId { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public int ScaleX { get; set; }
    public int ScaleY { get; set; }
    public int Rotation { get; set; }
    public int SortingLayer { get; set; }
}

