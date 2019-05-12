using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Sarif_Industries.Data_Access_Layer;
using Sarif_Industries.Models;
using Sarif_Industries.Security;

namespace Sarif_Industries.PartialModels
{
    public partial class User_PM
    {
        private ModelContext modelContext = new ModelContext();

        private CustomPasswordHasher customPasswordHasher = new CustomPasswordHasher();


        private string setFirstLetterCapital(string attribute)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(attribute);
        }

        private string setWhiteSpaceUpper(string attribute)
        {
            // remove white space if any
            Regex.Replace(attribute, @"\s+", "");

            // set white space
            return attribute.ToCharArray()
                .Aggregate("",
                (result, c) => result += ((!string.IsNullOrEmpty(result) && (result.Length + 1) % 4 == 0) ? " " : "")
                + c.ToString()
                );
        }

        private void getModifiedAttributes(User user)
        {
            user.Forename = setFirstLetterCapital(user.Forename);
            user.Surname = setFirstLetterCapital(user.Surname);
            user.Address = setFirstLetterCapital(user.Address);

            user.PostCode = setWhiteSpaceUpper(user.PostCode).ToUpper();
        }

        public void AddUser(User user)
        {
            getModifiedAttributes(user);

            user.Password = customPasswordHasher.HashPassword(user.Password);

            user.JoinDate = DateTime.Now;

            modelContext.Users.Add(user);
            modelContext.SaveChanges();
        }

        public void EditUser(User user)
        {
            getModifiedAttributes(user);

            user.Password = customPasswordHasher.HashPassword(user.Password);

            modelContext.Entry(user).State = EntityState.Modified;
            modelContext.SaveChanges();
        }
    }
}