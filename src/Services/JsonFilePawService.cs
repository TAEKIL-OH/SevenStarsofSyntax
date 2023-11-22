using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
        public IEnumerable<DetailedPawModel> GetPaws()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<DetailedPawModel[]>(jsonFileReader.ReadToEnd(),
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
        public bool CreatePaw(DetailedPawModel paw)
        {
            //newPaw will contain the details of new Paw
            var newPaw = new DetailedPawModel()
            {
                Id = paw.Id,
                Paw = new PawModel
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
        public bool UpdatePaw(DetailedPawModel Paw)
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
            PawToUpdate.Owner.Name = Paw.Owner.Name;
            PawToUpdate.Owner.Address = Paw.Owner.Address;
            PawToUpdate.Owner.Phone = Paw.Owner.Phone;
            PawToUpdate.Owner.City = Paw.Owner.City;
            PawToUpdate.Owner.Zipcode = Paw.Owner.Zipcode;
            PawToUpdate.Owner.Email = Paw.Owner.Email;
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
        /// Search Paw :- Search the existing paw and return the data
        /// </summary>
        /// <param name="pawName"></param>
        /// <returns></returns>
        public IEnumerable<DetailedPawModel> SearchPaw(String pawName)
        {
            //Checking if the paw exists
            var PawsData = GetPaws();
            var PawToGet = PawsData.FirstOrDefault(P => P.Paw.Name.Equals(pawName));

            //If the paw does not exist then return null
            if (PawToGet == null)
            {
                return null;
            }

            //Else return the data
            return new List<DetailedPawModel> { PawToGet };
        }

        /// <summary>
        /// AddFeedback will add feedback to an existing paw data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AddFeedckToPaw(string id, string message)
        {
            //Checking if the paw is existing or not
            var PawsData = GetPaws();
            var PawToUpdate = PawsData.FirstOrDefault(P => P.Id.Equals(id));

            ///If the paw data is null it will return false
            if (PawToUpdate == null)
            {
                return false;
            }

            //If the feedback field is not present then make it and add the message
            if (PawToUpdate.Paw.Feedback == null)
            {
                PawToUpdate.Paw.Feedback = new string[] { message };
                SavePawsDataToJsonFile(PawsData);
                return true;
            }

            //else append the message in the array

            var feedbacks = PawToUpdate.Paw.Feedback.ToList();
            feedbacks.Add(message);
            PawToUpdate.Paw.Feedback = feedbacks.ToArray();

            //Save the data to json file
            SavePawsDataToJsonFile(PawsData);

            return true;
        }


        /// <summary>
        /// Add Meetup will add new entry of meetup
        /// </summary>
        /// <param name="pawOne"></param>
        /// <param name="pawTwo"></param>
        /// <param name="dateOfMeetup"></param>
        /// <param name="location"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AddMeetup(
            string pawOne,
            string pawTwo,
            string dateOfMeetup,
            string location,
            string message)
        {
            //Getting paw lists
            var PawsData = GetPaws();

            //Getting both of paws to update
            var FirstPawToUpdate = PawsData.FirstOrDefault(P => P.Id.Equals(pawOne));
            var SecondPawToUpdate = PawsData.FirstOrDefault(P => P.Id.Equals(pawTwo));

            //Checking if first paw exists
            if (FirstPawToUpdate == null)
            {
                return false;
            }

            //If there is no entry of meetup lists then it will add first entry
            if (FirstPawToUpdate.BookingLists.Count == 0)
            {
                FirstPawToUpdate.BookingLists.Add(new MeetupModel()
                {
                    PawToMeet = pawTwo,
                    DateOfMeetup = dateOfMeetup,
                    LocationOfMeetup = location,
                    SpecialMessage = message
                });
            }

            //If there are entry available then append it to the entries
            if (FirstPawToUpdate.BookingLists.Count > 0)
            {
                List<MeetupModel> meetups = FirstPawToUpdate.BookingLists;
                meetups.Add(new MeetupModel()
                {
                    PawToMeet = pawTwo,
                    DateOfMeetup = dateOfMeetup,
                    LocationOfMeetup = location,
                    SpecialMessage = message
                });
            }

            //Checking if second paw exists
            if (SecondPawToUpdate == null)
            {
                return false;
            }

            //If there is no entry of meetup lists then it will add first entry
            if (SecondPawToUpdate.BookingLists.Count == 0)
            {
                SecondPawToUpdate.BookingLists.Add(new MeetupModel()
                {
                    PawToMeet = pawOne,
                    DateOfMeetup = dateOfMeetup,
                    LocationOfMeetup = location,
                    SpecialMessage = message
                });
            }

            //If there are entry available then append it to the entries
            if (SecondPawToUpdate.BookingLists.Count > 0)
            {
                List<MeetupModel> meetups = SecondPawToUpdate.BookingLists;
                meetups.Add(new MeetupModel()
                {
                    PawToMeet = pawOne,
                    DateOfMeetup = dateOfMeetup,
                    LocationOfMeetup = location,
                    SpecialMessage = message
                });
            }

            //Save the data to json file
            SavePawsDataToJsonFile(PawsData);

            return true;

        }

        /// <summary>
        /// SavePawsDataToJsonFile - Take pawmodel as a arguement and save the whole model to the paws.json file
        /// </summary>
        /// <param name="paws"></param>
        public void SavePawsDataToJsonFile(IEnumerable<DetailedPawModel> paws)
        {
            var json = JsonSerializer.Serialize(paws, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(JsonFileName, json);
        }

    }

}