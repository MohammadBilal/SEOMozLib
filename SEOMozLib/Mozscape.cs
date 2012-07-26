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

        public string MozAccessId
        {
            get
            {
                return this._mozAccessId;
            } 
            set
            {
                this._mozAccessId = value;
            }
        }

        public string MozSecretKey
        {
            get
            {
                return this._mozAccessId;
            }
            set
            {
                this._mozSecretKey = value;
            }
        }

        public MozAPI MozApiType
        {
            get
            {
                return this._mozType;
            }
            set
            {
                this._mozType = value;
            }
        }

        public string CreateTimeStamp(int intHours)
        {
            var baseTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));

            var expireTime = baseTime.Add(TimeSpan.FromHours(intHours));

            var intTotalTime = expireTime.TotalSeconds;

            var intUnixTimeStamp = (int) intTotalTime;

            return intUnixTimeStamp.ToString(CultureInfo.InvariantCulture);

        }

        public string CreateHashSignature(string strMozAccessId, string strMozSecretKey, string strTimeStamp)
        {
            string token = this.MozAccessId + Environment.NewLine.Replace("\r","") + this.MozSecretKey;

            byte[] salt_binary = SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(token));
            string salt_hex = ByteToString(salt_binary);
            string salt = salt_hex.Substring(0, salt_hex.Length);

            HMACSHA1 hmac_sha1 = new HMACSHA1(Encoding.ASCII.GetBytes(token));
            hmac_sha1.Initialize();

            byte[] private_key_binary = Encoding.ASCII.GetBytes(salt);
            byte[] passkey_binary = hmac_sha1.ComputeHash(private_key_binary, 0, private_key_binary.Length);

            string passkey = Convert.ToBase64String(passkey_binary).Trim();

            return HttpUtility.UrlEncode(passkey);
        }
        public string CreateHashSignature(int intHours =1)
        {
            var asciiEncoding = new ASCIIEncoding();
            var secretKeyBytes = asciiEncoding.GetBytes(this._mozSecretKey);
            var accessBytes = asciiEncoding.GetBytes(this._mozAccessId + Environment.NewLine + CreateTimeStamp(intHours));
            var hmacHash = new HMACSHA1(secretKeyBytes);
            var sigHash = hmacHash.ComputeHash(accessBytes);
            return HttpUtility.UrlEncode(Convert.ToBase64String(sigHash));
        }

        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }

        public string CreateHashSignature(string strMozAccessId, string strMozSecretKey, int intHours = 1)
        {
            var asciiEncoding = new ASCIIEncoding();
            var secretKeyBytes = asciiEncoding.GetBytes(strMozSecretKey);
            var accessBytes = asciiEncoding.GetBytes(strMozAccessId + Environment.NewLine + CreateTimeStamp(intHours));
            var hmacHash = new HMACSHA1(secretKeyBytes);
            var sigHash = hmacHash.ComputeHash(accessBytes);
            return HttpUtility.UrlEncode(Convert.ToBase64String(sigHash));
        }

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
                    //strApiUrl = strBaseUrl + "url-metrics/" + strUrl + "?AccessID=" + this._mozAccessId + "&Expires=" + expireTimeStamp + "&Signature=" + signatureHash;
                    break;

                    case MozAPI.LINK_SCAPE:
                    break;
            }
            return strApiUrl;
        }

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
        
        public MozResults.UrlLMetric GetResultsFiltered(string strRawResults)
        {
            if (string.IsNullOrEmpty(strRawResults)) return null;
            var jSerializer = new JavaScriptSerializer();
            return jSerializer.Deserialize<MozResults.UrlLMetric>(strRawResults);
        }
    }

}