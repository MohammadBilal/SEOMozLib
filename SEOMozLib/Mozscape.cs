using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Text;
using System.Security.Cryptography;

namespace SEOMozLib
{
    public enum MozAPI
    {
        URL_METRICS,
        LINK_SCAPE
    }

    public class Mozscape
    {
        private string _mozAccessId;
        private string _mozSecretKey;
        private MozAPI _mozType;

        public Mozscape()
        {
            this._mozAccessId = null;
            this._mozSecretKey = null;
            this._mozType = MozAPI.URL_METRICS;
        }

        public Mozscape(string strMozAccessId, string strMozSecretKey)
        {
            this._mozAccessId = strMozAccessId;
            this._mozSecretKey = strMozSecretKey;
        }

        public Mozscape(string strMozAccessId, string strMozSecretKey, MozAPI mozType)
        {
            this._mozAccessId = strMozAccessId;
            this._mozSecretKey = strMozSecretKey;
            this._mozType = mozType;
        }

        public string CreateTimeStamp(int intHours)
        {
            var baseTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));

            var expireTime = baseTime.Add(TimeSpan.FromHours(intHours));

            var intTotalTime = expireTime.TotalSeconds;

            var intUnixTimeStamp = (int) intTotalTime;

            return intUnixTimeStamp.ToString(CultureInfo.InvariantCulture);

        }

        public string CreateAPISignature(string strMozAccessId, string strMozSecretKey)
        {
            return null;
        }

        public string CreateMozAPIUrl(string strUrl, MozAPI apiType, int intHours)
        {
            if (string.IsNullOrEmpty(strUrl)) return null;

            var strApiUrl = string.Empty;
            var expireTimeStamp = CreateTimeStamp(intHours);

            switch (apiType)
            {
                    case MozAPI.URL_METRICS:
                    break;

                    case MozAPI.LINK_SCAPE:
                    break;
            }
            return strApiUrl;
        }

        public string GetResults(string strUrl)
        {
            if (string.IsNullOrEmpty(strUrl)) return null;

            var request = WebRequest.Create(strUrl);

            using (var responseStream = request.GetResponse().GetResponseStream())
            {
                if (responseStream == null) return null;

                var objReader = new StreamReader(responseStream);
                var responseText = new StringBuilder();

                string sLine = string.Empty;
                int i = 0;

                while (sLine != null)
                {
                    i++;
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        responseText.AppendLine(sLine);
                }
                return responseText.ToString();
            }
        }

        public List<T> FormatResults<T>(string strResults)
        {
            return null;
        }
    }

}