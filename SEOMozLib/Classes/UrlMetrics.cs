using SEOMozLib.Interfaces;

namespace SEOMozLib.Classes
{
    public class UrlMetrics : IUrlMetrics
    {
        #region IUrlMetrics Members

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string URL { get; set; }

        /// <summary>
        /// Gets or sets the subdomain.
        /// </summary>
        /// <value>The subdomain.</value>
        public string Subdomain { get; set; }

        /// <summary>
        /// Gets or sets the root domain.
        /// </summary>
        /// <value>The root domain.</value>
        public string RootDomain { get; set; }

        /// <summary>
        /// Gets or sets the external links.
        /// </summary>
        /// <value>The external links.</value>
        public int ExternalLinks { get; set; }

        /// <summary>
        /// Gets or sets the subdomain external links.
        /// </summary>
        /// <value>The subdomain external links.</value>
        public int SubdomainExternalLinks { get; set; }

        /// <summary>
        /// Gets or sets the root domain external links.
        /// </summary>
        /// <value>The root domain external links.</value>
        public int RootDomainExternalLinks { get; set; }

        /// <summary>
        /// Gets or sets the juice passing links.
        /// </summary>
        /// <value>The juice passing links.</value>
        public int JuicePassingLinks { get; set; }

        /// <summary>
        /// Gets or sets the subdomains linking.
        /// </summary>
        /// <value>The subdomains linking.</value>
        public int SubdomainsLinking { get; set; }

        /// <summary>
        /// Gets or sets the root domains linking.
        /// </summary>
        /// <value>The root domains linking.</value>
        public int RootDomainsLinking { get; set; }

        /// <summary>
        /// Gets or sets the links.
        /// </summary>
        /// <value>The links.</value>
        public int Links { get; set; }

        /// <summary>
        /// Gets or sets the subdomain subdomain linking.
        /// </summary>
        /// <value>The subdomain subdomain linking.</value>
        public int SubdomainSubdomainLinking { get; set; }

        /// <summary>
        /// Gets or sets the root domain root domain linking.
        /// </summary>
        /// <value>The root domain root domain linking.</value>
        public int RootDomainRootDomainLinking { get; set; }

        /// <summary>
        /// Gets or sets the moz rank.
        /// </summary>
        /// <value>The moz rank.</value>
        public int MozRank { get; set; }

        /// <summary>
        /// Gets or sets the subdomain moz rank.
        /// </summary>
        /// <value>The subdomain moz rank.</value>
        public int SubdomainMozRank { get; set; }

        /// <summary>
        /// Gets or sets the root domain moz rank.
        /// </summary>
        /// <value>The root domain moz rank.</value>
        public int RootDomainMozRank { get; set; }

        /// <summary>
        /// Gets or sets the moz trust.
        /// </summary>
        /// <value>The moz trust.</value>
        public int MozTrust { get; set; }

        /// <summary>
        /// Gets or sets the subdomain moz trust.
        /// </summary>
        /// <value>The subdomain moz trust.</value>
        public int SubdomainMozTrust { get; set; }

        /// <summary>
        /// Gets or sets the root domain moz trust.
        /// </summary>
        /// <value>The root domain moz trust.</value>
        public int RootDomainMozTrust { get; set; }

        /// <summary>
        /// Gets or sets the external moz rank.
        /// </summary>
        /// <value>The external moz rank.</value>
        public int ExternalMozRank { get; set; }

        /// <summary>
        /// Gets or sets the subdomain externa domain juice.
        /// </summary>
        /// <value>The subdomain externa domain juice.</value>
        public int SubdomainExternaDomainJuice { get; set; }

        /// <summary>
        /// Gets or sets the root domain external domain juice.
        /// </summary>
        /// <value>The root domain external domain juice.</value>
        public int RootDomainExternalDomainJuice { get; set; }

        /// <summary>
        /// Gets or sets the subdomain domain juice.
        /// </summary>
        /// <value>The subdomain domain juice.</value>
        public int SubdomainDomainJuice { get; set; }

        /// <summary>
        /// Gets or sets the root domain domain juice.
        /// </summary>
        /// <value>The root domain domain juice.</value>
        public int RootDomainDomainJuice { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status.
        /// </summary>
        /// <value>The HTTP status.</value>
        public string HTTPStatus { get; set; }

        /// <summary>
        /// Gets or sets the links to subdomain.
        /// </summary>
        /// <value>The links to subdomain.</value>
        public int LinksToSubdomain { get; set; }

        /// <summary>
        /// Gets or sets the links to root domain.
        /// </summary>
        /// <value>The links to root domain.</value>
        public int LinksToRootDomain { get; set; }

        /// <summary>
        /// Gets or sets the root domains linking to sub domains.
        /// </summary>
        /// <value>The root domains linking to sub domains.</value>
        public int RootDomainsLinkingToSubDomains { get; set; }

        /// <summary>
        /// Gets or sets the page authority.
        /// </summary>
        /// <value>The page authority.</value>
        public int PageAuthority { get; set; }

        /// <summary>
        /// Gets or sets the domain authority.
        /// </summary>
        /// <value>The domain authority.</value>
        public int DomainAuthority { get; set; }

        /// <summary>
        /// Transform and clean up values returned from initial json data
        /// <param name="metricObj">UrlMetric Object returned from results</param>
        /// </summary>
        public void Transform(MozResults.UrlLMetric metricObj)
        {
            if (metricObj != null)
            {
                this.Title = metricObj.ut;
                this.HTTPStatus = metricObj.us;
                this.PageAuthority = intParse(metricObj.upa);
                this.DomainAuthority = intParse(metricObj.pda);
                this.URL = metricObj.uu;
                this.ExternalLinks = intParse(metricObj.ueid);
                this.Links = intParse(metricObj.uid);
                this.MozRank = intParse(metricObj.umrp);
                this.SubdomainMozRank = intParse(metricObj.fmrp);
            }

        }

        private int intParse(string strValue)
        {
            if (string.IsNullOrEmpty(strValue)) return 0;
            var i = 0;
            int.TryParse(strValue,out i);
            return i;
        }
        #endregion
    }
}