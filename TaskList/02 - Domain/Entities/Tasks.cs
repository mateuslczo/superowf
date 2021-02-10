using System;
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

        public void ChangeDateForStatus(EnTypeStatus status)
        {

            switch (status)
            {
                case EnTypeStatus.Open:
                    this.CreateDate = DateTime.Now;
                    break;
                case EnTypeStatus.Inprogress:
                    this.EditDate = DateTime.Now;
                    break;
                case EnTypeStatus.Completed:
                    this.ConclusionDate = DateTime.Now;
                    break;
                case EnTypeStatus.Suspended:
                    ;
                    this.EditDate = DateTime.Now;
                    break;
                case EnTypeStatus.Canceled:
                    this.EditDate = DateTime.Now;
                    break;
                default:
                    this.EditDate = null;
                    this.ConclusionDate =null;
                    break;


            }

        }

    }
}

