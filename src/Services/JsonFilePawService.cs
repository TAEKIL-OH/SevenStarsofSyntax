using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    public class JsonFilePawService
    {
        //Initializing the webHostEnvironment so it can help to find the location of the json file
        public JsonFilePawService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        //Defining the path of the paws.json file
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "paws.json"); }
        }

        //This function will get the all paws data y opening the json file , deserialize it and read it to the end
        public IEnumerable<PawModel> GetPaws()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<PawModel[]>(jsonFileReader.ReadToEnd(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            }
        }

        public bool UpdatePaw(PawModel Paw)
        {
            var PawsData = GetPaws();
            var PawToUpdate = PawsData.FirstOrDefault(P => P.Id.Equals(Paw.Id));
            ///If the paw data is null it will return as it was null
            if (PawToUpdate == null)
            {
                return false;
            }
            PawToUpdate.Paw.Name = Paw.Paw.Name;
            PawToUpdate.Paw.Breed = Paw.Paw.Breed;
            PawToUpdate.Paw.Gender = Paw.Paw.Gender;
            PawToUpdate.Paw.Age = Paw.Paw.Age;
            PawToUpdate.Paw.Size = Paw.Paw.Size;
            PawToUpdate.Paw.Gender = Paw.Paw.Gender;
            PawToUpdate.Paw.Description = Paw.Paw.Description;
            PawToUpdate.Paw.Image = Paw.Paw.Image;
            SavePawsDataToJsonFile(PawsData);
            return true;
        }

        public void SavePawsDataToJsonFile(IEnumerable<PawModel> paws)
        {
            var json = JsonSerializer.Serialize(paws, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(JsonFileName, json);
        }
    }
}
