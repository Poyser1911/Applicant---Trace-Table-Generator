using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicant
{
    class ApplicantO
    {
        public string name { get; set; }
        public double sal { get; set; }

        public double sald { get; set; }
        public double sal_net { get { return  sal - sald; } set { sal_net = value; } }

        public bool quilified { get { return (sal_net >= Housing.QualifyingSal); } set { quilified = value; } }

        public double expen { get; set; }
        public double repay { get; set; }

        public bool approved { get { return ((expen + repay) <= (sal_net / 2)); } set { approved = value; } }


    }
}
