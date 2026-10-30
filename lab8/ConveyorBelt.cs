using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    public delegate void ConveyorEventHandler();

    public class ConveyorBelt
    {
        private double currentWeight;
        public double MaxAllowedWeight { get; set; } = 100.0;

        public double CurrentWeight
        {
            get => currentWeight;
            set
            {
                if (value < 0) throw new ArgumentException("Вага не може бути від'ємною");
                currentWeight = value;
            }
        }

        public event ConveyorEventHandler? CriticalStop;
        public event ConveyorEventHandler? WarningOverload;
        public event Action<double>? WeightChecked;
        public event Action? ProcessCompleted;
        public event Action<string>? MessageGenerated;

        public async void CheckWeight(double weight)
        {
            CurrentWeight = weight;
            WeightChecked?.Invoke(CurrentWeight);

            if (CurrentWeight > MaxAllowedWeight)
            {
                CriticalStop?.Invoke();

                MessageGenerated?.Invoke("Система авто-розвантаження: зниження ваги...");
                while (CurrentWeight > MaxAllowedWeight)
                {
                    CurrentWeight -= 10;
                    MessageGenerated?.Invoke($"Поточна вага: {CurrentWeight} кг");
                    await Task.Delay(800);
                }

                MessageGenerated?.Invoke($"[Лінія]: Нормалізувалася. Поточна вага: {CurrentWeight} кг.");
                ProcessCompleted?.Invoke();
            }
            else if (CurrentWeight > 80)
            {
                WarningOverload?.Invoke();
                ProcessCompleted?.Invoke();
            }
            else
            {
                ProcessCompleted?.Invoke();
            }
        }

    }


}   
