using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySleep.Model
{
    public class Request
    {

        public int Id { get; set; }
        public String ApplicantId { get; set; }
        public int OfferId { get; set; }
        public Boolean IsRequestAccepted { get; set; }

        public Request(string applicantId, int offerId, Boolean isRequestAccepted)
        {
            ApplicantId = applicantId;
            OfferId = offerId;
            IsRequestAccepted = isRequestAccepted;
        }

        [JsonConstructor]
        public Request(int id, string applicantId, int offerId, Boolean isRequestAccepted)
        {
            Id = id;
            ApplicantId = applicantId;
            OfferId = offerId;
            IsRequestAccepted = isRequestAccepted;
        }

        public override string ToString()
        {
            return ApplicantId + " " + OfferId + " " + IsRequestAccepted;
        }

    }
}
