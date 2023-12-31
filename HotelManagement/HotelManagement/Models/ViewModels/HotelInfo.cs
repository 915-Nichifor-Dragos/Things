﻿namespace HotelManagement.Models.ViewModels;

public class HotelInfo
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Location { get; set; }

    public bool IsAvailable { get; set; }

    public int NumberOfEmployees { get; set; }
}
