using OlycksRapporteringV2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OlycksRapporteringV2.Application.Services
{
    public static class UserSession
    {
        public static User CurrentUser { get; set; }
        public static DateTime LoginTime { get; set; }
        public static bool IsLoggedIn => CurrentUser != null;
        public static void StartSession(User user)
        {
            CurrentUser = user;
            LoginTime = DateTime.Now;
        }

        public static void EndSession()
        {
            CurrentUser = null;
        }


    }
}
