using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace MadMimi {
    /// <summary>
    /// A simple wrapper for sending MadMimi mailers
    /// </summary>
    public class Api {
        string _apiKey;
        string _userName;
        string _baseUrl = "https://api.madmimi.com/mailer";
        public Api(string userName, string apiKey ) {
            _apiKey = apiKey;
            _userName = userName;
        }
        /// <summary>
        /// Send off a mailer - alson known as a "promotion". The only thing required here is your api credentials, the mailer name and who to send it to
        /// </summary>
        public string SendMailer(string name, string recipient, string subject="", string bcc="", string from="", string replyTo="", object replacements = null) {
            var sb = new StringBuilder();
            sb.AppendFormat("username={0}", _userName);
            sb.AppendFormat("&api_key={0}", _apiKey);
            sb.AppendFormat("&promotion_name={0}", name);
            sb.AppendFormat("&recipient={0}", recipient);
            if(!String.IsNullOrEmpty(subject))  
                sb.AppendFormat("&subject={0}", subject);
            if (!String.IsNullOrEmpty(bcc))
                sb.AppendFormat("&bcc={0}", bcc);
            if (!String.IsNullOrEmpty(from))
                sb.AppendFormat("&from={0}", from);
            if (!String.IsNullOrEmpty(replyTo))
                sb.AppendFormat("&reply_to={0}", replyTo);
            if (!String.IsNullOrEmpty(from))
                sb.AppendFormat("&from={0}", from);
            if (replacements != null) {
                sb.Append("&body=---");
                var props = replacements.GetType().GetProperties();
                foreach (var p in props) {
                    var key = p.Name;
                    var val = p.GetValue(replacements,null);
                    sb.AppendFormat("\n{0}: {1}\n", key,val);
                }

            }

            return ExecuteRequest(_baseUrl, "POST", sb.ToString());

        }
        /// <summary>
        /// The core executor for sending off requests
        /// </summary>
        string ExecuteRequest(string url, string verb, string data) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            //add the form data
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentLength = byteArray.Length;

            using (var stream = request.GetRequestStream()) {
                // Write the data to the request stream.
                stream.Write(byteArray, 0, byteArray.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            string result = "";

            using (Stream stream = response.GetResponseStream()) {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }

    }
}
