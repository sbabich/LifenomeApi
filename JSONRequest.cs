using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JSONRequest
{
    public class JSON : Dictionary<string, string> { }

    public class JSONRequest
    {
        public delegate void JSONRequestCompleteEvent(string response);
        private static JSONRequestCompleteEvent requestCompleteEvent;
        private static JSONRequestCompleteEvent postCompleteEvent;
        /// <summary>
        /// Send JSON request with callback event
        /// </summary>
        /// <param name="url">JSON Request URL</param>
        /// <param name="callback">Callback function, can be used with annonymous function</param>
        public static void GET(string url, JSONRequestCompleteEvent callback)
        {
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            requestCompleteEvent = callback;
            wc.DownloadStringAsync(new Uri(url));
        }

        static void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (requestCompleteEvent != null)
            {
                requestCompleteEvent(e.Result);
            }
        }

        /// <summary>
        /// Send POST JSON Request
        /// </summary>
        /// <param name="url">JSON Request URL</param>
        /// <param name="data">Array of data</param>
        /// <param name="callback">Call back function</param>
        public static void POST(string url, JSON data, JSONRequestCompleteEvent callback)
        {
            WebClient wc = new WebClient();
            string rdata = "";
            foreach (string key in data.Keys)
            {
                rdata += key + "=" + data[key] + "&";
            }
            postCompleteEvent = callback;
            wc.UploadStringCompleted += new UploadStringCompletedEventHandler(wc_UploadStringCompleted);
            wc.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            wc.Encoding = Encoding.UTF8;
            wc.UploadStringAsync(new Uri(url), "POST", rdata);
        }

        static void wc_UploadStringCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (postCompleteEvent != null)
            {
                postCompleteEvent(e.Result);
            }
        }

        /// <summary>
        /// Parse JSON string to key value pairs
        /// </summary>
        /// <param name="rawjson">JSON String</param>
        /// <returns>Key value pairs data</returns>
        public static JSON Parse(string rawjson)
        {
            JSON outdict = new JSON();
            StringBuilder keybufferbuilder = new StringBuilder();
            StringBuilder valuebufferbuilder = new StringBuilder();
            StringReader bufferreader = new StringReader(rawjson);

            int s = 0;
            bool reading = false;
            bool inside_string = false;
            bool reading_value = false;
            //break at end (returns -1)
            while (s >= 0)
            {
                s = bufferreader.Read();
                //opening of json
                if (!reading)
                {
                    if ((char)s == '{' && !inside_string && !reading) reading = true;
                    continue;
                }
                else
                {
                    //if we find a quote and we are not yet inside a string, advance and get inside
                    if (!inside_string)
                    {
                        //read past the quote
                        if ((char)s == '\"') inside_string = true;
                        continue;
                    }
                    if (inside_string)
                    {
                        //if we reached the end of the string
                        if ((char)s == '\"')
                        {
                            inside_string = false;
                            s = bufferreader.Read(); //advance pointer
                            if ((char)s == ':')
                            {
                                reading_value = true;
                                continue;
                            }
                            if (reading_value && (char)s == ',')
                            {
                                //we know we just ended the line, so put itin our dictionary
                                if (!outdict.ContainsKey(keybufferbuilder.ToString())) outdict.Add(keybufferbuilder.ToString(), valuebufferbuilder.ToString());
                                //and clear the buffers
                                keybufferbuilder.Clear();
                                valuebufferbuilder.Clear();
                                reading_value = false;
                            }
                            if (reading_value && (char)s == '}')
                            {
                                //we know we just ended the line, so put itin our dictionary
                                if (!outdict.ContainsKey(keybufferbuilder.ToString())) outdict.Add(keybufferbuilder.ToString(), valuebufferbuilder.ToString());
                                //and clear the buffers
                                keybufferbuilder.Clear();
                                valuebufferbuilder.Clear();
                                reading_value = false;
                                reading = false;
                                break;
                            }
                        }
                        else
                        {
                            if (reading_value)
                            {
                                valuebufferbuilder.Append((char)s);
                                continue;
                            }
                            else
                            {
                                keybufferbuilder.Append((char)s);
                                continue;
                            }
                        }
                    }
                    else
                    {
                        switch ((char)s)
                        {
                            case ':':
                                reading_value = true;
                                break;
                            default:
                                if (reading_value)
                                {
                                    valuebufferbuilder.Append((char)s);
                                }
                                else
                                {
                                    keybufferbuilder.Append((char)s);
                                }
                                break;
                        }
                    }
                }
            }
            return outdict;
        }
    }

}