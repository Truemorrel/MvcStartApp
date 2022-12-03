﻿using System;

namespace MvcStartApp.Models
{
    /// <summary>
    /// модель пользователя в блоге
    /// </summary>
    public class WebUser
    {
        // Уникальный идентификатор сущности в базе
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime JoinDate { get; set; }
    }

}
