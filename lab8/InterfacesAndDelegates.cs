using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8
{
    public interface IStopLine    {
        string StopLine();
    }
    public interface IDiagnose
    {
        string Diagnose();
    }
    public interface IReport
    {
        string MakeReport();
    }
}
