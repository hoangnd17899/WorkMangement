using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkMangement
{
    public class PhaseDL
    {
        private readonly ApplicationDBContext dbContext;

        public PhaseDL(ApplicationDBContext context)
        {
            dbContext = context;
        }

        /// <summary>
        /// Hàm thêm pha vào công việc
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddPhase(PhaseCreateView phase)
        {
            try
            {
                var work = new Phase
                {
                    PhaseId = new Guid(),
                    PhaseName = phase.PhaseName,
                    WorkId = phase.WorkId
                };

                dbContext.Phases.Add(work);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Hàm xóa pha trong công việc
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        public Guid DeletePhase(Guid phaseId)
        {
            try
            {
                var work = dbContext.Phases.FirstOrDefault(x => x.PhaseId == phaseId);
                Guid projectId = work.WorkId;
                dbContext.Phases.Remove(work);
                dbContext.SaveChanges();
                return projectId;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Hàm lấy thông tin pha bằng id
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <param name="workId">id công việc</param>
        /// <returns></returns>
        public PhaseEditView GetPhaseById(Guid phaseId)
        {
            var work_db = dbContext.Phases.FirstOrDefault(x => x.PhaseId == phaseId);
            var work = new PhaseEditView
            {
                PhaseId = work_db.PhaseId,
                WorkId = work_db.WorkId,
                PhaseName = work_db.PhaseName,
                EmployeeId = work_db.EmployeeId,
                Employees = dbContext.Users.ToList()
            };
            return work;
        }

        /// <summary>
        /// Hàm thay đổi thông tin pha trong công việc
        /// Nguyễn Đình Hoàng - 20173143
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public bool EditPhase(PhaseEditView phase)
        {
            var old_pro = dbContext.Phases.FirstOrDefault(x => x.PhaseId == phase.PhaseId);
            if (old_pro != null)
            {
                old_pro.PhaseName = phase.PhaseName;
                old_pro.EmployeeId = phase.EmployeeId;
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
