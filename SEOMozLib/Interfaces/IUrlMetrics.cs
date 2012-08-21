using SEOMozLib.Classes;

namespace SEOMozLib.Interfaces
{
    interface IUrlMetrics
    {
        string Title { get; set; }
        string URL { get; set; }
        string Subdomain { get; set; }
        string RootDomain { get; set; }
        int ExternalLinks { get; set; }
        int SubdomainExternalLinks { get; set; }
        int RootDomainExternalLinks { get; set; }
        int JuicePassingLinks { get; set; }
        int SubdomainsLinking { get; set; }
        int RootDomainsLinking { get; set; }
        int Links { get; set; }
        int SubdomainSubdomainLinking { get; set; }
        int RootDomainRootDomainLinking { get; set; }
        int MozRank { get; set; }
        int SubdomainMozRank { get; set; }
        int RootDomainMozRank { get; set; }
        int MozTrust { get; set; }
        int SubdomainMozTrust { get; set; }
        int RootDomainMozTrust { get; set; }
        int ExternalMozRank { get; set; }
        int SubdomainExternaDomainJuice { get; set; }
        int RootDomainExternalDomainJuice { get; set; }
        int SubdomainDomainJuice { get; set; }
        int RootDomainDomainJuice { get; set; }
        string HTTPStatus { get; set; }
        int LinksToSubdomain { get; set; }
        int LinksToRootDomain { get; set; }
        int RootDomainsLinkingToSubDomains { get; set; }
        int PageAuthority { get; set; }
        int DomainAuthority { get; set; }
        void Transform(MozResults.UrlLMetric metricObj);
    }
}
