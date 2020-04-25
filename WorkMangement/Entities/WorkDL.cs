using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMangement
{
    public class WorkDL
    {
        private readonly ApplicationDBContext dbContext;
        public WorkDL(ApplicationDBContext context)
        {
            dbContext = context;
        }

        /// <summary>
        /// Hàm lấy danh sách công việc 
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <returns></returns>
        public List<Work> GetAllWorks()
        {
            return dbContext.Works.ToList();
        }

        /// <summary>
        /// Hàm thêm công việc
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <returns></returns>
        public bool AddWork(WorkCreateView model)
        {
            try
            {
                Work pro = new Work
                {
                    WorkName = model.WorkName,
                    WorkDescription = model.WorkDescription,
                    WorkPhases = new List<Phase>()
                };

                dbContext.Works.Add(pro);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Hàm lấy thông tin công việc bằng id
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public WorkEditView GetWorkById(Guid workId)
        {
            var pro_db = dbContext.Works.FirstOrDefault(x => x.WorkId == workId);
            var pro = new WorkEditView
            {
                WorkId = pro_db.WorkId,
                WorkName = pro_db.WorkName,
                WorkDescription = pro_db.WorkDescription,
                WorkPhases = dbContext.Phases.Where(x => x.WorkId == workId).ToList()
            };
            return pro;
        }

        /// <summary>
        /// Hàm sửa thông tin công việc
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public bool EditWork(WorkEditView work)
        {
            var old_pro = dbContext.Works.FirstOrDefault(x => x.WorkId == work.WorkId);
            if(old_pro != null)
            {
                old_pro.WorkName = work.WorkName;
                old_pro.WorkDescription = work.WorkDescription;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Hàm xóa công việc theo id
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public bool DeleteWork(Guid workId)
        {
            try
            {
                var pro = dbContext.Works.FirstOrDefault(x => x.WorkId == workId);
                dbContext.Works.Remove(pro);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
