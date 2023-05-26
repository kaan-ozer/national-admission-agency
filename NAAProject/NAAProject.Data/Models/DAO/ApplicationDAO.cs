﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAAProject.Data.Models.Domain;
using NAAProject.Data.Models.IDAO;
using NAAProject.Data.Models.Repository;

namespace NAAProject.Data.Models.DAO
{
    public class ApplicationDAO : IApplicationDAO
    {
        IUserDAO userDAO;

        public ApplicationDAO()
        {
           userDAO = new UserDAO();
        }
        public IList<Application> GetApplications(NAAContext context)
        {
            return context.Applications.ToList();
        }
         
         
        public Application GetApplication(NAAContext context, int id)
        {
            context.Applications.ToList();
            return context.Applications.Find(id);
        }

        public void AddApplication(NAAContext context, Application application)
        {
            //Maximal 5 pro User erlauben
            context.Applications.Add(application);
		}
        public void DeleteApplication(NAAContext context, Application application)
        {
            context.Applications.Remove(application);
        }
        public void UpdateApplication(NAAContext context, Application application)
        {
            Application a = context.Applications.Find(application.ApplicationId);
            context.Entry(a).CurrentValues.SetValues(application);
        }
    }
}
