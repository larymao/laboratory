using Lary.Laboratory.Core.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Lary.Laboratory.Core.Models
{
    /// <summary>
    ///     Represents a response message including the status code, message and data.
    /// </summary>
    /// <typeparam name="TResult">
    ///     The type of <see cref="ResponseMessage{TResult}.Data"/>.
    /// </typeparam>
    public class ResponseMessage<TResult>
    {
        /// <summary>
        ///     The status code of the response.
        /// </summary>
        [JsonProperty("code")]
        public int Code { get; set; }

        /// <summary>
        ///     The reason phrase of the response.
        /// </summary>
        [JsonProperty("reason")]
        public string ReasonPhrase { get; set; }

        /// <summary>
        ///     The data of the response.
        /// </summary>
        [JsonProperty("data")]
        public TResult Data { get; set; }


        /// <summary>
        ///     Returns a json formatted string that represents the current object.
        /// </summary>
        /// <returns>
        ///     A json formatted string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        ///     Returns a json formatted string that represents the current object.
        /// </summary>
        /// <param name="tryDeserializeData">
        ///     If true, the JSON/XML formatted <see cref="ResponseMessage{TResult}.Data"/> will 
        ///     be deserialized at first. 
        /// </param>
        /// <returns>
        ///     A json formatted string that represents the current object.
        /// </returns>
        public string ToString(bool tryDeserializeData)
        {
            if (tryDeserializeData)
            {
                try
                {
                    if (this.Data != null && this.Data is string)
                    {
                        var strData = this.Data as string;

                        if (StringHelper.IsValidJson(strData))
                        {
                            var jobj = JObject.Parse(strData);

                            var response = new ResponseMessage<JObject>
                            {
                                Code = this.Code,
                                ReasonPhrase = this.ReasonPhrase,
                                Data = jobj
                            };

                            return response.ToString();
                        }
                        
                        if (StringHelper.IsValidXml(strData))
                        {
                            var xdoc = XDocument.Parse(strData);

                            var response = new ResponseMessage<XDocument>
                            {
                                Code = this.Code,
                                ReasonPhrase = this.ReasonPhrase,
                                Data = xdoc
                            };

                            return response.ToString();
                        }
                    }
                }
                catch
                {
                }
            }

            return this.ToString();
        }
    }
}
