using System;

namespace PoultryFarm.Domains
{
    public class WorkerCageAssignment
    {
        public long AssignmentId { get; set; }
        public long WorkerId { get; set; }
        public long CageId { get; set; }
        public DateTime AssignedDate { get; set; }
    }
}