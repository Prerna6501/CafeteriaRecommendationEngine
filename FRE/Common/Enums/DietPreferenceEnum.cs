using System.ComponentModel;

namespace Common.Enums
{
    public enum DietPreferenceEnum
    {
        [Description("Vegetarian")]
        Vegetarian = 1,
        [Description("Non-Vegetarian")]
        NonVegetarian,
        [Description("Eggetarian")]
        Eggetarian
    }
}
