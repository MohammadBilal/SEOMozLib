using SEOMozLib.Classes;

namespace SEOMozLib.Interfaces
{
    interface ILinkMetrics
    {
        string Flags { get; set; }
        string AnchorText { get; set; }
        string NormalizedAnchorText { get; set; }
        int mozRankPassed { get; set; }
        int mozRankPassedRaw { get; set; }
        void Transform(MozResults.Linkscape metricObj);
    }
}
