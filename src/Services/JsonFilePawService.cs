using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;

namespace ContosoCrafts.WebSite.Services
{
    /// <summary>
    /// JsonFilePawService will enable the pages to use the basic CRUDi operations 
    /// </summary>
    public class JsonFilePawService
    {
        /// <summary>
        /// Constructor of JsonFilePawService 
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        public JsonFilePawService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        // Variale of IWebHostEnvironment getter
        public IWebHostEnvironment WebHostEnvironment { get; }

        //JsonFileName -> Having the path of paws.json
        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "paws.json"); }
        }

        /// <summary>
        /// Method of IEnumerable of PawModel Type
        /// </summary>
        /// <returns> Deserialized JSON data </returns>
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

        /// <summary>
        /// CreatePaw -> This function create a new paw data and saves it to paws.json file
        /// </summary>
        /// <param name="paw"></param>
        /// <returns>True</returns>
        public bool CreatePaw(PawModel paw)
        {
            //newPaw will contain the details of new Paw
            var newPaw = new PawModel()
            {
                Id = paw.Id,
                Paw = new Paw
                {
                    Name = paw.Paw.Name,
                    Breed = paw.Paw.Breed,
                    Gender = paw.Paw.Gender,
                    Age = paw.Paw.Age,
                    Size = paw.Paw.Size,
                    Description = paw.Paw.Description,
                    Image = paw.Paw.Image,
                },
                Owner = new Owner
                {
                    Name = paw.Owner.Name,
                    Address = paw.Owner.Address,
                    City = paw.Owner.City,
                    Zipcode = paw.Owner.Zipcode,
                    Email = paw.Owner.Email,
                    Phone = paw.Owner.Phone
                }
            };
            //Getting the old data and appending it with new data
            var PawsData = GetPaws();
            PawsData = PawsData.Append(newPaw);
            //Saving the new data to json file
            SavePawsDataToJsonFile(PawsData);
            return true;
        }

        /// <summary>
        /// UpdatePaw :- Updates an existing paw and saving it on paws.json
        /// </summary>
        /// <param name="Paw"></param>
        /// <returns>True / False</returns>
        public bool UpdatePaw(PawModel Paw)
        {
            //Checking if the paw is existing or not
            var PawsData = GetPaws();
            var PawToUpdate = PawsData.FirstOrDefault(P => P.Id.Equals(Paw.Id));
            ///If the paw data is null it will return false
            if (PawToUpdate == null)
            {
                return false;
            }
            //If the Paw exists then update the data
            PawToUpdate.Paw.Name = Paw.Paw.Name;
            PawToUpdate.Paw.Breed = Paw.Paw.Breed;
            PawToUpdate.Paw.Gender = Paw.Paw.Gender;
            PawToUpdate.Paw.Age = Paw.Paw.Age;
            PawToUpdate.Paw.Size = Paw.Paw.Size;
            PawToUpdate.Paw.Gender = Paw.Paw.Gender;
            PawToUpdate.Paw.Description = Paw.Paw.Description;
            PawToUpdate.Paw.Image = Paw.Paw.Image;
            //Save the new data to the json file
            SavePawsDataToJsonFile(PawsData);
            return true;
        }

        /// <summary>
        /// DeletePaw - Delete the existing paw data and saves to the paw.json file
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True / False</returns>
        public bool DeletePaw(String id)
        {
            //Checking if the paw is existing or not
            var PawsData = GetPaws();
            var PawToDelete = PawsData.FirstOrDefault(P => P.Id.Equals(id));
            ///If the paw data is null it will return false
            if (PawToDelete == null)
            {
                return false;
            }
            //If the Paw exists then delete the data
            var UpdatedPawData = GetPaws().Where(m => m.Id.Equals(PawToDelete.Id) == false);
            //Save the new data to the json file
            SavePawsDataToJsonFile(UpdatedPawData);
            return true;
        }

        /// <summary>
        /// SavePawsDataToJsonFile - Take pawmodel as a arguement and save the whole model to the paws.json file
        /// </summary>
        /// <param name="paws"></param>
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
