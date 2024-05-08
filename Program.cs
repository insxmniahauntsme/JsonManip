﻿using System.Globalization;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using AutoMapper;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("C://Users/User/dotnet-course-code/HelloWorld/appsettings.json")
                .Build();

            DataDapper dapper = new DataDapper(config);

            string computersJson = File.ReadAllText("C://Users/User/dotnet-course-code/HelloWorld/Computers.json");

            IEnumerable<Computer>? computers = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            if (computers != null){

                foreach(Computer computer in computers){
                    string sql = @"INSERT INTO TutorialAppSchema.Computer (Motherboard,HasWifi,HasLTE,ReleaseDate,Price,VideoCard
                            ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
                            + "','" + computer.HasWifi
                            + "','" + computer.HasLTE
                            + "','" + computer.ReleaseDate?.ToString("yyyy-MM-dd")
                            + "','" + computer.Price.ToString("0.00", CultureInfo.InvariantCulture)
                            + "','" + EscapeSingleQuote(computer.VideoCard)
                    + "')";
 
                    dapper.ExecuteSql(sql);
                }

            }

            JsonSerializerSettings settings = new JsonSerializerSettings(){
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            string computersNewtonsoft = JsonConvert.SerializeObject(computers, settings);

            File.WriteAllText("NewtonsoftComputers.txt", computersNewtonsoft);

        }    

        static string EscapeSingleQuote(string input)
        {
            string output = input.Replace("'", "''");

            return output;
        }

    }
}
