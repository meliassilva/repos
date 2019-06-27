using Byui.WSO2Checker.Enterprise.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Byui.WSO2Checker.Enterprise.Configuration
{
    public class StudentListConfiguration : IStudentListConfiguration
    {
        public string WSO2Username { get; }

        public string WSO2Password { get; }

        public StudentListConfiguration(string wso2Username, string wso2Password)
        {
            WSO2Username = wso2Username;
            WSO2Password = wso2Password;
        }
    }
}
