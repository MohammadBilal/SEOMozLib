using System;

namespace SEOMozLib.Classes
{
    public class MozResults
    {
        [Serializable]
        public class UrlLMetric
        {
            public string feid { get; set; }
            public string fmrp { get; set; }
            public string ujid { get; set; }
            public string fmrr { get; set; }
            public string frid { get; set; }
            public string pda { get; set; }
            public string prid { get; set; }
            public string ueid { get; set; }
            public string ufq { get; set; }
            public string uipl { get; set; }
            public string umrp { get; set; }
            public string umrr { get; set; }
            public string upa { get; set; }
            public string upl { get; set; }
            public string us { get; set; }
            public string ut { get; set; }
            public string uu { get; set; }
            public string pid { get; set; }
            public string fid { get; set; }
            public string fipl { get; set; }
            public string puid { get; set; }
            public string uid { get; set; }
            public string utrp { get; set; }
        }

        [Serializable]
        public class Linkscape
        {
            public string lf { get; set; }
            public string lt { get; set; }
            public string lnt { get; set; }
            public string lmrp {get;set;}
            public string lmrr { get; set; }
            public string fmrp { get; set; }
            public string fmrr { get; set; }
            public string ut { get; set; }
            public string uu { get; set; }
            public string pid { get; set; }
            public string fid { get; set; }
            public string fipl { get; set; }
            public string puid { get; set; }
            public string uid { get; set; }
            public string utrp { get; set; }
            public string frid { get; set; }
            public string lrid { get; set;}
            public string lsrc { get; set;}
            public string umrr { get; set;}
            public string upa { get; set;}
            public string upda { get; set;}
            public string pda { get; set; }
            public string upl { get; set; }
            public string ufq { get; set; }
            public string luuu { get; set; }
            public string lufmrr { get; set;}
            public string ltgt { get; set; }
            public string luut { get; set; }
            public string lupl { get; set; }
            public string lufrid { get; set; }
        }

        [Serializable]
        public class AnchorText
        {
            //phrase_to_domain: app
            public string appt { get; set; }
            public string appiu { get; set; }
            public string appif { get; set; }
            public string appeu { get; set; }
            public string appef { get; set; }
            public string appep { get; set; }
            public string appimp { get; set; }
            public string appemp { get; set; }
            public string appf { get; set; }

            //phrase_to_subdomain: apf
            public string apft { get; set; }
            public string apfiu { get; set; }
            public string apfif { get; set; }
            public string apfeu { get; set; }
            public string apfef { get; set; }
            public string apfep { get; set; }
            public string apfimp { get; set; }
            public string apfemp { get; set; }
            public string apff { get; set; }
        }
    }
}
