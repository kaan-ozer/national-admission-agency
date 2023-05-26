﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using NAAProject.Data.Models.DAO;
using NAAProject.Data.Models.Domain;
using NAAProject.Data.Models.IDAO;
using NAAProject.Data.Models.Repository;
using NAAProject.Services.IService;

namespace NAAProject.Services.Service
{
    public class ApplicationService : IApplicationService
    {
        IApplicationDAO applicationDAO;
        IUserDAO userDAO;
        IUniversityDAO universityDAO;
        public ApplicationService()
        {
            applicationDAO = new ApplicationDAO();
            userDAO = new UserDAO();
            universityDAO = new UniversityDAO();
        }
        public Application GetApplication(int id)
        {
            using (NAAContext context = new NAAContext())
            {
                Application application = applicationDAO.GetApplication(context, id);
                return application;
            }
        }

        public IList<Application> GetApplications(string userId)
        {
            using (NAAContext context = new NAAContext())
            {
                return userDAO.GetApplicationCollection(context, userId);
            }
        }


		public bool UpdateApplication(Application application)
		{
			
			try
			{
				using (NAAContext context = new NAAContext())
				{
					applicationDAO.UpdateApplication(context, application);
					context.SaveChanges();
				}

				return true;
			}
			catch
			{
				return false;
			}
		}


		public bool AddApplication(Application application, string userId, int uniId)
        {
            try
            {
                using (NAAContext context = new NAAContext())
                {
                 
                 if (userDAO.GetApplicationCollection(context, userId).Count() >= 5) return false;        
                    applicationDAO.AddApplication(context, application);
                    userDAO.AddToCollection(application, userId, context);
                    universityDAO.AddToCollection(application, uniId, context);
                    context.SaveChanges();
                } 
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteApplication(int id)
        {
            try
            {
                using(NAAContext context = new NAAContext()){
                    Application application = applicationDAO.GetApplication(context, id);
                    applicationDAO.DeleteApplication(context, application);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
