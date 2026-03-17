using OlycksRapporteringV2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OlycksRapporteringV2.Application.Services
{
    //Design pattern: singleton

    //Jag använder singleton för att lättare hålla reda op inloggade användare genom hela programmet.
    //Utan singleton kan praktisktaget vilken funktion som hälst i programmet skapa en ny instans av UserSession
    //Med singleton förhindras detta, så det alltid är samma instans av inloggad användare.

    public class UserSession
    {
        //Singleton:
        //Skapar bara en privat statisk instans.
        private static UserSession _instance;
        //En privat constructor förhindrar att UserSession kan anroppas utifrån.
        private UserSession() { }

        //Den här kan alla kalla på.
        public static UserSession Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserSession();
                return _instance;
            }
        }
        public User CurrentUser { get; set; }
        public DateTime LoginTime { get; set; }
        public bool IsLoggedIn => CurrentUser != null;
        public void StartSession(User user)
        {
            CurrentUser = user;
            LoginTime = DateTime.Now;
        }

        public void EndSession()
        {
            CurrentUser = null;
        }


    }
}
