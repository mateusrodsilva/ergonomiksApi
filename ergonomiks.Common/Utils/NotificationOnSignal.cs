using Nancy.Json;
using System.IO;
using System.Net;
using System.Text;


namespace ergonomiks.Common.Utils
{
    public static class NotificationOnSignal
    {
        public static void Notification(string title, string content)
        {
            var request = WebRequest.Create("https://onesignal.com/api/v1/notifications") as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("authorization", "Basic ZTc0NGJmOTYtYzcwZC00YzU3LWE4MmQtNGM1YzYzODk1OTM0");

            var serializer = new JavaScriptSerializer();
            var obj = new
            {
                app_id = "9e730ae1-3d7d-4a9a-b803-ef9c3eb94fa9",
                contents = new { en = content },
                included_segments = new string[] { "Subscribed Users" },
                headings = new {en = title},
            };
            var param = serializer.Serialize(obj);
            byte[] byteArray = Encoding.UTF8.GetBytes(param);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
            }

            System.Diagnostics.Debug.WriteLine(responseContent);
        }
    }
}
