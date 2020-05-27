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
                var newPhase = new Phase
                {
                    PhaseId = new Guid(),
                    PhaseName = phase.PhaseName,
                    WorkId = phase.WorkId
                };

                dbContext.Phases.Add(newPhase);
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

        /// <summary>
        /// Hàm lấy danh sách các pha công việc được giao của nhân viên theo id nhân viên
        /// Nguyễn Đình Hoàng 20173143
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<UserPhasesViewModel> GetAllPhaseByEmployeeId(Guid employeeId)
        {
            List<UserPhasesViewModel> lst = new List<UserPhasesViewModel>();
            var phases = dbContext.Phases.Where(x => x.EmployeeId == employeeId);
            foreach(Phase ph in phases)
            {
                UserPhasesViewModel model = new UserPhasesViewModel
                {
                    PhaseId = ph.PhaseId,
                    WorkId = ph.WorkId,
                    PhaseName = ph.PhaseName,
                    WorkName = dbContext.Works.FirstOrDefault(x => x.WorkId == ph.WorkId).WorkName,
                    IsFinish = ph.IsFinish
                };
                lst.Add(model);
            }

            return lst;
        }

        /// <summary>
        /// Hàm cập nhật tình trạng công việc của nhân viên
        /// </summary>
        /// <param name="lst"></param>
        public bool UpdatePhasesStatus(List<UserPhasesViewModel> lst)
        {
            foreach(UserPhasesViewModel phase in lst)
            {
                var oldPhase = dbContext.Phases.FirstOrDefault(x => x.PhaseId == phase.PhaseId);
                var previousPhase = dbContext.Phases.FirstOrDefault(x => x.WorkId == oldPhase.WorkId && x.OrderNumber == oldPhase.OrderNumber - 1);
                if(previousPhase != null)
                {
                    if(previousPhase.IsFinish == false && phase.IsFinish)
                    {
                        return false;
                    }
                    else
                    {
                        oldPhase.IsFinish = phase.IsFinish;
                        dbContext.SaveChanges();
                    }
                }
                else
                {
                    oldPhase.IsFinish = phase.IsFinish;
                    dbContext.SaveChanges();
                }
            }

            return true;
        }
    }
}
