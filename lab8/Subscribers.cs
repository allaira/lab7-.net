using lab8;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    public class Master : IStopLine, IDiagnose, IReport
    {
        public event Action<string>? MessageGenerated;

        public string StopLine() => "Терміново зупинити лінію!";
        public string Diagnose() => "Майстер перевіряє механічне заклинювання стрічки.";
        public string MakeReport() => "Майстер подав звіт про аварійну зупинку.";
        public void OnCriticalStop()
        {
            MessageGenerated?.Invoke($"Майстер: {StopLine()}");
        }
        public void OnWarning()
        {
            MessageGenerated?.Invoke($"Майстер: Попередження! Зменшити подачу деталей.");
        }
    }
    public class Engineer : IDiagnose
    {
        public event Action<string>? MessageGenerated;

        public string Diagnose() => "Діагностувати причину";
        public void OnCriticalStop()
        {
            MessageGenerated?.Invoke($"Інженер: {Diagnose()}");

        }
        public void OnWarning()
        {
            MessageGenerated?.Invoke($"Інженер: Перевірка температури підшипників.");
        }
    }
    public class Administration : IReport
    {
        public event Action<string>? MessageGenerated;

        public string MakeReport() => "Зафіксувати простій";
        public void OnCriticalStop()
        {
            MessageGenerated?.Invoke($"Адміністарція: {MakeReport()}");
        }
    }
}
