namespace HotelManagement.BusinessLogic.Converters;

public static class StringToBoolFilterConverter
{
    public static bool ConvertString(this string filter)
    {
        switch (filter)
        {
            case "true":
                return true;

            default:
                return false;
        }
    }
}
