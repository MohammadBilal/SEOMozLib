using SEOMozLib.Interfaces;

namespace SEOMozLib.Classes
{
    public class LinkMetrics : ILinkMetrics
    {
        #region ILinkMetrics Members

        public string AnchorText { get; set; }
        public string Domain { get; set; }
        public string DomainLinkUrl { get; set; }
        public string DomainTitle { get; set; }
        public int DomainExternalLinks { get; set; }
        public int DomainQualityLinks { get; set; }
        public int PageAuthority { get; set; }
        public int DomainMozRank { get; set; }

        public void Transform(MozResults.Linkscape metricObj)
        {
            //TODO: UPDATED TRANSFORM CLASS
            if (metricObj != null)
            {
                this.DomainLinkUrl = metricObj.uu;
                this.DomainTitle = metricObj.ut;
                this.PageAuthority = System.Convert.ToInt32(metricObj.upa);
                this.DomainMozRank = System.Convert.ToInt32(metricObj.fmrp);
                this.DomainExternalLinks = intParse(metricObj.uid);
                this.DomainQualityLinks = intParse(metricObj.ueid);
                this.Domain = metricObj.upl;
                this.AnchorText = metricObj.lt;
            }

        }
        #endregion

        private int intParse(string strValue)
        {
            if (string.IsNullOrEmpty(strValue)) return 0;
            var i = 0;
            int.TryParse(strValue, out i);
            return i;
        }
    }
}
