using Byui.LmsClients.LmsDataClient;
using Byui.StudentListApi.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Byui.StudentListApi.Business.Business
{
    public class StudentBusiness
    {

        private readonly LmsDataClient _lmsData;


        public StudentBusiness(LmsDataClient lmsData)
        {
            _lmsData = lmsData;
        }


        public List<Student> GetStudents(string entityCode)
        {

            _lmsData.GetEnrollmentsFromEntityDetailed(entityCode, false);
            var studentList = new List<Student> { };
            return studentList;

        }
    }
}
