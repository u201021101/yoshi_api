using System;

namespace Yoshi.EntityLayer.Model.Base
{
    public class BaseEntity
    {
        #region Constructor ------------------
        public BaseEntity()
        {
            CreatedOn = DateTime.Now;
            IsDeleted = false;
        }
        #endregion
        #region Properties -------------------
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        #endregion
    }
}
