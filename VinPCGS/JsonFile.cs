using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace VinPCGS
{
    /// <summary>
    /// Defines all methods for reading from and writing to the storedPath.json file. This file is stored at %USERPROFILE%\storedPaths.json
    /// </summary>
    internal static class JsonFile
    {
        private static readonly string storedPaths = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\storedPaths.json";

        /// <summary>
        /// Instantiate an object of type JsonSerializerOptions. Set WriteIndented property to true 
        /// in order to format any serialized / deserialized JSON data with white spaces.
        /// </summary>
        /// <remarks> Purpose is to make the JSON file human readable </remarks>
        /// <returns> JsonSerializerOptions object to format a JSON file with white spaces. </returns>
        private static JsonSerializerOptions GetWhiteSpaceFormatting()
        {
            JsonSerializerOptions jsonSettings = new JsonSerializerOptions();
            jsonSettings.WriteIndented = true;
            return jsonSettings;
        }

        /// <summary>
        /// Create the storedPath.json file with an empty string List.
        /// </summary>
        private static void CreateNewFile()
        {
            List<string> emptyFile = new List<string>();
            string jsonData = JsonSerializer.Serialize(emptyFile, GetWhiteSpaceFormatting());
            File.WriteAllText(storedPaths, jsonData);
        }

        /// <summary>
        /// Read all JSON text into a string. Deserialize the string, then return the List
        /// </summary>
        /// <returns> A string list of all stored file paths </returns>
        public static List<string> GetDeserializedList()
        {
            string jsonFileData = File.ReadAllText(storedPaths);
            List<string> jsonList = JsonSerializer.Deserialize<List<string>>(jsonFileData);
            return jsonList;
        }

        /// <summary>
        ///  Deserialize the JSON file as a string List, add a new file path, re-serialize the list, then save over the file. 
        /// </summary>
        /// <remarks> This method creates the program folder and the JSON file if they do not exist. </remarks>
        public static void WriteToFile(string newPath)
        {
            if (!File.Exists(storedPaths))
            {
                CreateNewFile();
            }

            List<string> jsonList = GetDeserializedList();   
            jsonList.Add(newPath);

            string serializedList = JsonSerializer.Serialize(jsonList, GetWhiteSpaceFormatting());
            File.WriteAllText(storedPaths, serializedList);
        }
    }
}
