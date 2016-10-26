using PastebookEntityLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pastebook.ViewModels
{
    public class SettingsViewModel
    {
        public USER USER { get; set; }
        public PasswordViewModel PASSWORD { get; set; }
    }
}