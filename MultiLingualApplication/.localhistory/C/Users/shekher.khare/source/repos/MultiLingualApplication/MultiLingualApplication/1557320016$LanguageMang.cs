﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace MultiLingualApplication
{
    public class LanguageMang
    {
        public static List<Languages> AvailableLanguages = new List<Languages> {
            new Languages {
                LanguageFullName = "English", LanguageCultureName = "en"
            },
            new Languages {
                LanguageFullName = "Arabic", LanguageCultureName = "ar-SA"
            },
            new Languages {
                LanguageFullName = "French", LanguageCultureName = "fr-FR"
            },
            new Languages {
                LanguageFullName = "Russian", LanguageCultureName = "ru-RU"
            },
        };
        public static bool IsLanguageAvailable(string language)
        {
            return AvailableLanguages.Where(a => a.LanguageCultureName.Equals(language)).SingleOrDefault() != null ? true : false;
        }
        public static string GetDefaultLanguage()
        {
            return AvailableLanguages[0].LanguageCultureName;
        }
        public void SetLanguage(string language)
        {
            try
            {
                if (!IsLanguageAvailable(language)) language = GetDefaultLanguage();
                var cultureInfo = new CultureInfo(language);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                HttpCookie langCookie = new HttpCookie("culture", language);
                langCookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Current.Response.Cookies.Add(langCookie);
            }
            catch (Exception) { }
        }
    }
    public class Languages
    {
        public string LanguageFullName
        {
            get;
            set;
        }
        public string LanguageCultureName
        {
            get;
            set;
        }
    }
}