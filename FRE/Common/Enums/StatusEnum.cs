using System.ComponentModel;

namespace Common.Enums
{
    public enum StatusEnum
    {
        [Description("Added to discarded list")]
        Discarded = 1,
        [Description("Sent for detailed Feedback")]
        DetailedFeedback,
        [Description("Removed")]
        Removed
    }
}
