using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA1.Core.Dto.EntityModels
{
    public class ModelProfile
    {
        public ModelProfile()
        {
            DateEdited = DateTime.Now;
        }
        [Key]
        public Guid ModelProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Build { get; set; }
        public string Nationality { get; set; }
        public string Country { get; set; }
        public string CityOfResident { get; set; }
        public DateTime DateEdited { get; set; }
        public string NickName { get; set; }
         public bool? Gender { get; set; }
        public int Age { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string HairColor { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EyeColor { get; set; }
        public string Ethinicity { get; set; }       
        public string BreastSize { get; set; }
        public string PubicHair { get; set; }
        public string Language { get; set; }
        public string SexualPreference { get; set; }       
        public string BodyChanges { get; set; }
        public string Toy { get; set; }
        public string AboutMe { get; set; }
        public string MyCompetence { get; set; }
        public string WhatAttractMe { get; set; }
        public bool? IsModelAccountDeleted { get; set; }
        public string RegisterId { get; set; } 
        public Register Register { get; set; }  


    }
}
