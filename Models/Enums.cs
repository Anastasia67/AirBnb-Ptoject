using System;

namespace AirBnb.Models
{
    [Flags]
    public enum Features
    {
        Smoking = 1,
        PetsAllowed = 2,
        Wifi = 4,
        TV = 8,
        Bath = 16,
        Breakfast = 32
    }

    public enum LocationType
    {
        Apartment,
        Cottage,
        Chalet,
        Room,
        Hotel,
        House
    }
}