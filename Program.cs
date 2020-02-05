using System;
using System.IO;
using static System.Console;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

// 2020-02-05 TL edit
// Quickstart: Detect faces in an image using the Face REST API and C#
// https://docs.microsoft.com/en-us/azure/cognitive-services/face/quickstarts/csharp
// Detect human faces in an image, return face rectangles, and optionally with faceIds, landmarks, and attributes.


namespace DetectFace
{
    class Program
    {
        const string subscriptionKey = "527c05ceeb5441c182d8050d62a33710";
        const string uriBase = "https://tryneanalyser.cognitiveservices.azure.com/face/v1.0/detect";
        static void Main(string[] args)
        {
            // Get the path and filename to process from the user.
            WriteLine("*** Detect faces ***");
            // Write("Enter the path to an image with faces that you wish to analyze: ");
            // string imageFilePath = Console.ReadLine();
            var imageFilePath = @"c:\woman.jpg";
            WriteLine("Input file: " + imageFilePath);
            FileInfo fi = new FileInfo(imageFilePath);
            if (File.Exists(imageFilePath))
            {
                try
                {
                    WriteLine("Input file exists.");
                    double size = fi.Length;
                    WriteLine("Input file's size in Bytes: {0}", size);
                    MakeAnalysisRequest(imageFilePath);
                    WriteLine("\nWait a moment for the results to appear.\n");
                }
                catch (Exception e)
                {
                    WriteLine("\n" + e.Message + "\nPress Enter to exit...\n");
                }
            }
            else
            {
                WriteLine("\nInvalid file path.\nPress Enter to exit...\n");
            }
            Console.ReadLine();
        }
        // Gets the analysis of the specified image by using the Face REST API.
        static async void MakeAnalysisRequest(string imageFilePath)
        {
            HttpClient client = new HttpClient();

            // Request headers.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

            // Request parameters. A third optional parameter is "details".
            string requestParameters = "returnFaceId=true&returnFaceLandmarks=false" +
                "&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses," +
                "emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

            // Assemble the URI for the REST API Call.
            string uri = uriBase + "?" + requestParameters;
            WriteLine(uri);
            HttpResponseMessage response;

            // Request body. Posts a locally stored JPEG image.
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using (ByteArrayContent content = new ByteArrayContent(byteData))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json"
                // and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Execute the REST API call.
                response = await client.PostAsync(uri, content);
                WriteLine("Uri: " + uri);
                WriteLine("Content: " + content);
                WriteLine("Response: " + response);
                // Get the JSON response.
                string contentString = await response.Content.ReadAsStringAsync();
                WriteLine("\nContentString:\n");
                WriteLine(contentString);
                // Display the JSON response.
                WriteLine("\nResponse:\n");
                string jsonoutput = JsonPrettyPrint(contentString);
                WriteLine(jsonoutput);
                WriteToFile(jsonoutput);
                WriteLine("\nPress Enter to exit...");
            }
        }
        // Returns the contents of the specified file as a byte array.
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }
        // Formats the given JSON string by adding line breaks and indents.
        static string JsonPrettyPrint(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;

            foreach (char ch in json)
            {
                switch (ch)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\'':
                        if (quote) ignore = !ignore;
                        break;
                }

                if (quote)
                    sb.Append(ch);
                else
                {
                    switch (ch)
                    {
                        case '{':
                        case '[':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(ch);
                            break;
                        case ',':
                            sb.Append(ch);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(ch);
                            sb.Append(' ');
                            break;
                        default:
                            if (ch != ' ') sb.Append(ch);
                            break;
                    }
                }
            }
            return sb.ToString().Trim();
        }

        static void WriteToFile(string json)
        {
            // define a directory path to output files
            // starting in the user's folder
            var dir = Combine(
              GetFolderPath(SpecialFolder.Personal), "json");

            CreateDirectory(dir);

            // define file paths
            string textFile = Combine(dir, "face.json");
            WriteLine($"Writing to: {textFile}");

            // create a new text file and write a line to it 
            StreamWriter textWriter = File.CreateText(textFile);
            textWriter.WriteLine(json);
            textWriter.Close();

            // Managing paths
            WriteLine($"Folder Name: {GetDirectoryName(textFile)}");
            WriteLine($"File Name: {GetFileName(textFile)}");
            // Getting file information
            var info = new FileInfo(textFile);
            WriteLine($"Contains {info.Length} bytes");
            WriteLine($"Last accessed {info.LastAccessTime}");
            WriteLine($"Has readonly set to {info.IsReadOnly}");
        }
    }
}
