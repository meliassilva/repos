using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Byui.StudentListApi.Business.Model
{
    public class Student
    {
        /// <summary>
        /// The name of the course. EX: Object Oriented Programming
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// The course code. EX: CIT 260
        /// </summary>
        [JsonProperty("course_code")]
        public string CourseCode { get; set; }
        /// <summary>
        /// The email. EX: something@byui.edu
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
        /// <summary>
        /// The INumber. EX: 100000000
        /// </summary>
        [JsonProperty("INumber")]
        public int INumber { get; set; }
    }
}
