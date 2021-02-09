﻿using System;
using TaskList._01___Domain.Entities.Enums;

namespace TaskList._01___Domain
{
    public class Tasks : BaseEntity
    {

        public string Title { get; set; }

        public EnTypeStatus Status { get; set; }

        public string Description { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? EditDate { get; set; }

        public DateTime? ConclusionDate { get; set; }
    }
}
