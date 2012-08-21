using SEOMozLib.Classes;

namespace SEOMozLib.Interfaces
{
    interface ILinkMetrics
    {
        string AnchorText { get; set; }
        string DomainTitle { get; set; }
        string Domain { get; set; }
        int DomainExternalLinks { get; set; }
        int DomainQualityLinks { get; set; }
        string DomainLinkUrl { get; set; }
        int PageAuthority { get; set; }
        int DomainMozRank { get; set; }
        void Transform(MozResults.Linkscape metricObj);
    }
}
