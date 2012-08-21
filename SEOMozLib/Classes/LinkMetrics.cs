using SEOMozLib.Interfaces;

namespace SEOMozLib.Classes
{
    public class LinkMetrics : ILinkMetrics
    {
        public string Flags { get; set; }
        public string AnchorText { get; set; }
        public string NormalizedAnchorText { get; set; }
        public int mozRankPassed { get; set; }
        public int mozRankPassedRaw { get; set; }
        public void Transform(MozResults.Linkscape metricObj)
        {
            return;
        }
    }
}
