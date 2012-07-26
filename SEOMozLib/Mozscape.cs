using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Text;
using System.Security.Cryptography;
using SEOMozLib.Classes;

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
            this._mozType = MozAPI.URL_METRICS;            
        }

        public Mozscape(string strMozAccessId, string strMozSecretKey, MozAPI mozType)
        {
            this._mozAccessId = strMozAccessId;
            this._mozSecretKey = strMozSecretKey;
            this._mozType = mozType;
        }

        /// <summary>
        /// Gets or sets the MozAccessId
        /// </summary>
        /// <value>The MozAccessId</value>
        public string MozAccessId
        {
            get { return this._mozAccessId; } 
            set { this._mozAccessId = value; }
        }

        /// <summary>
        /// Gets or sets the MozSecretKey
        /// </summary>
        /// <value>The MozSecretKey</value>
        public string MozSecretKey
        {
            get { return this._mozSecretKey; }
            set { this._mozSecretKey = value; }
        }

        /// <summary>
        /// Gets or sets the API Type
        /// </summary>
        /// <value>The Moz Api Type To Use</value>
        public MozAPI MozApiType
        {
            get { return this._mozType; }
            set { this._mozType = value; }
        }

        /// <summary>
        /// Create Unix TimeStamp
        /// </summary>
        /// <param name="intHours">1</param>
        /// <value>Unix TimeStamp </value>
        public string CreateTimeStamp(int intHours = 1)
        {
            var baseTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));

            var expireTime = baseTime.Add(TimeSpan.FromHours(intHours));

            var intTotalTime = expireTime.TotalSeconds;

            var intUnixTimeStamp = (int) intTotalTime;

            return intUnixTimeStamp.ToString(CultureInfo.InvariantCulture);

        }

        /// <summary>
        /// Create Hash Signature for API request.
        /// </summary>
        /// <param name="strMozAccessId"></param>
        /// <param name="strMozSecretKey"></param>
        /// <param name="strTimeStamp"></param>
        /// <value>String</value>
        public string CreateHashSignature(string strMozAccessId, string strMozSecretKey, string strTimeStamp)
        {
            string token = strMozAccessId + Environment.NewLine.Replace("\r", "") + strTimeStamp;

            using (var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(strMozSecretKey),true))
            {
                var hash = hmac.ComputeHash(Encoding.ASCII.GetBytes(token));
                var hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
                return HttpUtility.UrlEncode(Convert.ToBase64String(hash));
            }
        }

        /// <summary>
        /// Create Hash Signature for API request.
        /// </summary>
        /// <param name="intHours"></param>
        /// <value>String</value>
        public string CreateHashSignature(int intHours = 1)
        {
            string token = this._mozAccessId + Environment.NewLine.Replace("\r", "") + CreateTimeStamp(intHours);

            using (var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(this._mozSecretKey), true))
            {
                var hash = hmac.ComputeHash(Encoding.ASCII.GetBytes(token));
                var hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
                return HttpUtility.UrlEncode(Convert.ToBase64String(hash));
            }
        }

        /// <summary>
        /// Create the url for the api request
        /// </summary>
        /// <param name="apiType"></param>
        /// <param name="intHours"></param>
        /// <param name="strUrl"></param>
        /// <value>String</value>
        public string CreateMozAPIUrl(string strUrl, MozAPI apiType = MozAPI.URL_METRICS, int intHours = 1)
        {
            if (string.IsNullOrEmpty(strUrl)) return null;

            var strApiUrl = string.Empty;
            var expireTimeStamp = CreateTimeStamp(intHours);
            var signatureHash = CreateHashSignature(intHours);

            switch (apiType)
            {
                case MozAPI.URL_METRICS:
                    strApiUrl = String.Format("http://lsapi.seomoz.com/linkscape/url-metrics/{0}?AccessID={1}&Expires={2}&Signature={3}", strUrl, this._mozAccessId, expireTimeStamp, signatureHash);                    
                    break;

                    case MozAPI.LINK_SCAPE:
                    break;
            }
            return strApiUrl;
        }
        
        /// <summary>
        /// Get Raw String Result of API Url
        /// </summary>
        /// <param name="strUrl">Mozscape API Url</param>
        /// <value>String</value>
        public string GetRawResults(string strUrl)
        {
            if (string.IsNullOrEmpty(strUrl)) return null;

            var request = WebRequest.Create(strUrl);

            try
            {
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
            catch(System.Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get UrlMetrics Object from API Url
        /// </summary>
        /// <param name="strUrl">Mozscape API Url</param>
        /// <returns>UrlMetrics</returns>
        public UrlMetrics GetUrlMetrics(string strUrl)
        {
            if (string.IsNullOrEmpty(strUrl)) return null;
            var strRawResults = GetRawResults(strUrl);
            var jSON = new JavaScriptSerializer();
            var urlMetrics = new UrlMetrics();
            urlMetrics.Transform(jSON.Deserialize<MozResults.UrlLMetric>(strRawResults));
            return urlMetrics;
        }

    }

}