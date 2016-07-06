﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineDiary.Models.CRUDViewModels
{
    public class CreateUserViewModel : UserViewModel
    {
        private DiaryUser user = null;
        private ApplicationDbContext context = new ApplicationDbContext();

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(100, ErrorMessage = "Миниум 6 символов", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public CreateUserViewModel()
        {            
        }

        public CreateUserViewModel(DiaryUser user)
        {
            this.user = user;

            this.Id = user.Id;
            this.Email = user.Email;
            this.UserName = user.UserName;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.ParentName = user.ParentName;
        }
    }
}